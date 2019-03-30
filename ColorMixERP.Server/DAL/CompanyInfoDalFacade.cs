using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Entities.Tables;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class CompanyInfoDalFacade
    {
        private LinqContext db = null;

        public CompanyInfoDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<CompanyInfoDTO> GetCompanyInfos(PaginationDTO cmd, ref int pagesCount)
        {
            var query = from c in db.CompanyInfos
                where c.IsDeleted == false
                select new CompanyInfoDTO()
                {
                    Accountant = c.Accountant,
                    Address = c.Address,
                    BankDetails = c.BankDetails,
                    City = c.City,
                    Director = c.Director,
                    INN = c.INN,
                    LegalName = c.LegalName,
                    Id = c.Id,
                    MFO = c.MFO,
                    PaymentAccount = c.PaymentAccount,
                    Phone = c.Phone,
                    OKONX = c.OKONX
                };


            pagesCount = (int) Math.Ceiling((double) (from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public CompanyInfoDTO GetCompanyInfo(int id)
        {
            var query = from c in db.CompanyInfos
                where c.Id == id
                select new CompanyInfoDTO()
                {
                    Accountant = c.Accountant,
                    Address = c.Address,
                    BankDetails = c.BankDetails,
                    City = c.City,
                    Director = c.Director,
                    INN = c.INN,
                    LegalName = c.LegalName,
                    Id = c.Id,
                    MFO = c.MFO,
                    PaymentAccount = c.PaymentAccount,
                    Phone = c.Phone,
                    OKONX = c.OKONX
                };

            return query.FirstOrDefault();
        }
        
        public void Add(CompanyInfoDTO c)
        {
            var compInfo = new CompanyInfo()
            {
                Accountant = c.Accountant,
                Address = c.Address,
                BankDetails = c.BankDetails,
                City = c.City,
                Director = c.Director,
                INN = c.INN,
                LegalName = c.LegalName,
                Id = c.Id,
                MFO = c.MFO,
                PaymentAccount = c.PaymentAccount,
                Phone = c.Phone,
                OKONX = c.OKONX
            };
            db.CompanyInfos.InsertOnSubmit(compInfo);
            db.SubmitChanges();
        }

        public void Update(CompanyInfoDTO c)
        {
            var compInfo = (from p in db.CompanyInfos where p.Id == c.Id select p).FirstOrDefault();
            
            compInfo.Accountant = c.Accountant;
            compInfo.Address = c.Address;
            compInfo.BankDetails = c.BankDetails;
            compInfo.City = c.City;
            compInfo.Director = c.Director;
            compInfo.INN = c.INN;
            compInfo.LegalName = c.LegalName;
            compInfo.MFO = c.MFO;
            compInfo.PaymentAccount = c.PaymentAccount;
            compInfo.Phone = c.Phone;
            compInfo.OKONX = c.OKONX;
            compInfo.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from p in db.CompanyInfos where p.Id == id select p).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
