

using EmailClient.Models;
using QuickApp.SQLDAL.Models;
using QuickApp.SQLDAL.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;

namespace QuickApp.Business
{
    public class EmailManager
    {
        // ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private UnitOfWork unitOfWork = new UnitOfWork();
        private QuickappContext context = new QuickappContext();

        public IEnumerable<EmailModal> GetAll(System.Linq.Expressions.Expression<Func<EmailModal, bool>> expression = null)
        {
            try
            {
                var result = unitOfWork.EmailRepository.GetAll().Select(a => new EmailModal
                {
                    Id = a.Id,
                    EmialType = a.EmailTypeId
                });
                return result;
            }
            catch (Exception ex)
            {
                // logger.Error(ex.Message, ex);
                throw ex;
            }

        }


        /// <summary>
        /// GetPropertyById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmailModal GetById(long id)
        {
            try
            {
                var employeeModel = unitOfWork.EmailRepository.Find(a => a.Id == id).Select(a => new EmailModal
                {
                    Id = a.Id,
                    EmialType = a.EmailTypeId

                }).FirstOrDefault();


                return employeeModel;
            }
            catch (Exception ex)
            {
                // logger.Error(ex.Message, ex);
                throw ex;
            }
        }
       

       
        //public StatusMessage Create(EmployeeModel model)
        //{
        //    StatusMessage statusMessage = new StatusMessage();
        //    try
        //    {
        //        Employee objChk = unitOfWork.EmployeeRepository.GetAll(a => a.FirstName.ToLower() == model.FirstName.ToLower()).FirstOrDefault();



        //        if (objChk == null)
        //        {
        //            Employee employeeModel = new Employee();
        //            //objChk.PropertyID = model.PropertyID;
        //            employeeModel.FirstName = model.FirstName;
        //            employeeModel.LastName = model.LastName;
        //            employeeModel.Email = model.Email;
        //            employeeModel.MobileNumber = model.MobileNumber;
        //            employeeModel.Gender = model.Gender;
        //            employeeModel.City = model.City;

        //            unitOfWork.EmployeeRepository.Add(employeeModel);
        //            unitOfWork.Save();

        //            //statusMessage.ReturnMessage = CommonFunctions.strRecordCreated;
        //            statusMessage.ReturnStatus = true;

        //        }
        //        else
        //        {
        //            //statusMessage.ReturnMessage = objChk.Active == true ? CommonFunctions.strActiveRecordExists : CommonFunctions.strDeactiveRecordExists;
        //            statusMessage.ReturnStatus = false;
        //        }
        //        return statusMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        // logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}

       
        //public StatusMessage Edit(Property model)
        //{
        //    StatusMessage statusMessage = new StatusMessage();
        //    try
        //    {
        //        Property objChk = unitOfWork.PropertyRepository.GetAll(a => a.PropertyID != model.PropertyID && a.PropertyName.ToLower() == model.PropertyName.ToLower()).FirstOrDefault();

        //        if (objChk == null)
        //        {
        //            Property dbPropertyModel = unitOfWork.PropertyRepository.GetAllWithInclude(a => a.PropertyID == model.PropertyID, null, "PropertyFlatDetails,PropertyFlatDetails.PropertyFlatDocuments,PropertyFlatDetails.PropertyFlatDocuments.DocumentType,PropertyFlatDetails.PropertyFlatRoomDetails,PropertyFlatDetails.PropertyFlatRoomDetails.PropertyRoomBedDetails").FirstOrDefault();
        //            dbPropertyModel.PropertyName = model.PropertyName;
        //            dbPropertyModel.Address1 = model.Address1;
        //            dbPropertyModel.Address2 = model.Address2;
        //            dbPropertyModel.Address3 = model.Address3;
        //            dbPropertyModel.LandMark = model.LandMark;
        //            dbPropertyModel.FkCountryId = model.FkCountryId;
        //            dbPropertyModel.FKRegionId = model.FKRegionId;
        //            dbPropertyModel.FKStateId = model.FKStateId;
        //            dbPropertyModel.FKCityId = model.FKCityId;
        //            dbPropertyModel.FKAreaId = model.FKAreaId;
        //            dbPropertyModel.PinCode = model.PinCode;
        //            dbPropertyModel.GoogleMapLink = model.GoogleMapLink;
        //            dbPropertyModel.Active = true;
        //            dbPropertyModel.ModifiedOn = new CommonFunctions().ServerDate();
        //            dbPropertyModel.ModifiedBy = model.CreatedBy;

        //            UpdateFlat(model, dbPropertyModel);

        //            //unitOfWork.Save();
        //            unitOfWork.PropertyRepository.Edit(dbPropertyModel);
        //            unitOfWork.Save();
        //            //ts.Complete();

        //            statusMessage.ReturnMessage = CommonFunctions.strRecordUpdated;
        //            statusMessage.ReturnStatus = true;

        //        }
        //        else
        //        {
        //            statusMessage.ReturnMessage = objChk.Active == true ? CommonFunctions.strRecordActivated : CommonFunctions.strRecordDeactivated;
        //            statusMessage.ReturnStatus = false;
        //        }
        //        return statusMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}







      
        //public StatusMessage ActivateDeactivate(long id, int userid)
        //{
        //    StatusMessage statusMessage = new StatusMessage();
        //    try
        //    {
        //        Property model = unitOfWork.PropertyRepository.GetById(id);
        //        if (model != null)
        //        {
        //            model.Active = model.Active == false ? true : false;  // 0-> Deactive , 1 -> Active
        //            model.ModifiedBy = userid;
        //            model.ModifiedOn = new CommonFunctions().ServerDate();
        //            unitOfWork.PropertyRepository.Edit(model);
        //            unitOfWork.Save();

        //            statusMessage.ReturnMessage = model.Active == true ? CommonFunctions.strRecordActivated : CommonFunctions.strRecordDeactivated;
        //            statusMessage.ReturnStatus = true;
        //        }
        //        else
        //        {
        //            statusMessage.ReturnMessage = CommonFunctions.strRecordDeactivatingError;
        //            statusMessage.ReturnStatus = false;
        //        }
        //        return statusMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}



    }
}
