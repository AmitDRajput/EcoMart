using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    public class DBDataSync
    {
        public DBDataSync()
        {
        }

        public string GetMasterProductDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from masterproduct";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select * from masterproduct";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["ProductID"]), "masterproduct", "ProductID"))
                    {
                        string strSqlUpdate = GetMasterProductUpdateQuery(Convert.ToInt32(dr["ProductID"]), Convert.ToString(dr["ProdName"]),
Convert.ToString(dr["ProdLoosePack"]), Convert.ToString(dr["ProdPack"]), Convert.ToString(dr["ProdPackType"]), Convert.ToString(dr["ProdBoxQuantity"]), Convert.ToString(dr["ProdCompShortName"]), Convert.ToString(dr["ProdVATPercent"]),
Convert.ToString(dr["ProdCST"]), Convert.ToString(dr["ProdCSTPercent"]), Convert.ToString(dr["ProdGrade"]), Convert.ToString(dr["ProdScheduleDrugCode"]), Convert.ToString(dr["ProdDPCOCode"]), Convert.ToString(dr["ProdIfSchedule"]), Convert.ToString(dr["ProdIfShortListed"]), Convert.ToString(dr["ProdIfSaleDisc"]),
Convert.ToString(dr["ProdIfPurchaseRateInclusive"]), Convert.ToString(dr["ProdIfMRPInclusive"]), Convert.ToString(dr["ProdIfBarCodeRequired"]), Convert.ToString(dr["ProdIfOctroi"]), Convert.ToString(dr["ProdRequireColdStorage"]), Convert.ToString(dr["ProdMinLevel"]), Convert.ToString(dr["ProdMaxLevel"]), Convert.ToString(dr["ProdMargin"]),
Convert.ToString(dr["ProdLastPurchaseBillNumber"]), Convert.ToString(dr["ProdLastPurchaseDate"]), Convert.ToString(dr["ProdLastPurchasePartyId"]), Convert.ToString(dr["ProdLastPurchaseVoucherType"]), Convert.ToString(dr["ProdLastPurchaseVoucherNumber"]), Convert.ToString(dr["ProdLastPurchaseRate"]), Convert.ToString(dr["ProdLastPurchaseTradeRate"]), Convert.ToString(dr["ProdLastPurchaseSaleRate"]),
Convert.ToString(dr["ProdLastPurchasePTR"]), Convert.ToString(dr["ProdLastPurchaseMRP"]), Convert.ToString(dr["ProdLastPurchaseVATPer"]), Convert.ToString(dr["ProdLastPurchaseCSTPer"]), Convert.ToString(dr["ProdLastPurchaseCST"]), Convert.ToString(dr["ProdLastPurchaseSCMPer"]), Convert.ToString(dr["ProdLastPurchaseSCM"]), Convert.ToString(dr["ProdLastPurchaseItemDiscPer"]), Convert.ToString(dr["ProdLastPurchaseLocalTaxPer"]), Convert.ToString(dr["ProdLastPurchaseLocalTaxAmt"]),
Convert.ToString(dr["ProdLastPurchaseExpiry"]), Convert.ToString(dr["ProdLastPurchaseExpiryDate"]), Convert.ToString(dr["ProdLastPurchaseBatchNumber"]), Convert.ToString(dr["ProdLastPurchaseStockID"]), Convert.ToString(dr["ProdOpeningStock"]), Convert.ToString(dr["ProdClosingStock"]), Convert.ToString(dr["ProdUserDefineCode"]), Convert.ToString(dr["ProdSchemeOpeningQty"]), Convert.ToString(dr["ProdSchemePurchaseQty"]), Convert.ToString(dr["ProdSchemeSaleQty"]), Convert.ToString(dr["ProdSchemeCRQty"]),
Convert.ToString(dr["ProdSchemeDBQty"]), Convert.ToString(dr["ProdOctroiPer"]), Convert.ToString(dr["ProdLastSaleBillType"]), Convert.ToString(dr["ProdLastSaleBillNumber"]), Convert.ToString(dr["ProdLastSaleDate"]), Convert.ToString(dr["ProdLastSalePartyId"]), Convert.ToString(dr["ProdLastSaleStockID"]), Convert.ToString(dr["ProdLastSaleScanID"]), Convert.ToString(dr["TAG"]), Convert.ToString(dr["MSCDACode"]), Convert.ToString(dr["SSOpeningStock"]), Convert.ToString(dr["SSPurchaseStock"]), Convert.ToString(dr["SSSaleStock"]),
Convert.ToString(dr["SSCRStock"]), Convert.ToString(dr["SSDRStock"]), Convert.ToString(dr["CreatedDate"]), Convert.ToString(dr["CreatedTime"]), Convert.ToString(dr["CreatedUserID"]), Convert.ToString(dr["ModifiedDate"]), Convert.ToString(dr["ModifiedTime"]), Convert.ToString(dr["ModifiedUserID"]), Convert.ToString(dr["productCode"]), Convert.ToString(dr["companyCode"]),
Convert.ToString(dr["GlobalID"]), Convert.ToString(dr["opstock"]), Convert.ToString(dr["purstock"]), Convert.ToString(dr["salestock"]), Convert.ToString(dr["crstock"]), Convert.ToString(dr["dbstock"]), Convert.ToString(dr["PacktypeId"]), Convert.ToString(dr["ProdCompID"]), Convert.ToString(dr["ProdShelfID"]), Convert.ToString(dr["ProdDrugID"]), Convert.ToString(dr["ProdCategoryID"]), Convert.ToString(dr["ProdLBTID"]), Convert.ToString(dr["ProdPartyId_1"]), Convert.ToString(dr["ProdPartyId_2"]), Convert.ToString(dr["ProdTaxID"]), Convert.ToString(dr["prodmrp"]),
Convert.ToString(dr["HSNNumber"]), Convert.ToString(dr["ScannedBarcode"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetMasterProductInsertQuery(Convert.ToInt32(dr["ProductID"]), Convert.ToString(dr["ProdName"]),
Convert.ToString(dr["ProdLoosePack"]), Convert.ToString(dr["ProdPack"]), Convert.ToString(dr["ProdPackType"]), Convert.ToString(dr["ProdBoxQuantity"]), Convert.ToString(dr["ProdCompShortName"]), Convert.ToString(dr["ProdVATPercent"]),
Convert.ToString(dr["ProdCST"]), Convert.ToString(dr["ProdCSTPercent"]), Convert.ToString(dr["ProdGrade"]), Convert.ToString(dr["ProdScheduleDrugCode"]), Convert.ToString(dr["ProdDPCOCode"]), Convert.ToString(dr["ProdIfSchedule"]), Convert.ToString(dr["ProdIfShortListed"]), Convert.ToString(dr["ProdIfSaleDisc"]),
Convert.ToString(dr["ProdIfPurchaseRateInclusive"]), Convert.ToString(dr["ProdIfMRPInclusive"]), Convert.ToString(dr["ProdIfBarCodeRequired"]), Convert.ToString(dr["ProdIfOctroi"]), Convert.ToString(dr["ProdRequireColdStorage"]), Convert.ToString(dr["ProdMinLevel"]), Convert.ToString(dr["ProdMaxLevel"]), Convert.ToString(dr["ProdMargin"]),
Convert.ToString(dr["ProdLastPurchaseBillNumber"]), Convert.ToString(dr["ProdLastPurchaseDate"]), Convert.ToString(dr["ProdLastPurchasePartyId"]), Convert.ToString(dr["ProdLastPurchaseVoucherType"]), Convert.ToString(dr["ProdLastPurchaseVoucherNumber"]), Convert.ToString(dr["ProdLastPurchaseRate"]), Convert.ToString(dr["ProdLastPurchaseTradeRate"]), Convert.ToString(dr["ProdLastPurchaseSaleRate"]),
Convert.ToString(dr["ProdLastPurchasePTR"]), Convert.ToString(dr["ProdLastPurchaseMRP"]), Convert.ToString(dr["ProdLastPurchaseVATPer"]), Convert.ToString(dr["ProdLastPurchaseCSTPer"]), Convert.ToString(dr["ProdLastPurchaseCST"]), Convert.ToString(dr["ProdLastPurchaseSCMPer"]), Convert.ToString(dr["ProdLastPurchaseSCM"]), Convert.ToString(dr["ProdLastPurchaseItemDiscPer"]), Convert.ToString(dr["ProdLastPurchaseLocalTaxPer"]), Convert.ToString(dr["ProdLastPurchaseLocalTaxAmt"]),
Convert.ToString(dr["ProdLastPurchaseExpiry"]), Convert.ToString(dr["ProdLastPurchaseExpiryDate"]), Convert.ToString(dr["ProdLastPurchaseBatchNumber"]), Convert.ToString(dr["ProdLastPurchaseStockID"]), Convert.ToString(dr["ProdOpeningStock"]), Convert.ToString(dr["ProdClosingStock"]), Convert.ToString(dr["ProdUserDefineCode"]), Convert.ToString(dr["ProdSchemeOpeningQty"]), Convert.ToString(dr["ProdSchemePurchaseQty"]), Convert.ToString(dr["ProdSchemeSaleQty"]), Convert.ToString(dr["ProdSchemeCRQty"]),
Convert.ToString(dr["ProdSchemeDBQty"]), Convert.ToString(dr["ProdOctroiPer"]), Convert.ToString(dr["ProdLastSaleBillType"]), Convert.ToString(dr["ProdLastSaleBillNumber"]), Convert.ToString(dr["ProdLastSaleDate"]), Convert.ToString(dr["ProdLastSalePartyId"]), Convert.ToString(dr["ProdLastSaleStockID"]), Convert.ToString(dr["ProdLastSaleScanID"]), Convert.ToString(dr["TAG"]), Convert.ToString(dr["MSCDACode"]), Convert.ToString(dr["SSOpeningStock"]), Convert.ToString(dr["SSPurchaseStock"]), Convert.ToString(dr["SSSaleStock"]),
Convert.ToString(dr["SSCRStock"]), Convert.ToString(dr["SSDRStock"]), Convert.ToString(dr["CreatedDate"]), Convert.ToString(dr["CreatedTime"]), Convert.ToString(dr["CreatedUserID"]), Convert.ToString(dr["ModifiedDate"]), Convert.ToString(dr["ModifiedTime"]), Convert.ToString(dr["ModifiedUserID"]), Convert.ToString(dr["productCode"]), Convert.ToString(dr["companyCode"]),
Convert.ToString(dr["GlobalID"]), Convert.ToString(dr["opstock"]), Convert.ToString(dr["purstock"]), Convert.ToString(dr["salestock"]), Convert.ToString(dr["crstock"]), Convert.ToString(dr["dbstock"]), Convert.ToString(dr["PacktypeId"]), Convert.ToString(dr["ProdCompID"]), Convert.ToString(dr["ProdShelfID"]), Convert.ToString(dr["ProdDrugID"]), Convert.ToString(dr["ProdCategoryID"]), Convert.ToString(dr["ProdLBTID"]), Convert.ToString(dr["ProdPartyId_1"]), Convert.ToString(dr["ProdPartyId_2"]), Convert.ToString(dr["ProdTaxID"]), Convert.ToString(dr["prodmrp"]),
Convert.ToString(dr["HSNNumber"]), Convert.ToString(dr["ScannedBarcode"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }


        public string GetMasterVATDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,VATPercentage from tblvat";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select ID,VATPercentage from tblvat";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["ID"]), "tblvat", "ID"))
                    {
                        string strSqlUpdate = GetUpdateQueryMasterVAT(Convert.ToInt32(dr["ID"]), Convert.ToString(dr["VATPercentage"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetInsertQueryMasterVAT(Convert.ToInt32(dr["ID"]), Convert.ToString(dr["VATPercentage"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }
        public string GetMasterGenericCategoryDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GenericCategoryID,GenericCategoryName,DrugCode from mastergenericcategory";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select GenericCategoryID,GenericCategoryName,DrugCode from mastergenericcategory";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["GenericCategoryID"]), "mastergenericcategory", "GenericCategoryID"))
                    {
                        string strSqlUpdate = GetUpdateQueryMasterGenericCategory(Convert.ToInt32(dr["GenericCategoryID"]), Convert.ToString(dr["GenericCategoryName"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetInsertQueryMasterGenericCategory(Convert.ToInt32(dr["GenericCategoryID"]), Convert.ToString(dr["GenericCategoryName"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }
        public string GetMasterProductCategoryDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ProductCategoryId,ProductCategoryName from masterproductcategory";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select ProductCategoryId,ProductCategoryName from masterproductcategory";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["ProductCategoryId"]), "masterproductcategory", "ProductCategoryId"))
                    {
                        string strSqlUpdate = GetUpdateQueryMasterProductCategory(Convert.ToInt32(dr["ProductCategoryId"]), Convert.ToString(dr["ProductCategoryName"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetInsertQueryMasterProductCategory(Convert.ToInt32(dr["ProductCategoryId"]), Convert.ToString(dr["ProductCategoryName"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }

        public string GetMasterProductPackTypeDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,PackTypeName from masterpacktype";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select ID,PackTypeName from masterpacktype";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["ID"]), "masterpacktype", "ID"))
                    {
                        string strSqlUpdate = GetUpdateQueryMasterpackType(Convert.ToInt32(dr["ID"]), Convert.ToString(dr["PackTypeName"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetInsertQueryMasterpackType(Convert.ToInt32(dr["ID"]), Convert.ToString(dr["PackTypeName"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }

        public string GetMasterProductPackDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select PackId,PackName from Masterpack";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select PackId,PackName  from Masterpack";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["PackId"]), "Masterpack", "PackId"))
                    {
                        string strSqlUpdate = GetUpdateQueryMasterpack(Convert.ToInt32(dr["PackId"]), Convert.ToString(dr["PackName"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetInsertQueryMasterpack(Convert.ToInt32(dr["PackId"]), Convert.ToString(dr["PackName"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }
        public string GetMasterCompanyDataToSync()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from mastercompany";
            dtable = DBInterface.SelectDataTable(strSql);

            DataTable dtableAzure = new DataTable();
            string strSqlAzure = "Select * from mastercompany";
            dtableAzure = AzureDBInterface.SelectDataTable(strSqlAzure);

            DataTable dtSync = null;
            var rows = dtableAzure.AsEnumerable().Except(dtable.AsEnumerable(), DataRowComparer.Default);

            if (rows.Any())
            {
                dtSync = rows.CopyToDataTable();
                foreach (DataRow dr in dtSync.Rows)
                {
                    if (ReadDetailsByID(Convert.ToString(dr["CompID"]), "mastercompany", "CompId"))
                    {
                        string strSqlUpdate = GetUpdateQuery(Convert.ToInt32(dr["CompID"]), Convert.ToString(dr["CompName"]), Convert.ToString(dr["CompShortName"]), Convert.ToString(dr["CompTelephone"]), Convert.ToString(dr["CompMailId"]),
             Convert.ToString(dr["CompContactPerson"]), Convert.ToString(dr["CompAddress"]), Convert.ToString(dr["Address2"]), Convert.ToString(dr["PartyID_1"]), Convert.ToString(dr["PartyID_2"]), Convert.ToString(dr["PartyID_3"]), Convert.ToString(dr["PartyID_4"]),
             Convert.ToString(dr["CreatedDate"]), Convert.ToString(dr["CreatedTime"]), Convert.ToString(dr["CreatedUserID"]), Convert.ToString(dr["ModifiedDate"]), Convert.ToString(dr["ModifiedTime"]),
             Convert.ToString(dr["ModifiedUserID"]), Convert.ToString(dr["Companycode"]), Convert.ToString(dr["GlobalID"]), Convert.ToString(dr["Tag"]), Convert.ToString(dr["MaincompID"]));

                        DBInterface.ExecuteScalar(strSqlUpdate);
                    }
                    else
                    {

                        string strSqlInsert = GetInsertQuery(Convert.ToInt32(dr["CompID"]), Convert.ToString(dr["CompName"]), Convert.ToString(dr["CompShortName"]), Convert.ToString(dr["CompTelephone"]), Convert.ToString(dr["CompMailId"]),
                Convert.ToString(dr["CompContactPerson"]), Convert.ToString(dr["CompAddress"]), Convert.ToString(dr["Address2"]), Convert.ToString(dr["PartyID_1"]), Convert.ToString(dr["PartyID_2"]), Convert.ToString(dr["PartyID_3"]), Convert.ToString(dr["PartyID_4"]),
                Convert.ToString(dr["CreatedDate"]), Convert.ToString(dr["CreatedTime"]), Convert.ToString(dr["CreatedUserID"]), Convert.ToString(dr["ModifiedDate"]), Convert.ToString(dr["ModifiedTime"]),
                Convert.ToString(dr["ModifiedUserID"]), Convert.ToString(dr["Companycode"]), Convert.ToString(dr["GlobalID"]), Convert.ToString(dr["Tag"]), Convert.ToString(dr["MaincompID"]));

                        DBInterface.ExecuteScalar(strSqlInsert);
                    }

                }
            }

            return "";
        }
        public bool ReadDetailsByID(string Id, string tableName, string where)
        {
            bool exists = false;
            DataTable dTable = null;
            if (Id != "")
            {
                string strSql = "Select * from " + tableName + " where " + where + " ='{0}' ";
                strSql = string.Format(strSql, Id);
                dTable = DBInterface.SelectDataTable(strSql);
                if (dTable.Rows.Count > 0)
                    exists = true;
            }
            return exists;
        }



        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataTable GetAreaList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT AreaId, AreaName FROM MasterArea";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool IsNameUniqueForAdd(string Name, string Id)
        {
            int ifdup = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (ifdup > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Id)
        {
            int ifdup = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (ifdup > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(areaid) as maxid from MasterArea ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        #region Query Building Functions

        private int GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            DataRow dRow = null;
            string strSql = "Select AreaId from masterarea where AreaName= '" + Name + "'";
            dRow = DBInterface.SelectFirstRow(strSql);
            if (dRow == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AreaId from masterarea where AreaName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AreaId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        private string GetInsertQuery(int CompID, string CompName, string CompShortName, string CompTelephone, string CompMailId,
            string CompContactPerson, string CompAddress, string Address2, string PartyID_1, string PartyID_2, string PartyID_3, string PartyID_4,
            string CreatedDate, string CreatedTime, string CreatedUserID, string ModifiedDate, string ModifiedTime,
            string ModifiedUserID, string Companycode, string GlobalID, string Tag, string MaincompID)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastercompany";
            objQuery.AddToQuery("CompID", CompID);
            objQuery.AddToQuery("CompName", CompName);
            objQuery.AddToQuery("CompShortName", CompShortName);
            objQuery.AddToQuery("CompTelephone", CompTelephone);


            objQuery.AddToQuery("CompMailId", CompMailId);
            objQuery.AddToQuery("CompContactPerson", CompContactPerson);
            objQuery.AddToQuery("CompAddress", CompAddress);
            objQuery.AddToQuery("PartyID_1", PartyID_1);
            objQuery.AddToQuery("PartyID_2", PartyID_2);
            objQuery.AddToQuery("PartyID_3", PartyID_3);
            objQuery.AddToQuery("PartyID_4", PartyID_4);

            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            objQuery.AddToQuery("ModifiedTime", ModifiedTime);

            objQuery.AddToQuery("ModifiedUserID", ModifiedUserID);
            objQuery.AddToQuery("Companycode", Companycode);
            objQuery.AddToQuery("GlobalID", GlobalID);
            objQuery.AddToQuery("Tag", Tag);
            objQuery.AddToQuery("MaincompID", MaincompID);


            return objQuery.InsertQuery(true);
        }


        private string GetMasterProductInsertQuery(int ProductID, string ProdName, string ProdLoosePack, string ProdPack, string ProdPackType, string ProdBoxQuantity, string ProdCompShortName, string ProdVATPercent, string ProdCST, string ProdCSTPercent,
string ProdGrade, string ProdScheduleDrugCode, string ProdDPCOCode, string ProdIfSchedule, string ProdIfShortListed, string ProdIfSaleDisc, string ProdIfPurchaseRateInclusive, string ProdIfMRPInclusive, string ProdIfBarCodeRequired, string ProdIfOctroi, string ProdRequireColdStorage,
string ProdMinLevel, string ProdMaxLevel, string ProdMargin, string ProdLastPurchaseBillNumber, string ProdLastPurchaseDate, string ProdLastPurchasePartyId, string ProdLastPurchaseVoucherType, string ProdLastPurchaseVoucherNumber, string ProdLastPurchaseRate, string ProdLastPurchaseTradeRate, string ProdLastPurchaseSaleRate, string ProdLastPurchasePTR,
string ProdLastPurchaseMRP, string ProdLastPurchaseVATPer, string ProdLastPurchaseCSTPer, string ProdLastPurchaseCST, string ProdLastPurchaseSCMPer, string ProdLastPurchaseSCM, string ProdLastPurchaseItemDiscPer, string ProdLastPurchaseLocalTaxPer, string ProdLastPurchaseLocalTaxAmt, string ProdLastPurchaseExpiry, string ProdLastPurchaseExpiryDate,
string ProdLastPurchaseBatchNumber, string ProdLastPurchaseStockID, string ProdOpeningStock, string ProdClosingStock, string ProdUserDefineCode, string ProdSchemeOpeningQty, string ProdSchemePurchaseQty, string ProdSchemeSaleQty, string ProdSchemeCRQty, string ProdSchemeDBQty,
string ProdOctroiPer, string ProdLastSaleBillType, string ProdLastSaleBillNumber, string ProdLastSaleDate, string ProdLastSalePartyId, string ProdLastSaleStockID, string ProdLastSaleScanID, string TAG, string MSCDACode, string SSOpeningStock, string SSPurchaseStock, string SSSaleStock,
string SSCRStock, string SSDRStock, string CreatedDate, string CreatedTime, string CreatedUserID, string ModifiedDate, string ModifiedTime,
string ModifiedUserID, string productCode, string companyCode, string GlobalID, string opstock, string purstock, string salestock, string crstock, string dbstock,
string PacktypeId, string ProdCompID, string ProdShelfID, string ProdDrugID, string ProdCategoryID, string ProdLBTID,
string ProdPartyId_1, string ProdPartyId_2, string ProdTaxID, string prodmrp, string HSNNumber, string ScannedBarcode)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("ProdName", ProdName);
            //objQuery.AddToQuery("ProdLoosePack", ProdLoosePack);
            objQuery.AddToQuery("ProdPack", ProdPack);
            objQuery.AddToQuery("ProdPackType", ProdPackType);
            objQuery.AddToQuery("ProdBoxQuantity", ProdBoxQuantity);
            objQuery.AddToQuery("ProdCompShortName", ProdCompShortName);
            objQuery.AddToQuery("ProdVATPercent", ProdVATPercent);
            //objQuery.AddToQuery("ProdCST", ProdCST);
            //objQuery.AddToQuery("ProdCSTPercent", ProdCSTPercent);
            //objQuery.AddToQuery("ProdGrade", ProdGrade);
            //objQuery.AddToQuery("ProdScheduleDrugCode", ProdScheduleDrugCode);
            //objQuery.AddToQuery("ProdDPCOCode", ProdDPCOCode);
            //objQuery.AddToQuery("ProdIfSchedule", ProdIfSchedule);
            //objQuery.AddToQuery("ProdIfShortListed", ProdIfShortListed);
            //objQuery.AddToQuery("ProdIfSaleDisc", ProdIfSaleDisc);
            //objQuery.AddToQuery("ProdIfPurchaseRateInclusive", ProdIfPurchaseRateInclusive);
            //objQuery.AddToQuery("ProdIfMRPInclusive", ProdIfMRPInclusive);
            //objQuery.AddToQuery("ProdIfBarCodeRequired", ProdIfBarCodeRequired);
            //objQuery.AddToQuery("ProdIfOctroi", ProdIfOctroi);
            //objQuery.AddToQuery("ProdRequireColdStorage", ProdRequireColdStorage);
            //objQuery.AddToQuery("ProdMinLevel", ProdMinLevel);
            //objQuery.AddToQuery("ProdMaxLevel", ProdMaxLevel);
            //objQuery.AddToQuery("ProdMargin", ProdMargin);
            //objQuery.AddToQuery("ProdLastPurchaseBillNumber", ProdLastPurchaseBillNumber);
            //objQuery.AddToQuery("ProdLastPurchaseDate", ProdLastPurchaseDate);
            //objQuery.AddToQuery("ProdLastPurchasePartyId", ProdLastPurchasePartyId);
            //objQuery.AddToQuery("ProdLastPurchaseVoucherType", ProdLastPurchaseVoucherType);
            //objQuery.AddToQuery("ProdLastPurchaseVoucherNumber", ProdLastPurchaseVoucherNumber);
            //objQuery.AddToQuery("ProdLastPurchaseRate", ProdLastPurchaseRate);
            //objQuery.AddToQuery("ProdLastPurchaseTradeRate", ProdLastPurchaseTradeRate);
            //objQuery.AddToQuery("ProdLastPurchaseSaleRate", ProdLastPurchaseSaleRate);
            //objQuery.AddToQuery("ProdLastPurchasePTR", ProdLastPurchasePTR);
            //objQuery.AddToQuery("ProdLastPurchaseMRP", ProdLastPurchaseMRP);
            //objQuery.AddToQuery("ProdLastPurchaseVATPer", ProdLastPurchaseVATPer);
            //objQuery.AddToQuery("ProdLastPurchaseCSTPer", ProdLastPurchaseCSTPer);
            //objQuery.AddToQuery("ProdLastPurchaseCST", ProdLastPurchaseCST);
            //objQuery.AddToQuery("ProdLastPurchaseSCMPer", ProdLastPurchaseSCMPer);
            //objQuery.AddToQuery("ProdLastPurchaseSCM", ProdLastPurchaseSCM);
            //objQuery.AddToQuery("ProdLastPurchaseItemDiscPer", ProdLastPurchaseItemDiscPer);
            //objQuery.AddToQuery("ProdLastPurchaseLocalTaxPer", ProdLastPurchaseLocalTaxPer);
            //objQuery.AddToQuery("ProdLastPurchaseLocalTaxAmt", ProdLastPurchaseLocalTaxAmt);
            //objQuery.AddToQuery("ProdLastPurchaseExpiry", ProdLastPurchaseExpiry);
            //objQuery.AddToQuery("ProdLastPurchaseExpiryDate", ProdLastPurchaseExpiryDate);
            //objQuery.AddToQuery("ProdLastPurchaseBatchNumber", ProdLastPurchaseBatchNumber);
            //objQuery.AddToQuery("ProdLastPurchaseStockID", ProdLastPurchaseStockID);
            //objQuery.AddToQuery("ProdOpeningStock", ProdOpeningStock);
            //objQuery.AddToQuery("ProdClosingStock", ProdClosingStock);
            //objQuery.AddToQuery("ProdUserDefineCode", ProdUserDefineCode);
            //objQuery.AddToQuery("ProdSchemeOpeningQty", ProdSchemeOpeningQty);
            //objQuery.AddToQuery("ProdSchemePurchaseQty", ProdSchemePurchaseQty);
            //objQuery.AddToQuery("ProdSchemeSaleQty", ProdSchemeSaleQty);
            //objQuery.AddToQuery("ProdSchemeCRQty", ProdSchemeCRQty);
            //objQuery.AddToQuery("ProdSchemeDBQty", ProdSchemeDBQty);
            //objQuery.AddToQuery("ProdOctroiPer", ProdOctroiPer);
            //objQuery.AddToQuery("ProdLastSaleBillType", ProdLastSaleBillType);
            //objQuery.AddToQuery("ProdLastSaleBillNumber", ProdLastSaleBillNumber);
            //objQuery.AddToQuery("ProdLastSaleDate", ProdLastSaleDate);
            //objQuery.AddToQuery("ProdLastSalePartyId", ProdLastSalePartyId);
            //objQuery.AddToQuery("ProdLastSaleStockID", ProdLastSaleStockID);
            //objQuery.AddToQuery("ProdLastSaleScanID", ProdLastSaleScanID);
            //objQuery.AddToQuery("TAG", TAG);
            //objQuery.AddToQuery("MSCDACode", MSCDACode);
            //objQuery.AddToQuery("SSOpeningStock", SSOpeningStock);
            //objQuery.AddToQuery("SSPurchaseStock", SSPurchaseStock);
            //objQuery.AddToQuery("SSSaleStock", SSSaleStock);
            //objQuery.AddToQuery("SSCRStock", SSCRStock);
            //objQuery.AddToQuery("SSDRStock", SSDRStock);
            //objQuery.AddToQuery("CreatedDate", CreatedDate);
            //objQuery.AddToQuery("CreatedTime", CreatedTime);
            //objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            //objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            //objQuery.AddToQuery("ModifiedTime", ModifiedTime);
            //objQuery.AddToQuery("ModifiedUserID", ModifiedUserID);
            //objQuery.AddToQuery("productCode", productCode);
            //objQuery.AddToQuery("companyCode", companyCode);
            //objQuery.AddToQuery("GlobalID", GlobalID);
            //objQuery.AddToQuery("opstock", opstock);
            //objQuery.AddToQuery("purstock", purstock);
            //objQuery.AddToQuery("salestock", salestock);
            //objQuery.AddToQuery("crstock", crstock);
            //objQuery.AddToQuery("dbstock", dbstock);
            //objQuery.AddToQuery("PacktypeId", PacktypeId);
            objQuery.AddToQuery("ProdCompID", ProdCompID);
            //objQuery.AddToQuery("ProdShelfID", ProdShelfID);
            //objQuery.AddToQuery("ProdDrugID", ProdDrugID);
            objQuery.AddToQuery("ProdCategoryID", ProdCategoryID);
            //objQuery.AddToQuery("ProdLBTID", ProdLBTID);
            //objQuery.AddToQuery("ProdPartyId_1", ProdPartyId_1);
            //objQuery.AddToQuery("ProdPartyId_2", ProdPartyId_2);
            //objQuery.AddToQuery("ProdTaxID", ProdTaxID);
            //objQuery.AddToQuery("prodmrp", prodmrp);
            objQuery.AddToQuery("HSNNumber", HSNNumber);
            //objQuery.AddToQuery("ScannedBarcode", ScannedBarcode);
            return objQuery.InsertQuery(true);
        }

        private string GetMasterProductUpdateQuery(int ProductID, string ProdName, string ProdLoosePack, string ProdPack, string ProdPackType, string ProdBoxQuantity, string ProdCompShortName, string ProdVATPercent, string ProdCST, string ProdCSTPercent,
string ProdGrade, string ProdScheduleDrugCode, string ProdDPCOCode, string ProdIfSchedule, string ProdIfShortListed, string ProdIfSaleDisc, string ProdIfPurchaseRateInclusive, string ProdIfMRPInclusive, string ProdIfBarCodeRequired, string ProdIfOctroi, string ProdRequireColdStorage,
string ProdMinLevel, string ProdMaxLevel, string ProdMargin, string ProdLastPurchaseBillNumber, string ProdLastPurchaseDate, string ProdLastPurchasePartyId, string ProdLastPurchaseVoucherType, string ProdLastPurchaseVoucherNumber, string ProdLastPurchaseRate, string ProdLastPurchaseTradeRate, string ProdLastPurchaseSaleRate, string ProdLastPurchasePTR,
string ProdLastPurchaseMRP, string ProdLastPurchaseVATPer, string ProdLastPurchaseCSTPer, string ProdLastPurchaseCST, string ProdLastPurchaseSCMPer, string ProdLastPurchaseSCM, string ProdLastPurchaseItemDiscPer, string ProdLastPurchaseLocalTaxPer, string ProdLastPurchaseLocalTaxAmt, string ProdLastPurchaseExpiry, string ProdLastPurchaseExpiryDate,
string ProdLastPurchaseBatchNumber, string ProdLastPurchaseStockID, string ProdOpeningStock, string ProdClosingStock, string ProdUserDefineCode, string ProdSchemeOpeningQty, string ProdSchemePurchaseQty, string ProdSchemeSaleQty, string ProdSchemeCRQty, string ProdSchemeDBQty,
string ProdOctroiPer, string ProdLastSaleBillType, string ProdLastSaleBillNumber, string ProdLastSaleDate, string ProdLastSalePartyId, string ProdLastSaleStockID, string ProdLastSaleScanID, string TAG, string MSCDACode, string SSOpeningStock, string SSPurchaseStock, string SSSaleStock,
string SSCRStock, string SSDRStock, string CreatedDate, string CreatedTime, string CreatedUserID, string ModifiedDate, string ModifiedTime,
string ModifiedUserID, string productCode, string companyCode, string GlobalID, string opstock, string purstock, string salestock, string crstock, string dbstock,
string PacktypeId, string ProdCompID, string ProdShelfID, string ProdDrugID, string ProdCategoryID, string ProdLBTID,
string ProdPartyId_1, string ProdPartyId_2, string ProdTaxID, string prodmrp, string HSNNumber, string ScannedBarcode)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", ProductID, true);
            objQuery.AddToQuery("ProdName", ProdName);
            //objQuery.AddToQuery("ProdLoosePack", ProdLoosePack);
            objQuery.AddToQuery("ProdPack", ProdPack);
            objQuery.AddToQuery("ProdPackType", ProdPackType);
            objQuery.AddToQuery("ProdBoxQuantity", ProdBoxQuantity);
            objQuery.AddToQuery("ProdCompShortName", ProdCompShortName);
            objQuery.AddToQuery("ProdVATPercent", ProdVATPercent);
            objQuery.AddToQuery("ProdCST", ProdCST);
            objQuery.AddToQuery("ProdCSTPercent", ProdCSTPercent);
            objQuery.AddToQuery("ProdGrade", ProdGrade);
            //objQuery.AddToQuery("ProdScheduleDrugCode", ProdScheduleDrugCode);
            //objQuery.AddToQuery("ProdDPCOCode", ProdDPCOCode);
            //objQuery.AddToQuery("ProdIfSchedule", ProdIfSchedule);
            //objQuery.AddToQuery("ProdIfShortListed", ProdIfShortListed);
            //objQuery.AddToQuery("ProdIfSaleDisc", ProdIfSaleDisc);
            //objQuery.AddToQuery("ProdIfPurchaseRateInclusive", ProdIfPurchaseRateInclusive);
            //objQuery.AddToQuery("ProdIfMRPInclusive", ProdIfMRPInclusive);
            //objQuery.AddToQuery("ProdIfBarCodeRequired", ProdIfBarCodeRequired);
            //objQuery.AddToQuery("ProdIfOctroi", ProdIfOctroi);
            //objQuery.AddToQuery("ProdRequireColdStorage", ProdRequireColdStorage);
            //objQuery.AddToQuery("ProdMinLevel", ProdMinLevel);
            //objQuery.AddToQuery("ProdMaxLevel", ProdMaxLevel);
            //objQuery.AddToQuery("ProdMargin", ProdMargin);
            //objQuery.AddToQuery("ProdLastPurchaseBillNumber", ProdLastPurchaseBillNumber);
            //objQuery.AddToQuery("ProdLastPurchaseDate", ProdLastPurchaseDate);
            //objQuery.AddToQuery("ProdLastPurchasePartyId", ProdLastPurchasePartyId);
            //objQuery.AddToQuery("ProdLastPurchaseVoucherType", ProdLastPurchaseVoucherType);
            //objQuery.AddToQuery("ProdLastPurchaseVoucherNumber", ProdLastPurchaseVoucherNumber);
            //objQuery.AddToQuery("ProdLastPurchaseRate", ProdLastPurchaseRate);
            //objQuery.AddToQuery("ProdLastPurchaseTradeRate", ProdLastPurchaseTradeRate);
            //objQuery.AddToQuery("ProdLastPurchaseSaleRate", ProdLastPurchaseSaleRate);
            //objQuery.AddToQuery("ProdLastPurchasePTR", ProdLastPurchasePTR);
            //objQuery.AddToQuery("ProdLastPurchaseMRP", ProdLastPurchaseMRP);
            //objQuery.AddToQuery("ProdLastPurchaseVATPer", ProdLastPurchaseVATPer);
            //objQuery.AddToQuery("ProdLastPurchaseCSTPer", ProdLastPurchaseCSTPer);
            //objQuery.AddToQuery("ProdLastPurchaseCST", ProdLastPurchaseCST);
            //objQuery.AddToQuery("ProdLastPurchaseSCMPer", ProdLastPurchaseSCMPer);
            //objQuery.AddToQuery("ProdLastPurchaseSCM", ProdLastPurchaseSCM);
            //objQuery.AddToQuery("ProdLastPurchaseItemDiscPer", ProdLastPurchaseItemDiscPer);
            //objQuery.AddToQuery("ProdLastPurchaseLocalTaxPer", ProdLastPurchaseLocalTaxPer);
            //objQuery.AddToQuery("ProdLastPurchaseLocalTaxAmt", ProdLastPurchaseLocalTaxAmt);
            //objQuery.AddToQuery("ProdLastPurchaseExpiry", ProdLastPurchaseExpiry);
            //objQuery.AddToQuery("ProdLastPurchaseExpiryDate", ProdLastPurchaseExpiryDate);
            //objQuery.AddToQuery("ProdLastPurchaseBatchNumber", ProdLastPurchaseBatchNumber);
            //objQuery.AddToQuery("ProdLastPurchaseStockID", ProdLastPurchaseStockID);
            //objQuery.AddToQuery("ProdOpeningStock", ProdOpeningStock);
            //objQuery.AddToQuery("ProdClosingStock", ProdClosingStock);
            //objQuery.AddToQuery("ProdUserDefineCode", ProdUserDefineCode);
            //objQuery.AddToQuery("ProdSchemeOpeningQty", ProdSchemeOpeningQty);
            //objQuery.AddToQuery("ProdSchemePurchaseQty", ProdSchemePurchaseQty);
            //objQuery.AddToQuery("ProdSchemeSaleQty", ProdSchemeSaleQty);
            //objQuery.AddToQuery("ProdSchemeCRQty", ProdSchemeCRQty);
            //objQuery.AddToQuery("ProdSchemeDBQty", ProdSchemeDBQty);
            //objQuery.AddToQuery("ProdOctroiPer", ProdOctroiPer);
            //objQuery.AddToQuery("ProdLastSaleBillType", ProdLastSaleBillType);
            //objQuery.AddToQuery("ProdLastSaleBillNumber", ProdLastSaleBillNumber);
            //objQuery.AddToQuery("ProdLastSaleDate", ProdLastSaleDate);
            //objQuery.AddToQuery("ProdLastSalePartyId", ProdLastSalePartyId);
            //objQuery.AddToQuery("ProdLastSaleStockID", ProdLastSaleStockID);
            //objQuery.AddToQuery("ProdLastSaleScanID", ProdLastSaleScanID);
            //objQuery.AddToQuery("TAG", TAG);
            //objQuery.AddToQuery("MSCDACode", MSCDACode);
            //objQuery.AddToQuery("SSOpeningStock", SSOpeningStock);
            //objQuery.AddToQuery("SSPurchaseStock", SSPurchaseStock);
            //objQuery.AddToQuery("SSSaleStock", SSSaleStock);
            //objQuery.AddToQuery("SSCRStock", SSCRStock);
            //objQuery.AddToQuery("SSDRStock", SSDRStock);
            //objQuery.AddToQuery("CreatedDate", CreatedDate);
            //objQuery.AddToQuery("CreatedTime", CreatedTime);
            //objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            //objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            //objQuery.AddToQuery("ModifiedTime", ModifiedTime);
            //objQuery.AddToQuery("ModifiedUserID", ModifiedUserID);
            objQuery.AddToQuery("productCode", productCode);
            objQuery.AddToQuery("companyCode", companyCode);
            //objQuery.AddToQuery("GlobalID", GlobalID);
            //objQuery.AddToQuery("opstock", opstock);
            //objQuery.AddToQuery("purstock", purstock);
            //objQuery.AddToQuery("salestock", salestock);
            //objQuery.AddToQuery("crstock", crstock);
            //objQuery.AddToQuery("dbstock", dbstock);
            //objQuery.AddToQuery("PacktypeId", PacktypeId);
            objQuery.AddToQuery("ProdCompID", ProdCompID);
            //objQuery.AddToQuery("ProdShelfID", ProdShelfID);
            objQuery.AddToQuery("ProdDrugID", ProdDrugID);
            objQuery.AddToQuery("ProdCategoryID", ProdCategoryID);
            //objQuery.AddToQuery("ProdLBTID", ProdLBTID);
            //objQuery.AddToQuery("ProdPartyId_1", ProdPartyId_1);
            //objQuery.AddToQuery("ProdPartyId_2", ProdPartyId_2);
            //objQuery.AddToQuery("ProdTaxID", ProdTaxID);
            //objQuery.AddToQuery("prodmrp", prodmrp);
            objQuery.AddToQuery("HSNNumber", HSNNumber);
            //objQuery.AddToQuery("ScannedBarcode", ScannedBarcode);

            return objQuery.UpdateQuery();
        }

        private string GetUpdateQueryMasterpackType(int ID, string PackTypeName)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpacktype";
            objQuery.AddToQuery("ID", ID, true);
            objQuery.AddToQuery("PackTypeName", PackTypeName);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQueryMasterpackType(int ID, string PackTypeName)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpacktype";
            objQuery.AddToQuery("ID", ID);
            objQuery.AddToQuery("PackTypeName", PackTypeName);
            return objQuery.InsertQuery(true);
        }

        private string GetUpdateQueryMasterpack(int PackId, string PackName)
        {
            Query objQuery = new Query();
            objQuery.Table = "Masterpack";
            objQuery.AddToQuery("PackId", PackId, true);
            objQuery.AddToQuery("PackName", PackName);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQueryMasterpack(int PackId, string PackName)
        {
            Query objQuery = new Query();
            objQuery.Table = "Masterpack";
            objQuery.AddToQuery("PackId", PackId);
            objQuery.AddToQuery("PackName", PackName);
            return objQuery.InsertQuery(true);
        }

        private string GetUpdateQueryMasterVAT(int ID, string VATPercentage)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblvat";
            objQuery.AddToQuery("ID", ID, true);
            objQuery.AddToQuery("VATPercentage", VATPercentage);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQueryMasterVAT(int ID, string VATPercentage)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblvat";
            objQuery.AddToQuery("ID", ID);
            objQuery.AddToQuery("VATPercentage", VATPercentage);
            return objQuery.InsertQuery(true);
        }

        private string GetUpdateQueryMasterGenericCategory(int GenericCategoryID, string GenericCategoryName)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastergenericcategory";
            objQuery.AddToQuery("GenericCategoryID", GenericCategoryID, true);
            objQuery.AddToQuery("GenericCategoryName", GenericCategoryName);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQueryMasterGenericCategory(int GenericCategoryID, string GenericCategoryName)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastergenericcategory";
            objQuery.AddToQuery("GenericCategoryID", GenericCategoryID);
            objQuery.AddToQuery("GenericCategoryName", GenericCategoryName);
            return objQuery.InsertQuery(true);
        }


        private string GetUpdateQueryMasterProductCategory(int ProductCategoryId, string ProductCategoryName)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproductcategory";
            objQuery.AddToQuery("ProductCategoryId", ProductCategoryId, true);
            objQuery.AddToQuery("ProductCategoryName", ProductCategoryName);
            return objQuery.UpdateQuery();
        }


        private string GetInsertQueryMasterProductCategory(int ProductCategoryId, string ProductCategoryName)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproductcategory";
            objQuery.AddToQuery("ProductCategoryId", ProductCategoryId);
            objQuery.AddToQuery("ProductCategoryName", ProductCategoryName);
            return objQuery.InsertQuery(true);
        }


        private string GetUpdateQuery(int CompID, string CompName, string CompShortName, string CompTelephone, string CompMailId,
            string CompContactPerson, string CompAddress, string Address2, string PartyID_1, string PartyID_2, string PartyID_3, string PartyID_4,
            string CreatedDate, string CreatedTime, string CreatedUserID, string ModifiedDate, string ModifiedTime,
            string ModifiedUserID, string Companycode, string GlobalID, string Tag, string MaincompID)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastercompany";
            objQuery.AddToQuery("CompID", CompID, true);
            objQuery.AddToQuery("CompName", CompName);
            objQuery.AddToQuery("CompShortName", CompShortName);
            objQuery.AddToQuery("CompTelephone", CompTelephone);


            objQuery.AddToQuery("CompMailId", CompMailId);
            objQuery.AddToQuery("CompContactPerson", CompContactPerson);
            objQuery.AddToQuery("CompAddress", CompAddress);
            objQuery.AddToQuery("PartyID_1", PartyID_1);
            objQuery.AddToQuery("PartyID_2", PartyID_2);
            objQuery.AddToQuery("PartyID_3", PartyID_3);
            objQuery.AddToQuery("PartyID_4", PartyID_4);

            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            objQuery.AddToQuery("ModifiedTime", ModifiedTime);

            objQuery.AddToQuery("ModifiedUserID", ModifiedUserID);
            objQuery.AddToQuery("Companycode", Companycode);
            objQuery.AddToQuery("GlobalID", GlobalID);
            objQuery.AddToQuery("Tag", Tag);
            objQuery.AddToQuery("MaincompID", MaincompID);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterArea";
            objQuery.AddToQuery("AreaId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion private methods



        public DataTable GetOverViewDataForAddress()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AreaId,AreaName from MasterArea union ( select '' as AreaId ,PatientAddress1 as AreaName from vouchersale where PatientAddress1 is not null && PatientAddress1 != '') union ( select '' as AreaId ,DoctorAddress as AreaName from vouchersale where DoctorAddress is not null && DoctorAddress != '') order by AreaName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}


