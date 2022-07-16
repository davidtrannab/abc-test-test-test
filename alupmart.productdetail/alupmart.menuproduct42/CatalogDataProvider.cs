using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
/// <summary>
/// Summary description for CategoryDataProvider
/// </summary>
namespace ALupMartV2.Manager
{
    public class CatalogDataProvider
    {
        MartLinQDataContext martLinQDataContext;
        public CatalogDataProvider()
        {
            if (martLinQDataContext == null)
                martLinQDataContext = new MartLinQDataContext();

        }

        #region SEARCH STOREPRO
        public List<SearchChildCatalogByParentCatalogResult> SearchChildCatalogByParentCatalog(int id)
        {
            var temp = martLinQDataContext.SearchChildCatalogByParentCatalog(id);
            return temp.ToList();
        }
        public SearchCatalogByCatalogIDResult SearchCatalogByCatalog(int id)
        {
            var temp = martLinQDataContext.SearchCatalogByCatalogID(id);
            if (temp != null)
                return temp.First();
            else return null;
        }
        public Array SearchCatalogLevel2(int langId)
        {
            var temp = martLinQDataContext.SearchCatalogLevel2(langId);

            return temp.ToArray();
        }
        #endregion

        #region SEARCH
        public List<CategoryClass> SearchCategoryByNameAndLangIDAndIsPublic(string CategoryName, string isPublic, int LangID, int page, int SIZE)
        {
            List<ALupMart_Catalog> category;
            if (isPublic == "-1")
            {
                category = (from p in martLinQDataContext.ALupMart_Catalogs
                            where p.LangID == LangID
                            where p.CatalogName.Contains(CategoryName)

                            orderby p.CatalogID descending
                            select p).Skip(page * SIZE).Take(SIZE).ToList();
            }
            else
            {
                bool isPublic_ = bool.Parse(isPublic);
                category = (from p in martLinQDataContext.ALupMart_Catalogs
                            where p.LangID == LangID
                            where p.CatalogName.Contains(CategoryName)
                            where p.ALupMart_CatalogsInfo.IsPublic == isPublic_

                            orderby p.CatalogID descending
                            select p).Skip(page * SIZE).Take(SIZE).ToList();
            }

            List<CategoryClass> categoryList = new List<CategoryClass>(category.Count());
            foreach (ALupMart_Catalog record in category)
            {
                CategoryClass categoryClass = new CategoryClass(record.ALupMart_CatalogsInfo.PortalId, record.CatalogID,
                                                                 record.CatalogName,
                                                                 record.ALupMart_CatalogsInfo.ParentID,
                                                                 record.Descriptions,
                                                                 record.ALupMart_CatalogsInfo.ImageSource,
                                                                 (bool)record.ALupMart_CatalogsInfo.IsPublic,
                                                                 (bool)record.ALupMart_CatalogsInfo.IsLastCatalog,
                                                                 record.ALupMart_CatalogsInfo.Order);
                categoryClass.CreateDate = record.ALupMart_CatalogsInfo.CreateDate;
                categoryList.Add(categoryClass);
            }
            return categoryList;
        }

        public int CountByNameAndLangIDAndIsPublic(int portalId, string CategoryName, string isPublic, int LangID)
        {

            int count = 0;
            if (isPublic == "-1")
            {

                count = (from p in martLinQDataContext.ALupMart_Catalogs
                         where p.ALupMart_CatalogsInfo.PortalId == portalId && p.LangID == LangID
                         where p.CatalogName.Contains(CategoryName)

                         select p).Count();
            }
            else
            {
                bool isPublic_ = bool.Parse(isPublic);
                count = (from p in martLinQDataContext.ALupMart_Catalogs
                         where p.ALupMart_CatalogsInfo.PortalId == portalId && p.LangID == LangID
                         where p.CatalogName.Contains(CategoryName)
                         where p.ALupMart_CatalogsInfo.IsPublic == isPublic_
                         select p).Count();

            }

            return count;
        }


        public List<CategoryClass> SearchCategoryByIsLastCatalog(int portalId, bool isLastCatalog, int langID)
        {
            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.PortalId == portalId && p.ALupMart_CatalogsInfo.IsLastCatalog == isLastCatalog
                           && p.LangID == langID

                           orderby p.CatalogID
                           select p;
            List<CategoryClass> categoryList = new List<CategoryClass>(category.Count());
            foreach (ALupMart_Catalog record in category)
            {
                CategoryClass categoryClass = new CategoryClass(record.ALupMart_CatalogsInfo.PortalId, record.CatalogID,
                                                                 record.CatalogName,
                                                                 record.ALupMart_CatalogsInfo.ParentID,
                                                                 record.Descriptions,
                                                                 record.ALupMart_CatalogsInfo.ImageSource,
                                                                 (bool)record.ALupMart_CatalogsInfo.IsPublic,
                                                                 (bool)record.ALupMart_CatalogsInfo.IsLastCatalog, record.ALupMart_CatalogsInfo.Order);

                categoryList.Add(categoryClass);
            }
            return categoryList;
        }
        public string searchIDbyName(string name)
        {
            var temp = from p in martLinQDataContext.ALupMart_Catalogs
                       where p.ALupMart_CatalogsInfo.IsLastCatalog == true
                       && p.LangID == 1
                       && p.CatalogName == name

                       select p;
            if (temp.Count() > 0)
                return temp.First().CatalogID.ToString();
            return "";

        }

        public List<ALupMart_Catalog> SearchCategoryCAP1(int langID)
        {
            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.ParentID < 0
                           where p.ALupMart_CatalogsInfo.IsPublic == true
                           orderby p.ALupMart_CatalogsInfo.Order

                           select p;

            return category.ToList();
        }

        public List<CategoryClass> SearchAllChildCategoryByCatalogID(int CatalogID, int langID)
        {
            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.ParentID == CatalogID
                           where p.ALupMart_CatalogsInfo.IsPublic == true
                           orderby p.ALupMart_CatalogsInfo.Order

                           select p;
            List<CategoryClass> categoryList = new List<CategoryClass>(category.Count());
            foreach (ALupMart_Catalog record in category)
            {
                CategoryClass categoryClass = new CategoryClass(record.ALupMart_CatalogsInfo.PortalId, record.CatalogID, record.CatalogName, record.ALupMart_CatalogsInfo.ParentID, record.Descriptions, record.ALupMart_CatalogsInfo.ImageSource, record.ALupMart_CatalogsInfo.IsPublic, record.ALupMart_CatalogsInfo.IsLastCatalog, record.ALupMart_CatalogsInfo.Order);
                categoryList.Add(categoryClass);
            }
            return categoryList;
        }
        public List<CategoryClass> SearchAllCategoryHome(int langID)
        {
            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.IsLastCatalog == true
                           where p.ALupMart_CatalogsInfo.IsPublic == true
                           orderby p.ALupMart_CatalogsInfo.Order

                           select p;
            List<CategoryClass> categoryList = new List<CategoryClass>(category.Count());
            foreach (ALupMart_Catalog record in category)
            {
                CategoryClass categoryClass = new CategoryClass(record.ALupMart_CatalogsInfo.PortalId, record.CatalogID, record.CatalogName, record.ALupMart_CatalogsInfo.ParentID, record.Descriptions, record.ALupMart_CatalogsInfo.ImageSource, record.ALupMart_CatalogsInfo.IsPublic, record.ALupMart_CatalogsInfo.IsLastCatalog, record.ALupMart_CatalogsInfo.Order);
                categoryList.Add(categoryClass);
            }
            return categoryList;
        }
        public List<ALupMart_Catalog> SearchAllCategory(long CatalogID, int langID)
        {
            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.IsPublic == true
                           && p.CatalogID != CatalogID && p.LangID == langID
                           select p;
            return category.ToList();
        }
        public ALupMart_Catalog SearchCatalogByID2(int PortalId, long CategoryID, int langID)
        {
            ALupMart_Catalog cat = martLinQDataContext.ALupMart_Catalogs.Single(p => p.ALupMart_CatalogsInfo.PortalId == PortalId && p.CatalogID == CategoryID);
            return cat;
        }

        public CategoryClass searchCategoryByID(int PortalId, long CategoryID, int langID)
        {
            CategoryClass categoryClass = new CategoryClass();
            var category = martLinQDataContext.ALupMart_CatalogsInfos.Single(p => p.PortalId == PortalId && p.CatalogID == CategoryID);

            categoryClass.IsLastCatalog = (bool)category.IsLastCatalog;
            categoryClass.IsPublish = (bool)category.IsPublic;
            categoryClass.ParentID = category.ParentID;
            categoryClass.Order = category.Order;
            categoryClass.Number = category.Number;
            categoryClass.IsDiscounts = category.IsDiscounts;
            categoryClass.BeginDate = category.BeginDate;
            categoryClass.EndDate = category.EndDate;

            foreach (ALupMart_Catalog re in category.ALupMart_Catalogs)
            {
                if (re.LangID == langID)
                {
                    categoryClass.CatalogName = re.CatalogName;
                    categoryClass.Description = re.Descriptions;
                    categoryClass.SeoDescription = re.SeoDescription;
                    categoryClass.SeoKeyWord = re.SeoKeyword;
                    categoryClass.SeoTitle = re.SeoTitle;
                    categoryClass.Temp = re.Temp;
                    categoryClass.KeyWord = re.KeyWord;
                    return categoryClass;
                }
            }

            return categoryClass;
        }

        public List<ALupMart_Catalog> searchCategoryChildByID(int catalogID, int langID)
        {

            var catalog = from p in martLinQDataContext.ALupMart_Catalogs
                          where p.ALupMart_CatalogsInfo.ParentID == catalogID
                          where p.LangID == langID
                          where p.ALupMart_CatalogsInfo.IsPublic == true

                          select p;
            return catalog.ToList();
        }


        public LinkedList<CategoryClass> searchCategoryChildByID(int catalogID, bool isLastCatalog, int langID)
        {
            LinkedList<CategoryClass> linkedList = new LinkedList<CategoryClass>();
            CategoryClass catalogClass = new CategoryClass();
            var catalog = from p in martLinQDataContext.ALupMart_Catalogs
                          where p.CatalogID == catalogID
                          where p.LangID == langID
                          where p.ALupMart_CatalogsInfo.IsPublic == true
                          select p;

            catalogClass.CatalogName = catalog.First().CatalogName;
            catalogClass.CatalogID = catalogID;

            linkedList.AddLast(catalogClass);
            FindcatalogChild(ref linkedList, catalogID, isLastCatalog, langID);
            return linkedList;
        }
        protected void FindcatalogChild(ref LinkedList<CategoryClass> linkedList, long catalogID, bool isLastCatalog, int langID)
        {
            var catalog = from p in martLinQDataContext.ALupMart_Catalogs
                          where p.ALupMart_CatalogsInfo.ParentID == catalogID
                          where p.ALupMart_CatalogsInfo.IsPublic == true
                          && p.LangID == langID
                          select p;
            CategoryClass catalogClass;
            foreach (ALupMart_Catalog record in catalog)
            {
                catalogClass = new CategoryClass();
                catalogClass.CatalogID = record.CatalogID;
                catalogClass.CatalogName = record.CatalogName;
                linkedList.AddLast(catalogClass);
                FindcatalogChild(ref linkedList, record.CatalogID, isLastCatalog, langID);
            }
        }

        public long[] FindChildCatalogByCatalogID(long catalogID)
        {
            LinkedList<long> linkedList = new LinkedList<long>();
            CategoryClass catalogClass = new CategoryClass();
            var catalog = from p in martLinQDataContext.ALupMart_CatalogsInfos
                          where p.CatalogID == catalogID
                          //where p.ALupMart_CatalogsInfo.IsPublic==true

                          select p;
            linkedList.AddLast(catalogID);
            FindcatalogChild(ref linkedList, catalogID, true);
            long[] arrayCatalog = new long[linkedList.Count()];
            int j = 0;
            foreach (long i in linkedList)
            {
                arrayCatalog[j] = i;
                j++;
            }
            return arrayCatalog;
        }
        protected void FindcatalogChild(ref LinkedList<long> linkedList, long catalogID, bool isLastCatalog)
        {
            var catalog = from p in martLinQDataContext.ALupMart_CatalogsInfos
                          where p.ParentID == catalogID
                          //where p.IsLastCatalog == isLastCatalog
                          //where p.IsPublish==true

                          select p;

            foreach (ALupMart_CatalogsInfo record in catalog)
            {
                linkedList.AddLast(record.CatalogID);
                FindcatalogChild(ref linkedList, record.CatalogID, isLastCatalog);
            }
        }
        #endregion

        #region INSERT
        public long InsertCategory(ALupMart_Catalog catalog, ALupMart_CatalogsInfo cataloginfo)
        {
            cataloginfo.ALupMart_Catalogs.Add(catalog);
            martLinQDataContext.ALupMart_CatalogsInfos.InsertOnSubmit(cataloginfo);
            martLinQDataContext.SubmitChanges();
            return cataloginfo.CatalogID;
        }
        #endregion

        #region UPDATE
        public void UpdateCategory(ALupMart_Catalog catalog, ALupMart_CatalogsInfo cataloginfo)
        {
            //Sửa bảng CatalogInfo
            var category = martLinQDataContext.ALupMart_CatalogsInfos.Single(p => p.CatalogID == cataloginfo.CatalogID);
            category.IsLastCatalog = cataloginfo.IsLastCatalog;
            category.IsPublic = cataloginfo.IsPublic;
            category.ParentID = cataloginfo.ParentID;
            category.Order = cataloginfo.Order;
            if (!string.IsNullOrEmpty(cataloginfo.ImageSource))
                category.ImageSource = cataloginfo.ImageSource;

            category.BeginDate = cataloginfo.BeginDate;
            category.EndDate = cataloginfo.EndDate;
            category.IsDiscounts = cataloginfo.IsDiscounts;
            category.Number = cataloginfo.Number;

            martLinQDataContext.SubmitChanges();

            //Sửa bang catalog
            try
            {
                var temp = martLinQDataContext.ALupMart_Catalogs.Single(p => p.CatalogID == catalog.CatalogID && p.LangID == catalog.LangID);
                temp.CatalogName = catalog.CatalogName;
                temp.Descriptions = catalog.Descriptions;
                temp.SeoTitle = catalog.SeoTitle;
                temp.SeoKeyword = catalog.SeoKeyword;
                temp.SeoDescription = catalog.SeoDescription;
                temp.Temp = catalog.Temp;
                temp.KeyWord = catalog.KeyWord;
                martLinQDataContext.SubmitChanges();
            }
            catch
            {
                martLinQDataContext.ALupMart_Catalogs.InsertOnSubmit(catalog);
                martLinQDataContext.SubmitChanges();
            }



        }
        #endregion

        #region UPDATE PUBLIC
        public void UpdatePublic(long[] id, bool isPublic)
        {
            var temp = from p in martLinQDataContext.ALupMart_CatalogsInfos
                       where id.Contains(p.CatalogID)
                       select p;
            foreach (ALupMart_CatalogsInfo re in temp)
                re.IsPublic = isPublic;
            martLinQDataContext.SubmitChanges();
        }

        public void UpdatePublic(long id)
        {
            ALupMart_CatalogsInfo temp = martLinQDataContext.ALupMart_CatalogsInfos.Single(p => p.CatalogID == id);
            temp.IsPublic = !temp.IsPublic;

            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region DELETE
        public bool DeleteCategoryCheck(long[] CatalogId, int i)
        {
            MartLinQDataContext DeleteLinqDataSource, UpdateLinqDataSource;
            int j;
            bool IsSucc = true;
            ALupMart_CatalogsInfo CatalogMe = new ALupMart_CatalogsInfo();
            for (j = 0; j < i; j++)
            {
                /*Tìm bản ghi tương ứng*/
                DeleteLinqDataSource = new MartLinQDataContext();
                CatalogMe = DeleteLinqDataSource.ALupMart_CatalogsInfos.Single(p => p.CatalogID.Equals(CatalogId[j]) == true);
                if (CatalogMe.IsLastCatalog == false)
                {
                    DeleteLinqDataSource.ALupMart_CatalogsInfos.DeleteOnSubmit(CatalogMe);
                    DeleteLinqDataSource.SubmitChanges();
                }
                else
                {

                    int countNews = 0;//new DataProvider().CountRecordByCategoryID(CatalogId[j]);
                    if (CatalogMe.ALupMart_ProductsInfos.Count() > 0)
                    {
                        IsSucc = false;
                        continue;
                    }
                    else
                    {
                        DeleteLinqDataSource.ALupMart_CatalogsInfos.DeleteOnSubmit(CatalogMe);
                        DeleteLinqDataSource.SubmitChanges();
                        continue;
                    }
                }
                /*Tìm các bản ghi con cap nhat lai ParentID*/
                UpdateLinqDataSource = new MartLinQDataContext();
                var CatalogChild = from p1 in UpdateLinqDataSource.ALupMart_CatalogsInfos
                                   where p1.ParentID.Equals(CatalogId[j]) == true
                                   select p1;
                foreach (ALupMart_CatalogsInfo CatalogRow in CatalogChild)
                    CatalogRow.ParentID = CatalogMe.ParentID;
                UpdateLinqDataSource.SubmitChanges();

            }
            return IsSucc;
        }
        #endregion

        #region CHECK PUBLICK PRODUCT
        public bool CheckProductPublic(int PortalId, long productID)
        {
            ALupMart_ProductsInfo temp = new ProductDataProvider().SearchProductByID(PortalId, productID);
            return CheckCatalogPublic(PortalId, temp.CatalogID);
        }

        protected bool CheckCatalogPublic(int PortalId, long CatalogID)
        {
            if (CatalogID < 0) return true;
            ALupMart_CatalogsInfo temp = SearchCatalogInfoByID(PortalId, CatalogID);
            if (!temp.IsPublic)
                return false;

            else
            {
                bool isPublic = CheckCatalogPublic(PortalId, temp.ParentID);
            }
            return true;
        }
        public ALupMart_CatalogsInfo SearchCatalogInfoByID(int PortalId, long id)
        {
            return martLinQDataContext.ALupMart_CatalogsInfos.Single(p => p.CatalogID == id && p.PortalId == PortalId);
        }
        #endregion

        #region COUNT
        public int countChildCatalogByCatalogID(long CatalogID)
        {
            return (from p in martLinQDataContext.ALupMart_CatalogsInfos
                    where p.ParentID == CatalogID
                    select p).Count();
        }

        public int CountAll()
        {
            return (from p in martLinQDataContext.ALupMart_CatalogsInfos
                    select p).Count();
        }
        #endregion


        /////////////////////

        #region INSERT MANUFACTURER
        public void InsertManufacturer(ALupMart_Manufacturer record)
        {
            martLinQDataContext.ALupMart_Manufacturers.InsertOnSubmit(record);
            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region INSERT MANUFACTURER
        public void UpdateManufacturer(int PortalId, ALupMart_Manufacturer record)
        {
            ALupMart_Manufacturer manu = martLinQDataContext.ALupMart_Manufacturers.Single(p => p.ManufacturerID == record.ManufacturerID && p.PortalId == PortalId);
            manu.ManufacturerName = record.ManufacturerName;
            if (!string.IsNullOrEmpty(record.LogoUrl))
                manu.LogoUrl = record.LogoUrl;
            manu.Description = record.Description;
            manu.Orders = record.Orders;
            manu.SeoTitle = record.SeoTitle;
            manu.SeoKeyword = record.SeoKeyword;
            manu.SeoDescription = record.SeoDescription;
            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region SEARCH MANUDCTURER
        public ALupMart_Manufacturer searchManufacturerByID(int PortalId, long manufacturerID)
        {
            return martLinQDataContext.ALupMart_Manufacturers.Single(p => p.ManufacturerID == manufacturerID && p.PortalId == PortalId);
        }
        public List<ALupMart_Manufacturer> searchAllManufacturer(int PortalId, string name)
        {
            var manufacturer = (from p in martLinQDataContext.ALupMart_Manufacturers
                                where p.PortalId == PortalId && p.ManufacturerName.Contains(name)
                                orderby p.Orders, p.ManufacturerName ascending
                                select p);
            return manufacturer.ToList();
        }

        public int CountAllManufacturer(string name, bool isdeleted)
        {
            return (from p in martLinQDataContext.ALupMart_Manufacturers
                    where p.ManufacturerName.Contains(name)

                    select p).Count();

        }
        public List<ALupMart_Manufacturer> searchAllManufacturer(string groupName)
        {
            var manufacturer = from p in martLinQDataContext.ALupMart_Manufacturers

                               orderby p.Orders ascending
                               select p;
            return manufacturer.ToList();
        }
        public List<ALupMart_Manufacturer> searchAllManufacturer()
        {
            var manufacturer = from p in martLinQDataContext.ALupMart_Manufacturers
                               orderby p.Orders ascending
                               select p;
            return manufacturer.ToList();
        }
        public List<ALupMart_Manufacturer> searchAllManufacturer(int PortalId, long catalogID, string name)
        {
            var manufacturer = (from p in martLinQDataContext.ALupMart_Manufacturers
                                where p.PortalId == PortalId && p.ManufacturerName.Contains(name)
                                //where p.ALupMart_ProductsInfos. == catalogID
                                where p.IsActive == true
                                orderby p.Orders ascending
                                select p);
            return manufacturer.ToList();
        }
        #endregion

        #region DELETE MANUFACTURR


        public void Delete(long[] id)
        {
            var temp = from p in martLinQDataContext.ALupMart_Manufacturers
                       where id.Contains(p.ManufacturerID)
                       select p;

            martLinQDataContext.ALupMart_Manufacturers.DeleteAllOnSubmit(temp);

            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region INIT MANUFACTURER
        public void InitDropDownList(int PortalId, ref DropDownList dropDownList)
        {
            var temp = from p in martLinQDataContext.ALupMart_Manufacturers
                       where p.PortalId == PortalId
                       orderby p.ManufacturerName
                       select p;

            ListItem Item = new ListItem("--Chọn hãng sản xuất--", "-1");
            dropDownList.Items.Add(Item);
            foreach (ALupMart_Manufacturer re in temp)
            {
                Item = new ListItem(HttpUtility.HtmlDecode(re.ManufacturerName), re.ManufacturerID.ToString());
                dropDownList.Items.Add(Item);
            }
        }
        public void InitDropDownList(int PortalId, ref ListBox dropDownList)
        {
            var temp = from p in martLinQDataContext.ALupMart_Manufacturers
                       where p.PortalId == PortalId
                       orderby p.ManufacturerName
                       select p;

            ListItem Item;

            foreach (ALupMart_Manufacturer re in temp)
            {
                Item = new ListItem(re.ManufacturerName, re.ManufacturerID.ToString());
                dropDownList.Items.Add(Item);
            }
        }
        #endregion
        public Array SearchCategoryCAP1Active_1(int langID)
        {
            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.ParentID < 0
                           where p.ALupMart_CatalogsInfo.IsPublic == true
                           //where p.ALupMart_CatalogsInfo.IsMenu == true
                           orderby p.ALupMart_CatalogsInfo.Order

                           select new { p.CatalogID, p.CatalogName, p.Descriptions, p.ALupMart_CatalogsInfo.ImageSource, p.ALupMart_CatalogsInfo.AnhNenMenu, p.ALupMart_CatalogsInfo.IsAnhNen };

            return category.ToArray();
        }
         public List<ALupMart_Catalog> SearchAllChildrenCatalogAMenuTrue(long ca)
        {
            var temp = from p in martLinQDataContext.ALupMart_Catalogs
                       where p.ALupMart_CatalogsInfo.ParentID == ca
                       where p.ALupMart_CatalogsInfo.IsPublic == true
                       where p.ALupMart_CatalogsInfo.IsMenu == true
                       select p;
            return temp.ToList();
        }
        public List<CategoryClass> SearchCategoryCAP1Active_2(int langID)
        {

            var category = from p in martLinQDataContext.ALupMart_Catalogs
                           where p.ALupMart_CatalogsInfo.ParentID < 0
                           where p.ALupMart_CatalogsInfo.IsPublic == true
                           where p.ALupMart_CatalogsInfo.IsMenu == true
                           orderby p.ALupMart_CatalogsInfo.Order

                           select p;
            List<CategoryClass> ListCata = new List<CategoryClass>();
            foreach (ALupMart_Catalog re in category)
            {
                CategoryClass categoryClass = new CategoryClass();
                categoryClass.CatalogID = re.CatalogID;
                categoryClass.CatalogName = re.CatalogName;
                categoryClass.Description = re.Descriptions;
                categoryClass.ImageSource = re.ALupMart_CatalogsInfo.ImageSource;
                categoryClass.AnhNenMenu = re.ALupMart_CatalogsInfo.AnhNenMenu;
                categoryClass.IsAnhNen = re.ALupMart_CatalogsInfo.IsAnhNen.Value;

                ListCata.Add(categoryClass);
            }

            return ListCata;
        }
        public List<ALupMart_Catalog> SearchAllChildrenCatalogA(long ca)
        {
            var temp = from p in martLinQDataContext.ALupMart_Catalogs where p.ALupMart_CatalogsInfo.ParentID == ca select p;
            return temp.ToList();
        }

        public List<CategoryClass> SearchCategoryByName(int portalId, string key, long idLoai)
        {
            var list = SearchCategoryByParentID(portalId, "└-------", key, -1, idLoai);
            return list.ToList();
        }
        public List<CategoryClass> SearchCategoryByParentID(int portalId, string sp, string key, long paID, long idLoai)
        {
            List<CategoryClass> list = new List<CategoryClass>();
            CategoryClass cate;
            var temp = from p in martLinQDataContext.ALupMart_Catalogs
                       where p.ALupMart_CatalogsInfo.PortalId == portalId && p.CatalogName.Contains(key)
                       && p.ALupMart_CatalogsInfo.ParentID == paID && p.ALupMart_CatalogsInfo.CatalogID != idLoai
                       && p.LangID == 1
                       orderby p.ALupMart_CatalogsInfo.Order
                       select p;
            foreach (ALupMart_Catalog re in temp)
            {
                List<CategoryClass> list1 = SearchCategoryByParentID(portalId, "-------", key, re.ALupMart_CatalogsInfo.CatalogID, idLoai);
                cate = new CategoryClass();
                cate.CatalogID = re.CatalogID;
                cate.CatalogName = re.CatalogName;
                cate.CreateDate = re.ALupMart_CatalogsInfo.CreateDate;
                cate.ImageSource = re.ALupMart_CatalogsInfo.ImageSource;
                cate.IsLastCatalog = re.ALupMart_CatalogsInfo.IsLastCatalog;
                cate.IsPublish = re.ALupMart_CatalogsInfo.IsPublic;
                cate.Order = re.ALupMart_CatalogsInfo.Order;
                list.Add(cate);
                foreach (CategoryClass re1 in list1)
                {
                    re1.CatalogName = sp + re1.CatalogName;
                    list.Add(re1);
                }
            }
            return list;
        }

        public CategoryClass searchCategoryByID(int CategoryID, int langID)
        {
            CategoryClass categoryClass = new CategoryClass();
            var category = martLinQDataContext.ALupMart_CatalogsInfos.Single(p => p.CatalogID == CategoryID);
            categoryClass.IsLastCatalog = (bool)category.IsLastCatalog;
            categoryClass.IsPublish = (bool)category.IsPublic;
            categoryClass.ParentID = category.ParentID;
            foreach (ALupMart_Catalog record in category.ALupMart_Catalogs)
            {
                if (record.LangID == langID)
                {
                    categoryClass.CatalogName = record.CatalogName;
                    categoryClass.Description = record.Descriptions;
                    return categoryClass;
                }
            }
            return categoryClass;
        }

        #region INSERT COLOR
        public void InsertColor(ALupMart_Color record)
        {
            martLinQDataContext.ALupMart_Colors.InsertOnSubmit(record);
            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region INSERT MANUFACTURER
        public void UpdateColor(ALupMart_Color record)
        {
            ALupMart_Color manu = martLinQDataContext.ALupMart_Colors.Single(p => p.Id == record.Id);
            manu.Title = record.Title;
            if (!string.IsNullOrEmpty(record.ImageSource))
                manu.ImageSource = record.ImageSource;
            manu.IsPublic = record.IsPublic;
            manu.Order = record.Order;
            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region SEARCH MANUDCTURER
        public ALupMart_Color searchColorByID(long manufacturerID)
        {
            return martLinQDataContext.ALupMart_Colors.Single(p => p.Id == manufacturerID);
        }
        public List<ALupMart_Color> searchAllColor(string name)
        {
            var manufacturer = (from p in martLinQDataContext.ALupMart_Colors
                                where p.Title.Contains(name)
                                orderby p.Order, p.Id ascending
                                select p);
            return manufacturer.ToList();
        }

        public int CountAllColor(string name)
        {
            return (from p in martLinQDataContext.ALupMart_Colors
                    where p.Title.Contains(name)

                    select p).Count();

        }

        public List<ALupMart_Color> searchAllColor()
        {
            var manufacturer = from p in martLinQDataContext.ALupMart_Colors
                               orderby p.Order ascending
                               select p;
            return manufacturer.ToList();
        }
        #endregion

        #region DELETE MANUFACTURR


        public void DeleteColor(long[] id)
        {
            var temp = from p in martLinQDataContext.ALupMart_Colors
                       where id.Contains(p.Id)
                       select p;

            martLinQDataContext.ALupMart_Colors.DeleteAllOnSubmit(temp);

            martLinQDataContext.SubmitChanges();
        }
        #endregion

        #region INIT MANUFACTURER
        public void InitColorDropDownList(ref DropDownList dropDownList)
        {
            var temp = from p in martLinQDataContext.ALupMart_Colors

                       orderby p.Title
                       select p;

            ListItem Item = new ListItem("--Chọn màu--", "-1");
            dropDownList.Items.Add(Item);
            foreach (ALupMart_Color re in temp)
            {
                Item = new ListItem(HttpUtility.HtmlDecode(re.Title), re.Id.ToString());
                dropDownList.Items.Add(Item);
            }
        }
        public void InitColorDropDownList(ref ListBox dropDownList)
        {
            var temp = from p in martLinQDataContext.ALupMart_Colors
                       orderby p.Title
                       select p;

            ListItem Item;

            foreach (ALupMart_Color re in temp)
            {
                Item = new ListItem(re.Title, re.Id.ToString());
                dropDownList.Items.Add(Item);
            }
        }
        #endregion
    }
}