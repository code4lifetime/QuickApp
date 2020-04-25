
import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

import { AlertService, DialogType, MessageSeverity } from 'src/app/services/alert.service';
import { AppTranslationService } from 'src/app/services/app-translation.service';
import { AccountService } from 'src/app/services/account.service';
import { Utilities } from 'src/app/services/utilities';
import { User } from 'src/app/models/user.model';
import { Role } from 'src/app/models/role.model';
import { Permission } from 'src/app/models/permission.model';
import { MatTableDataSource,MatSort,MatPaginator } from '@angular/material';
import { UserInfoComponent } from '../../controls/user-info.component';
import { UserEdit } from 'src/app/models/user-edit.model';



@Component({
    selector: 'users-management',
    templateUrl: './users-management-new.component.html',
    styleUrls: ['./users-management-new.component.scss']
})
export class UsersManagementComponent implements OnInit, AfterViewInit {

  listData: MatTableDataSource<any>;
  displayedColumns: string[] = [];
  @ViewChild(MatSort , {static: false}) sort: MatSort;
  @ViewChild(MatPaginator , {static: false}) paginator: MatPaginator;


    columns: any[] = [];
    rows: User[] = [];
    rowsCache: User[] = [];
    editedUser: UserEdit;
    sourceUser: UserEdit;
    editingUserName: { name: string };
    loadingIndicator: boolean;

    allRoles: Role[] = [];

    searchKey: string;


    @ViewChild('indexTemplate', {static: false})
    indexTemplate: TemplateRef<any>;

    @ViewChild('userNameTemplate', {static: false})
    userNameTemplate: TemplateRef<any>;

    @ViewChild('rolesTemplate', {static: false})
    rolesTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate', {static: false})
    actionsTemplate: TemplateRef<any>;

    @ViewChild('editorModal', {static: false})
    editorModal: ModalDirective;

    @ViewChild('userEditor', {static: false})
    userEditor: UserInfoComponent;

    constructor(private alertService: AlertService, private translationService: AppTranslationService, private accountService: AccountService) {
    }


    ngOnInit() {

        const gT = (key: string) => this.translationService.getTranslation(key);

        this.displayedColumns = ['jobTitle', 'userName', 'fullName', 'email', 'roles', 'phoneNumber'];

        if (this.canManageUsers) {
          this.displayedColumns.push('actions');
          }
        // this.columns = [
        //     { prop: 'index', name: '#', width: 40, cellTemplate: this.indexTemplate, canAutoResize: false },
        //     { prop: 'jobTitle', name: gT('users.management.Title'), width: 50 },
        //     { prop: 'userName', name: gT('users.management.UserName'), width: 90, cellTemplate: this.userNameTemplate },
        //     { prop: 'fullName', name: gT('users.management.FullName'), width: 120 },
        //     { prop: 'email', name: gT('users.management.Email'), width: 140 },
        //     { prop: 'roles', name: gT('users.management.Roles'), width: 120, cellTemplate: this.rolesTemplate },
        //     { prop: 'phoneNumber', name: gT('users.management.PhoneNumber'), width: 100 }
        // ];

        // if (this.canManageUsers) {
        //     this.columns.push({ name: '', width: 160, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });
        // }

        this.loadData();
    }


    ngAfterViewInit() {

        this.userEditor.changesSavedCallback = () => {
            this.addNewUserToList();
            this.editorModal.hide();
        };

        this.userEditor.changesCancelledCallback = () => {
            this.editedUser = null;
            this.sourceUser = null;
            this.editorModal.hide();
        };
    }


    addNewUserToList() {
        if (this.sourceUser) {
            Object.assign(this.sourceUser, this.editedUser);

            let sourceIndex = this.rowsCache.indexOf(this.sourceUser, 0);
            if (sourceIndex > -1) {
                Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
            }

            sourceIndex = this.rows.indexOf(this.sourceUser, 0);
            if (sourceIndex > -1) {
                Utilities.moveArrayItem(this.rows, sourceIndex, 0);
            }

            this.editedUser = null;
            this.sourceUser = null;
        } else {
            const user = new User();
            Object.assign(user, this.editedUser);
            this.editedUser = null;

            let maxIndex = 0;
            for (const u of this.rowsCache) {
                if ((<any>u).index > maxIndex) {
                    maxIndex = (<any>u).index;
                }
            }

            (<any>user).index = maxIndex + 1;

            this.rowsCache.splice(0, 0, user);
            this.rows.splice(0, 0, user);
            this.rows = [...this.rows];
        }
    }


    loadData() {
        this.alertService.startLoadingMessage();
        this.loadingIndicator = true;

        if (this.canViewRoles) {
            this.accountService.getUsersAndRoles().subscribe(results => this.onDataLoadSuccessful(results[0], results[1]), error => this.onDataLoadFailed(error));
        } else {
            this.accountService.getUsers().subscribe(users => this.onDataLoadSuccessful(users, this.accountService.currentUser.roles.map(x => new Role(x))), error => this.onDataLoadFailed(error));
        }
    }


    onDataLoadSuccessful(users: User[], roles: Role[]) {

        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        users.forEach((user, index, users) => {
            (<any>user).index = index + 1;
        });

        this.rowsCache = [...users];
        this.rows = users;
        this.allRoles = roles;


        this.listData = new MatTableDataSource(users);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
        // this.listData.filterPredicate = (data, filter) => {
        //   debugger;
        //   return this.displayedColumns.some(ele => {
        //     return ele != 'actions' && data[ele].toLowerCase().indexOf(filter) != -1;
        //   });
        // };

    }


    onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;

        this.alertService.showStickyMessage('Load Error', `Unable to retrieve users from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
    }




    onEditorModalHidden() {
        this.editingUserName = null;
        this.userEditor.resetForm(true);
    }


    newUser() {
        this.editingUserName = null;
        this.sourceUser = null;
        this.editedUser = this.userEditor.newUser(this.allRoles);
        this.editorModal.show();
    }


    editUser(row: UserEdit) {
        this.editingUserName = { name: row.userName };
        this.sourceUser = row;
        this.editedUser = this.userEditor.editUser(row, this.allRoles);
        this.editorModal.show();
    }


    deleteUser(row: UserEdit) {
        this.alertService.showDialog('Are you sure you want to delete \"' + row.userName + '\"?', DialogType.confirm, () => this.deleteUserHelper(row));
    }


    deleteUserHelper(row: UserEdit) {

        this.alertService.startLoadingMessage('Deleting...');
        this.loadingIndicator = true;

        this.accountService.deleteUser(row)
            .subscribe(results => {
                this.alertService.stopLoadingMessage();
                this.loadingIndicator = false;

                this.rowsCache = this.rowsCache.filter(item => item !== row);
                this.rows = this.rows.filter(item => item !== row);
            },
            error => {
                this.alertService.stopLoadingMessage();
                this.loadingIndicator = false;

                this.alertService.showStickyMessage('Delete Error', `An error occured whilst deleting the user.\r\nError: "${Utilities.getHttpResponseMessages(error)}"`,
                    MessageSeverity.error, error);
            });
    }



    get canAssignRoles() {
        return this.accountService.userHasPermission(Permission.assignRolesPermission);
    }

    get canViewRoles() {
        return this.accountService.userHasPermission(Permission.viewRolesPermission);
    }

    get canManageUsers() {
        return this.accountService.userHasPermission(Permission.manageUsersPermission);
    }

    onSearchChanged(value: string) {

      this.listData.filter = value.toLowerCase();
    }

}
