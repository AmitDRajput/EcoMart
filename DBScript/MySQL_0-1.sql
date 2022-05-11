# HeidiSQL Dump 
#
# --------------------------------------------------------
# Host:                         localhost
# Database:                     EcoMart1415
# Server version:               5.6.21-log
# Server OS:                    Win32
# Target compatibility:         Same as source (5.6.21)
# Target max_allowed_packet:    4194304
# HeidiSQL version:             4.0
# Date/time:                    2015-08-15 21:07:02
# --------------------------------------------------------

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;*/


#
# Table structure for table 'changeddetailcashbankexpenses'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailcashbankexpenses` (
  `DetailCashBankExpensesID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci NOT NULL,
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) DEFAULT NULL,
  `Credit` double(15,2) DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankExpensesID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changeddetailcashbankpayment'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailcashbankpayment` (
  `DetailCashBankPaymentID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterPurchaseID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankPaymentID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changeddetailcashbankreceipt'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailcashbankreceipt` (
  `DetailCashBankReceiptID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankReceiptID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changeddetailcreditdebitnoteamount'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailcreditdebitnoteamount` (
  `DetailCreditDebitNoteAmountID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CRDBID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Particulars` varchar(30) COLLATE latin1_german1_ci DEFAULT NULL,
  `Amount` double DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCreditDebitNoteAmountID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changeddetailcreditdebitnotestock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailcreditdebitnotestock` (
  `DetailCreditDebitNoteStockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(11) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `ReturnRate` double(13,2) unsigned DEFAULT NULL,
  `DiscountPercent` double(13,2) unsigned DEFAULT NULL,
  `DiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReasonCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AddVatInTradeRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VATPer` double(13,2) unsigned DEFAULT NULL,
  `VatAmount` double(13,2) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCreditDebitNoteStockID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changeddetailpurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailpurchase` (
  `DetailPurchaseID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `TradeRate` double(15,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `MRP` double(15,2) unsigned DEFAULT NULL,
  `SaleRate` double(15,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `ReplacementQuantity` int(10) unsigned DEFAULT NULL,
  `ItemDiscountPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(15,4) unsigned DEFAULT NULL,
  `SchemeDiscountPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(15,2) unsigned DEFAULT NULL,
  `SpecialDiscountPercent` double(15,4) unsigned DEFAULT NULL,
  `AmountSpecialDiscount` double(15,2) unsigned DEFAULT NULL,
  `PurchaseVATPercent` double(15,2) unsigned DEFAULT NULL,
  `ProductVATPercent` double(15,2) unsigned DEFAULT NULL,
  `Margin` double(9,2) unsigned DEFAULT NULL,
  `MarginAfterDiscount` double(9,2) unsigned DEFAULT NULL,
  `AmountPurchaseVAT` double(15,4) unsigned DEFAULT NULL,
  `AmountProdVAT` double(15,2) unsigned DEFAULT NULL,
  `CSTPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountCreditNote` double(15,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountCST` double(15,2) unsigned DEFAULT NULL,
  `IfMRPInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfTradeRateInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `scancode` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `DistributorSaleRatePer` double(12,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailPurchaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changeddetailsale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changeddetailsale` (
  `DetailSaleID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `CashDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `OctroiPer` double(6,2) unsigned DEFAULT NULL,
  `OctroiAmount` double(13,2) unsigned DEFAULT NULL,
  `CSTPer` double(6,2) unsigned DEFAULT NULL,
  `CSTAmount` double(13,2) unsigned DEFAULT NULL,
  `VATPer` double(6,2) unsigned DEFAULT NULL,
  `VATAmount` double(13,2) unsigned DEFAULT NULL,
  `InwardNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IndentNumber` int(10) unsigned DEFAULT NULL,
  `PMTDiscount` double(6,2) unsigned DEFAULT NULL,
  `PMTAmount` double(13,2) unsigned DEFAULT NULL,
  `ItemDiscountPer` double(6,2) unsigned DEFAULT NULL,
  `ItemDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `SchemeDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `IfProductDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailSaleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedspecialdetailsale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedspecialdetailsale` (
  `DetailSaleID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedMasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `OctroiPer` double(6,2) unsigned DEFAULT NULL,
  `OctroiAmount` double(13,2) unsigned DEFAULT NULL,
  `CSTPer` double(6,2) unsigned DEFAULT NULL,
  `CSTAmount` double(13,2) unsigned DEFAULT NULL,
  `VATPer` double(6,2) unsigned DEFAULT NULL,
  `VATAmount` double(13,2) unsigned DEFAULT NULL,
  `InwardNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IndentNumber` int(10) unsigned DEFAULT NULL,
  `PMTDiscount` double(6,2) unsigned DEFAULT NULL,
  `PMTAmount` double(13,2) unsigned DEFAULT NULL,
  `ItemDiscountPer` double(6,2) unsigned DEFAULT NULL,
  `ItemDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `IfProductDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) unsigned DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) unsigned DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailSaleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedspecialvouchersale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedspecialvouchersale` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `CounterSaleNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountClear` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountBalance` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountGross` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `CashDiscountPercent` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountSpecialDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountPMTDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(13,2) unsigned DEFAULT NULL,
  `AddOnFreight` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountCreditNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountDebitNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `OctroiPercentage` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountOctroi` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `SalePrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT5Per` double(13,2) unsigned DEFAULT '0.00',
  `VAT12Point5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT '0.00',
  `AmountForZeroVAT` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `RoundingAmount` double(13,2) NOT NULL DEFAULT '0.00',
  `DiscountAmountCB` double(13,2) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) unsigned DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) unsigned DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvouchercashbankexpenses'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvouchercashbankexpenses` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvouchercashbankpayment'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvouchercashbankpayment` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvouchercashbankreceipt'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvouchercashbankreceipt` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvouchercreditdebitnote'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvouchercreditdebitnote` (
  `CRDBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `DiscountPer` double(5,2) unsigned DEFAULT NULL,
  `DiscountAmount` double(15,2) unsigned DEFAULT NULL,
  `RoundingAmount` double(5,2) DEFAULT NULL,
  `VAT5` double(15,2) unsigned DEFAULT NULL,
  `VAT12point5` double(15,2) unsigned DEFAULT NULL,
  `Amount` double(15,2) unsigned DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherNumber` int(10) unsigned DEFAULT NULL,
  `ClearedInVoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInPurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Uploaded` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvoucherjv'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvoucherjv` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL,
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `AccountID` varchar(32) COLLATE latin1_german1_ci NOT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) unsigned DEFAULT NULL,
  `Credit` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `AmountBalance` double(15,2) unsigned DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReferenceVoucherID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvoucherpurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvoucherpurchase` (
  `purchaseID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(5) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `AmountBalance` double(15,2) unsigned DEFAULT NULL,
  `AmountGross` double(15,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountSpecialDiscount` double(15,4) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(15,2) unsigned DEFAULT NULL,
  `CashDiscountPercentage` double(15,2) unsigned DEFAULT NULL,
  `SpecialDiscountPercentage` double(15,4) unsigned DEFAULT NULL,
  `AmountAddOnFreight` double(15,2) unsigned DEFAULT NULL,
  `AmountLess` double(15,2) unsigned DEFAULT NULL,
  `AmountCreditNote` double(15,2) unsigned DEFAULT NULL,
  `AmountDebitNote` double(15,2) unsigned DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `OctroiPercentage` double(15,2) DEFAULT NULL,
  `AmountOctroi` double(15,2) unsigned DEFAULT NULL,
  `DueDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountPurchaseZeroVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchase5PercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVAT5Percent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchase12point5PercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVAT12point5Percent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchaseOPercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVATOPercent` double(15,2) unsigned DEFAULT NULL,
  `NumberofChallans` int(10) unsigned DEFAULT NULL,
  `EntryDate` varchar(25) COLLATE latin1_german1_ci DEFAULT NULL,
  `RoundUpAmount` double(15,2) DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'changedvouchersale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `changedvouchersale` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChangedID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `CounterSaleNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountClear` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountBalance` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountGross` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `CashDiscountPercent` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountSpecialDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountPMTDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(13,2) unsigned DEFAULT NULL,
  `AddOnFreight` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountCreditNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountDebitNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `OctroiPercentage` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountOctroi` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScanPrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreditCardBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScanPrescriptionFileName` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Telephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT5Per` double(13,2) unsigned DEFAULT '0.00',
  `VAT12Point5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT '0.00',
  `AmountForZeroVAT` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `RoundingAmount` double(13,2) NOT NULL DEFAULT '0.00',
  `DiscountAmountCB` double(13,2) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) DEFAULT NULL,
  `MySpecialDiscountPercent` double(10,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount12point5` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount5` double(12,2) unsigned DEFAULT NULL,
  `AmountCashDiscount5` double(12,2) unsigned DEFAULT NULL,
  `AmountCashDiscount12point5` double(12,2) unsigned DEFAULT NULL,
  `IfFullPayment` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChangedID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailcashbankexpenses'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailcashbankexpenses` (
  `DetailCashBankExpensesID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) DEFAULT NULL,
  `Credit` double(15,2) DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankExpensesID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailcashbankpayment'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailcashbankpayment` (
  `DetailCashBankPaymentID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterPurchaseID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankPaymentID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailcashbankreceipt'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailcashbankreceipt` (
  `DetailCashBankReceiptID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankReceiptID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailcreditdebitnoteamount'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailcreditdebitnoteamount` (
  `DetailCreditDebitNoteAmountID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `CRDBID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Particulars` varchar(30) COLLATE latin1_german1_ci DEFAULT NULL,
  `Amount` double DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCreditDebitNoteAmountID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailcreditdebitnotestock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailcreditdebitnotestock` (
  `DetailCreditDebitNoteStockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(11) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `ReturnRate` double(13,2) unsigned DEFAULT NULL,
  `DiscountPercent` double(13,2) unsigned DEFAULT NULL,
  `DiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReasonCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AddVatInTradeRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VATPer` double(13,2) unsigned DEFAULT NULL,
  `VatAmount` double(13,2) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCreditDebitNoteStockID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailpurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailpurchase` (
  `DetailPurchaseID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PurchaseID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `TradeRate` double(15,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `MRP` double(15,2) unsigned DEFAULT NULL,
  `SaleRate` double(15,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `ReplacementQuantity` int(10) unsigned DEFAULT NULL,
  `ItemDiscountPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(15,4) unsigned DEFAULT NULL,
  `SchemeDiscountPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(15,2) unsigned DEFAULT NULL,
  `SpecialDiscountPercent` double(15,4) unsigned DEFAULT NULL,
  `AmountSpecialDiscount` double(15,2) unsigned DEFAULT NULL,
  `PurchaseVATPercent` double(15,2) unsigned DEFAULT NULL,
  `ProductVATPercent` double(15,2) unsigned DEFAULT NULL,
  `Margin` double(9,2) unsigned DEFAULT NULL,
  `MarginAfterDiscount` double(9,2) unsigned DEFAULT NULL,
  `AmountPurchaseVAT` double(15,4) unsigned DEFAULT NULL,
  `AmountProdVAT` double(15,2) unsigned DEFAULT NULL,
  `CSTPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountCreditNote` double(15,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountCST` double(15,2) unsigned DEFAULT NULL,
  `IfMRPInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfTradeRateInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `scancode` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `DistributorSaleRatePer` double(12,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailPurchaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deleteddetailsale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deleteddetailsale` (
  `DetailSaleID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `CashDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `OctroiPer` double(6,2) unsigned DEFAULT NULL,
  `OctroiAmount` double(13,2) unsigned DEFAULT NULL,
  `CSTPer` double(6,2) unsigned DEFAULT NULL,
  `CSTAmount` double(13,2) unsigned DEFAULT NULL,
  `VATPer` double(6,2) unsigned DEFAULT NULL,
  `VATAmount` double(13,2) unsigned DEFAULT NULL,
  `InwardNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IndentNumber` int(10) unsigned DEFAULT NULL,
  `PMTDiscount` double(6,2) unsigned DEFAULT NULL,
  `PMTAmount` double(13,2) unsigned DEFAULT NULL,
  `ItemDiscountPer` double(6,2) unsigned DEFAULT NULL,
  `ItemDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `SchemeDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `IfProductDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailSaleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedspecialdetailsale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedspecialdetailsale` (
  `DetailSaleID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `OctroiPer` double(6,2) unsigned DEFAULT NULL,
  `OctroiAmount` double(13,2) unsigned DEFAULT NULL,
  `CSTPer` double(6,2) unsigned DEFAULT NULL,
  `CSTAmount` double(13,2) unsigned DEFAULT NULL,
  `VATPer` double(6,2) unsigned DEFAULT NULL,
  `VATAmount` double(13,2) unsigned DEFAULT NULL,
  `InwardNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IndentNumber` int(10) unsigned DEFAULT NULL,
  `PMTDiscount` double(6,2) unsigned DEFAULT NULL,
  `PMTAmount` double(13,2) unsigned DEFAULT NULL,
  `ItemDiscountPer` double(6,2) unsigned DEFAULT NULL,
  `ItemDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `IfProductDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) unsigned DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) unsigned DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailSaleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedspecialvouchersale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedspecialvouchersale` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `CounterSaleNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountClear` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountBalance` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountGross` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `CashDiscountPercent` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountSpecialDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountPMTDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(13,2) unsigned DEFAULT NULL,
  `AddOnFreight` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountCreditNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountDebitNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `OctroiPercentage` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountOctroi` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `SalePrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT5Per` double(13,2) unsigned DEFAULT '0.00',
  `VAT12Point5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT '0.00',
  `AmountForZeroVAT` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `RoundingAmount` double(13,2) NOT NULL DEFAULT '0.00',
  `DiscountAmountCB` double(13,2) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) unsigned DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) unsigned DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `NewIndex` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvouchercashbankexpenses'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvouchercashbankexpenses` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvouchercashbankpayment'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvouchercashbankpayment` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvouchercashbankreceipt'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvouchercashbankreceipt` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvouchercreditdebitnote'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvouchercreditdebitnote` (
  `CRDBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `DiscountPer` double(5,2) unsigned DEFAULT NULL,
  `DiscountAmount` double(15,2) unsigned DEFAULT NULL,
  `RoundingAmount` double(5,2) DEFAULT NULL,
  `VAT5` double(15,2) unsigned DEFAULT NULL,
  `VAT12point5` double(15,2) unsigned DEFAULT NULL,
  `Amount` double(15,2) unsigned DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherNumber` int(10) unsigned DEFAULT NULL,
  `ClearedInVoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInPurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Uploaded` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CRDBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvoucherjv'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvoucherjv` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) unsigned DEFAULT NULL,
  `Credit` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `AmountBalance` double(15,2) unsigned DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReferenceVoucherID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvoucherpurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvoucherpurchase` (
  `purchaseID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(5) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `AmountBalance` double(15,2) unsigned DEFAULT NULL,
  `AmountGross` double(15,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountSpecialDiscount` double(15,4) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(15,2) unsigned DEFAULT NULL,
  `CashDiscountPercentage` double(15,2) unsigned DEFAULT NULL,
  `SpecialDiscountPercentage` double(15,4) unsigned DEFAULT NULL,
  `AmountAddOnFreight` double(15,2) unsigned DEFAULT NULL,
  `AmountLess` double(15,2) unsigned DEFAULT NULL,
  `AmountCreditNote` double(15,2) unsigned DEFAULT NULL,
  `AmountDebitNote` double(15,2) unsigned DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `OctroiPercentage` double(15,2) DEFAULT NULL,
  `AmountOctroi` double(15,2) unsigned DEFAULT NULL,
  `DueDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountPurchaseZeroVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchase5PercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVAT5Percent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchase12point5PercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVAT12point5Percent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchaseOPercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVATOPercent` double(15,2) unsigned DEFAULT NULL,
  `NumberofChallans` int(10) unsigned DEFAULT NULL,
  `EntryDate` varchar(25) COLLATE latin1_german1_ci DEFAULT NULL,
  `RoundUpAmount` double(15,2) DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`purchaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'deletedvouchersale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `deletedvouchersale` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `CounterSaleNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountClear` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountBalance` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountGross` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `CashDiscountPercent` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountSpecialDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountPMTDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(13,2) unsigned DEFAULT NULL,
  `AddOnFreight` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountCreditNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountDebitNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `OctroiPercentage` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountOctroi` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScanPrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreditCardBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Scanprescriptionfilename` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Telephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT5Per` double(13,2) unsigned DEFAULT '0.00',
  `VAT12Point5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT '0.00',
  `AmountForZeroVAT` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `RoundingAmount` double(13,2) NOT NULL DEFAULT '0.00',
  `DiscountAmountCB` double(13,2) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) DEFAULT NULL,
  `MySpecialDiscountPercent` double(10,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount12point5` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount5` double(12,2) unsigned DEFAULT NULL,
  `AmountCashDiscount5` double(12,2) unsigned DEFAULT NULL,
  `AmountCashDiscount12point5` double(12,2) unsigned DEFAULT NULL,
  `IfFullPayment` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `NewIndex` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailcashbankexpenses'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailcashbankexpenses` (
  `DetailCashBankExpensesID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) DEFAULT NULL,
  `Credit` double(15,2) DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankExpensesID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailcashbankpayment'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailcashbankpayment` (
  `DetailCashBankPaymentID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterPurchaseID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankPaymentID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailcashbankreceipt'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailcashbankreceipt` (
  `DetailCashBankReceiptID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCashBankReceiptID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailchequereturn'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailchequereturn` (
  `DetailChequeReturnID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterChequeReturnID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillNumber` int(11) DEFAULT NULL,
  `BillDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BillAmount` double DEFAULT NULL,
  `BalanceAmount` double DEFAULT NULL,
  `ClearAmount` double DEFAULT NULL,
  `DiscountAmount` double DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailChequeReturnID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailcreditdebitnoteamount'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailcreditdebitnoteamount` (
  `DetailCreditDebitNoteAmountID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `CRDBID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Particulars` varchar(30) COLLATE latin1_german1_ci DEFAULT NULL,
  `Amount` double DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCreditDebitNoteAmountID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailcreditdebitnotestock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailcreditdebitnotestock` (
  `DetailCreditDebitNoteStockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(11) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `ReturnRate` double(13,2) unsigned DEFAULT NULL,
  `DiscountPercent` double(13,2) unsigned DEFAULT NULL,
  `DiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReasonCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AddVatInTradeRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VATPer` double(13,2) unsigned DEFAULT NULL,
  `VatAmount` double(13,2) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailCreditDebitNoteStockID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailopstock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailopstock` (
  `DetailOpStockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `TradeRate` double(15,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `MRP` double(15,2) unsigned DEFAULT NULL,
  `SaleRate` double(15,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `ReplacementQuantity` int(10) unsigned DEFAULT NULL,
  `PurchaseVATPercent` double(15,2) unsigned DEFAULT NULL,
  `ProductVATPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchaseVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountProdVAT` double(15,2) unsigned DEFAULT NULL,
  `CSTPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountCST` double(15,2) unsigned DEFAULT NULL,
  `IfMRPInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfTradeRateInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailOpStockID`),
  KEY `NewIndex` (`DetailOpStockID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailpurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailpurchase` (
  `DetailPurchaseID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PurchaseID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `TradeRate` double(15,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `MRP` double(15,2) unsigned DEFAULT NULL,
  `SaleRate` double(15,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `ReplacementQuantity` int(10) unsigned DEFAULT NULL,
  `ItemDiscountPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(15,4) unsigned DEFAULT NULL,
  `SchemeDiscountPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(15,2) unsigned DEFAULT NULL,
  `SpecialDiscountPercent` double(15,4) unsigned DEFAULT NULL,
  `AmountSpecialDiscount` double(15,2) unsigned DEFAULT NULL,
  `PurchaseVATPercent` double(15,2) unsigned DEFAULT NULL,
  `ProductVATPercent` double(15,2) unsigned DEFAULT NULL,
  `Margin` double(9,2) unsigned DEFAULT NULL,
  `MarginAfterDiscount` double(9,2) unsigned DEFAULT NULL,
  `AmountPurchaseVAT` double(15,4) unsigned DEFAULT NULL,
  `AmountProdVAT` double(15,2) unsigned DEFAULT NULL,
  `CSTPercent` double(15,2) unsigned DEFAULT NULL,
  `AmountCreditNote` double(15,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountCST` double(15,2) unsigned DEFAULT NULL,
  `IfMRPInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfTradeRateInclusiveOfVAT` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `scancode` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `DistributorSaleRatePer` double(12,2) unsigned DEFAULT NULL,
  `DistributorSaleRate` double(15,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailPurchaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'detailsale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `detailsale` (
  `DetailSaleID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `CashDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `OctroiPer` double(6,2) unsigned DEFAULT NULL,
  `OctroiAmount` double(13,2) unsigned DEFAULT NULL,
  `CSTPer` double(6,2) unsigned DEFAULT NULL,
  `CSTAmount` double(13,2) unsigned DEFAULT NULL,
  `VATPer` double(6,2) unsigned DEFAULT NULL,
  `VATAmount` double(13,2) unsigned DEFAULT NULL,
  `InwardNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IndentNumber` int(10) unsigned DEFAULT NULL,
  `PMTDiscount` double(6,2) unsigned DEFAULT NULL,
  `PMTAmount` double(13,2) unsigned DEFAULT NULL,
  `ItemDiscountPer` double(6,2) unsigned DEFAULT NULL,
  `ItemDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `SchemeDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `IfProductDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailSaleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'inity'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `inity` (
  `InityID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompTelephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompEmailID` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompDLN` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompVATTIN` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompJurisdiction` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompLicNumber` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompStartDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompEndDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompIFYearEndOver` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'linkdebtorproduct'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `linkdebtorproduct` (
  `LinkDebtorProductID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`LinkDebtorProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'linkdruggrouping'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `linkdruggrouping` (
  `LinkDrugGroupingID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `GenericCategoryID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`LinkDrugGroupingID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'linkpartycompany'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `linkpartycompany` (
  `LinkPartyCompanyID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`LinkPartyCompanyID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'linkpatientproduct'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `linkpatientproduct` (
  `LinkPatientProductID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `patientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `productID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `quantity` int(7) unsigned DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`LinkPatientProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'linkprescription'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `linkprescription` (
  `LinkPrescriptionID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`LinkPrescriptionID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masteraccount'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masteraccount` (
  `AccountID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `AccCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccOpeningDebit` double(12,2) DEFAULT NULL,
  `AccOpeningCredit` double(12,2) DEFAULT NULL,
  `AccAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccTelephone` varchar(100) COLLATE latin1_german1_ci DEFAULT NULL,
  `MobileNumberForSMS` varchar(13) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccContactPerson` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccDiscountOffered` double(12,2) DEFAULT NULL,
  `AccCrVisitDays` varchar(7) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccTransactionType` varchar(2) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccIFOctroi` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccOctroiPer` double(12,2) DEFAULT NULL,
  `AccBankId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccGroupID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccDoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccAreaID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccTransactionOpeningDB` double(15,2) unsigned DEFAULT NULL,
  `AccTransactionOpeningCR` double(15,2) DEFAULT NULL,
  `AccTransactionDebit` double(12,2) DEFAULT NULL,
  `AccTransactionCredit` double(12,2) DEFAULT NULL,
  `AccClosingDebit` double(12,2) DEFAULT NULL,
  `AccClosingCredit` double(12,2) DEFAULT NULL,
  `AccClearedAmount` double(12,2) DEFAULT NULL,
  `AccLastVoucherNumber` double(12,0) DEFAULT NULL,
  `AccVoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccShortName` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'mostly required for creditor For printing on Barcode sticker',
  `AccNameAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'for debtors for printing on bill',
  `AccBirthDay` int(2) unsigned DEFAULT NULL,
  `AccBirthMonth` int(2) unsigned DEFAULT NULL,
  `AccBirthYear` int(4) unsigned DEFAULT NULL,
  `AccHistory` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'for debtors as patient',
  `AccVATTinNumber` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'for Creditors',
  `AccDLN` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccPAN` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccLBT` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccEmailID` varchar(40) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccTokenNumber` int(10) DEFAULT NULL,
  `AccStatement15Days` varchar(1) COLLATE latin1_german1_ci DEFAULT 'Y',
  `IPartyID` int(10) unsigned DEFAULT NULL COMMENT 'emedico.in',
  `AccNumber` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccRemark1` varchar(100) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccRemark2` varchar(100) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccBankAccountNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccDbVisitDay1` int(2) unsigned DEFAULT NULL,
  `AccDbVisitDay2` int(2) unsigned DEFAULT NULL,
  `AccDbVisitDay3` int(2) unsigned DEFAULT NULL,
  `AccLessPercentInDebitNote` double(5,2) unsigned DEFAULT NULL,
  `IFFIX` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IFLBT` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `TAG` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountCode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  `GlobalID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `MSCDACode` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AlliedCode` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`AccountID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterarea'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterarea` (
  `AreaID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `AreaName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `areacode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`AreaID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterbank'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterbank` (
  `BankID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `BankName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(45) COLLATE latin1_german1_ci DEFAULT NULL,
  `Bankcode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`BankID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterbranch'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterbranch` (
  `BranchID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `BranchName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Branchcode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`BranchID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastercompany'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastercompany` (
  `CompID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `CompName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompShortName` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompTelephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompMailId` varchar(40) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompContactPerson` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CompAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PartyID_1` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PartyID_2` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Companycode` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `GlobalID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Tag` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CompID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastercustomer'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastercustomer` (
  `CustomerID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `CustomerNameAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterdoctor'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterdoctor` (
  `DocID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `DocName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DocAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DocTelephone` varchar(100) COLLATE latin1_german1_ci DEFAULT NULL,
  `MobileNumberForSMS` varchar(13) COLLATE latin1_german1_ci DEFAULT NULL,
  `DocEmailID` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DocShortNameAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DocRegistrationNumber` varchar(150) COLLATE latin1_german1_ci DEFAULT NULL,
  `DocDegree` varchar(150) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorCode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`DocID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastergenericcategory'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastergenericcategory` (
  `GenericCategoryID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `GenericCategoryName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `DrugCode` int(10) DEFAULT NULL,
  PRIMARY KEY (`GenericCategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastergroup'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastergroup` (
  `GroupID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `GroupName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `GroupCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `UnderGroupId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `UnderGroupIDParentID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IFFIX` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IFMainGroup` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfSubGroup` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` int(10) unsigned DEFAULT NULL,
  `LevelNumber` int(32) unsigned DEFAULT NULL,
  `BalanceSheetCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ShowInBalanceSheet` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `BalanceSheetSrNumber` int(1) unsigned DEFAULT NULL,
  `OpeningDebit` double(15,2) DEFAULT NULL,
  `OpeningCredit` double(15,2) DEFAULT NULL,
  `TransactionDebit` double(15,2) DEFAULT NULL,
  `TransactionCredit` double(15,2) DEFAULT NULL,
  `ClosingDebit` double(15,2) unsigned DEFAULT NULL,
  `ClosingCredit` double(15,2) unsigned DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`GroupID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterhospitalpatient'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterhospitalpatient` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `InwardNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(30) COLLATE latin1_german1_ci DEFAULT NULL,
  `Address1` varchar(30) COLLATE latin1_german1_ci DEFAULT NULL,
  `Address2` varchar(30) COLLATE latin1_german1_ci DEFAULT NULL,
  `ShortNameAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Telephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Email` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `RoomNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `IDNumber` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BirthDay` int(10) unsigned DEFAULT NULL,
  `BirthMonth` int(10) unsigned DEFAULT NULL,
  `BirthYear` int(10) unsigned DEFAULT NULL,
  `AgeYears` int(10) unsigned DEFAULT NULL,
  `AgeMonths` int(10) unsigned DEFAULT NULL,
  `AgeDays` int(10) unsigned DEFAULT NULL,
  `Gender` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `WardID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientCategoryID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `DateOfAdmission` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `DateofDischarge` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Remark1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Remark2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Remark3` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `StatementDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementAmount` double(15,2) unsigned DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterorder'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterorder` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Amount` double(15,2) DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterpack'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterpack` (
  `PackID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PackName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`PackID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterpacktype'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterpacktype` (
  `ID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PackTypeName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterpatient'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterpatient` (
  `PatientID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `TelephoneNumber` varchar(100) COLLATE latin1_german1_ci DEFAULT NULL,
  `MobileNumberForSMS` varchar(13) COLLATE latin1_german1_ci DEFAULT NULL,
  `Email` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BirthDay` int(10) unsigned DEFAULT NULL,
  `BirthMonth` int(10) unsigned DEFAULT NULL,
  `BirthYear` int(10) unsigned DEFAULT NULL,
  `VisitDay1` int(10) unsigned DEFAULT NULL,
  `VisitDay2` int(10) unsigned DEFAULT NULL,
  `VisitDay3` int(10) unsigned DEFAULT NULL,
  `ShortNameAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Gender` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Remark1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Remark2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Remark3` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DiscountOffered` double(8,2) unsigned DEFAULT NULL,
  `TokenNumber` int(10) DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientCode` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterprescription'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterprescription` (
  `prescriptionID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PrescriptionName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`prescriptionID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterproduct'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterproduct` (
  `ProductID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ProdName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLoosePack` int(4) unsigned DEFAULT NULL,
  `ProdPack` varchar(6) COLLATE latin1_german1_ci DEFAULT NULL,
  `prodpacktype` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdBoxQuantity` int(10) unsigned DEFAULT NULL,
  `ProdCompShortName` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdVATPercent` double(12,2) DEFAULT NULL,
  `ProdCST` double(12,2) DEFAULT NULL,
  `ProdCSTPercent` double(12,2) DEFAULT NULL,
  `ProdGrade` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdCompID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdShelfID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdDrugID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdCategoryID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdScheduleDrugCode` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfSchedule` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfShortListed` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfSaleDisc` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfPurchaseRateInclusive` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfMRPInclusive` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfOctroi` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdRequireColdStorage` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdIfBarcoderequired` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdPartyId_1` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdPartyId_2` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdMinLevel` int(10) unsigned DEFAULT NULL,
  `ProdMaxLevel` int(10) unsigned DEFAULT NULL,
  `ProdMargin` double(10,2) unsigned DEFAULT NULL,
  `ProdLastPurchaseBillNumber` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchasePartyId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseVoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseVoucherNumber` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseRate` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseTradeRate` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseSaleRate` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseDistributorSaleRatePer` double(12,2) unsigned DEFAULT NULL,
  `ProdLastPurchaseDistributorSaleRate` double(15,2) unsigned DEFAULT NULL,
  `ProdLastPurchaseMRP` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseVATPer` double(6,2) DEFAULT NULL,
  `ProdLastPurchaseCSTPer` double(6,2) DEFAULT NULL,
  `ProdLastPurchaseCST` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseSCMPer` double(6,2) DEFAULT NULL,
  `ProdLastPurchaseSCM` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseItemDiscPer` double(6,2) DEFAULT NULL,
  `ProdLastPurchaseLocalTaxPer` double(6,2) unsigned DEFAULT NULL,
  `ProdLastPurchaseLocalTaxAmt` double(12,2) DEFAULT NULL,
  `ProdLastPurchaseExpiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseBatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastPurchaseStockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdOpeningStock` int(10) unsigned DEFAULT NULL,
  `ProdClosingStock` int(10) DEFAULT NULL,
  `ProdUserDefineCode` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdSchemeOpeningQty` int(10) unsigned DEFAULT NULL,
  `ProdSchemePurchaseQty` int(10) unsigned DEFAULT NULL,
  `ProdSchemeSaleQty` int(10) unsigned DEFAULT NULL,
  `ProdSchemeCRQty` int(10) unsigned DEFAULT NULL,
  `ProdSchemeDBQty` int(10) unsigned DEFAULT NULL,
  `ProdOctroiPer` double(12,2) DEFAULT NULL,
  `ProdLastSaleBillType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastSaleBillNumber` int(10) unsigned DEFAULT NULL,
  `ProdLastSaleDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastSalePartyId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastSaleStockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProdLastSaleScanID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `prodMRP` double(15,2) unsigned DEFAULT NULL,
  `TAG` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `SSOpeningStock` int(10) DEFAULT NULL,
  `SSPurchaseStock` int(10) DEFAULT NULL,
  `SSSaleStock` int(10) DEFAULT NULL,
  `SSCRStock` int(10) DEFAULT NULL,
  `SSDRStock` int(10) DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `productCode` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `companyCode` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `GlobalID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `opstock` int(10) DEFAULT NULL,
  `purstock` int(10) DEFAULT NULL,
  `salestock` int(10) DEFAULT NULL,
  `crstock` int(10) DEFAULT NULL,
  `dbstock` int(10) DEFAULT NULL,
  PRIMARY KEY (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterproductcategory'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterproductcategory` (
  `ProductCategoryID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ProductCategoryName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `SaleDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `LBTPercent` double(10,2) DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `catcode` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ProductCategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastersalesman'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastersalesman` (
  `SalesmanID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `SalesmanName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(45) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`SalesmanID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterscheduleddrug'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterscheduleddrug` (
  `ScheduleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScheduleCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScheduleName` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterscheme'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterscheme` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductQuantity1` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity1` int(10) unsigned DEFAULT NULL,
  `ProductQuantity2` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity2` int(10) unsigned DEFAULT NULL,
  `ProductQuantity3` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity3` int(10) unsigned DEFAULT NULL,
  `SchemeDiscountPercent` double unsigned DEFAULT NULL,
  `StartingDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClosingDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfSchemeClosed` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastershelf'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastershelf` (
  `ShelfID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ShelfCode` varchar(8) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ShelfDescription` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ShelfID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastervatpercent'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `mastervatpercent` (
  `vatpercent` double(8,2) unsigned DEFAULT '0.00'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



#
# Table structure for table 'masterward'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterward` (
  `WardID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `WardName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `WardCode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`WardID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'specialdetailsale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `specialdetailsale` (
  `DetailSaleID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterSaleID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `StockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(13,2) unsigned DEFAULT NULL,
  `MRP` double(13,2) unsigned DEFAULT NULL,
  `SaleRate` double(13,2) unsigned DEFAULT NULL,
  `TradeRate` double(13,2) unsigned DEFAULT NULL,
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Quantity` int(10) unsigned DEFAULT NULL,
  `SchemeQuantity` int(10) unsigned DEFAULT NULL,
  `Amount` double(13,2) DEFAULT NULL,
  `OctroiPer` double(6,2) unsigned DEFAULT NULL,
  `OctroiAmount` double(13,2) unsigned DEFAULT NULL,
  `CSTPer` double(6,2) unsigned DEFAULT NULL,
  `CSTAmount` double(13,2) unsigned DEFAULT NULL,
  `VATPer` double(6,2) unsigned DEFAULT NULL,
  `VATAmount` double(13,2) unsigned DEFAULT NULL,
  `InwardNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IndentNumber` int(10) unsigned DEFAULT NULL,
  `PMTDiscount` double(6,2) unsigned DEFAULT NULL,
  `PMTAmount` double(13,2) unsigned DEFAULT NULL,
  `ItemDiscountPer` double(6,2) unsigned DEFAULT NULL,
  `ItemDiscountAmount` double(13,2) unsigned DEFAULT NULL,
  `IfProductDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `SerialNumber` tinyint(3) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) unsigned DEFAULT NULL,
  `ProfitPercentBySaleRate` double(8,4) unsigned DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  PRIMARY KEY (`DetailSaleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'specialvouchersale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `specialvouchersale` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `CounterSaleNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountClear` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountBalance` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountGross` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `CashDiscountPercent` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountSpecialDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountPMTDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(13,2) unsigned DEFAULT NULL,
  `AddOnFreight` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountCreditNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountDebitNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `OctroiPercentage` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountOctroi` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `StatementID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `SalePrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Telephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT5Per` double(13,2) unsigned DEFAULT '0.00',
  `VAT12Point5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT '0.00',
  `AmountForZeroVAT` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `RoundingAmount` double(13,2) NOT NULL DEFAULT '0.00',
  `DiscountAmountCB` double(13,2) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) unsigned DEFAULT NULL,
  `ProfitPercentBySaleRate` double(13,4) unsigned DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) unsigned DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `NewIndex` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblaccountingyear'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblaccountingyear` (
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `YearEndOver` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CurrentYear` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`VoucherSeries`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbldailyshortlist'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbldailyshortlist` (
  `DSLID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MasterID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` int(10) unsigned DEFAULT NULL,
  `OrderDate` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderQuantity` int(10) unsigned DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ShortListDate` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ShortListTime` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfSave` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfDailyShortList` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ac_no_w` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ac_name_w` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `comp_name` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`DSLID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'masterEmail'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterEmail` (
  `EmailID` int COLLATE latin1_german1_ci DEFAULT NULL,
  `EmailName` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL,
  `Description` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblfavourite'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblfavourite` (
  `FavouriteId` varchar(32) DEFAULT NULL,
  `FavName` varchar(250) DEFAULT NULL,
  `FavControlName` varchar(250) DEFAULT NULL,
  `FavOperationMode` int(11) DEFAULT NULL,
  `FavIndex` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



#
# Table structure for table 'tblfixaccounts'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblfixaccounts` (
  `AccountID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `AccCode` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccOpeningDebit` double(12,2) DEFAULT NULL,
  `AccOpeningCredit` double(12,2) DEFAULT NULL,
  `AccGroupID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccDoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccAreaID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccTransactionDebit` double(12,2) DEFAULT NULL,
  `AccTransactionCredit` double(12,2) DEFAULT NULL,
  `AccClosingDebit` double(12,2) DEFAULT NULL,
  `AccClosingCredit` double(12,2) DEFAULT NULL,
  `AccClearedAmount` double(12,2) DEFAULT NULL,
  `AccLastVoucherNumber` double(12,0) DEFAULT NULL,
  `AccVoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccShortName` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'mostly required for creditor For printing on Barcode sticker',
  `AccNameAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'for debtors for printing on bill',
  `AccBirthDay` int(2) unsigned DEFAULT NULL,
  `AccBirthMonth` int(2) unsigned DEFAULT NULL,
  `AccBirthYear` int(4) unsigned DEFAULT NULL,
  `AccHistory` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'for debtors as patient',
  `AccVATTinNumber` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'for Creditors',
  `AccDLN` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccPAN` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccEmailID` varchar(40) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccTokenNumber` int(10) DEFAULT NULL,
  `IPartyID` int(10) unsigned DEFAULT NULL COMMENT 'emedico.in',
  `AccNumber` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccRemark` varchar(255) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccBankAccountNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccDbVisitDay1` int(2) unsigned DEFAULT NULL,
  `AccDbVisitDay2` int(2) unsigned DEFAULT NULL,
  `AccDbVisitDay3` int(2) unsigned DEFAULT NULL,
  `IFFIX` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountCode` varchar(10) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`AccountID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblformulae'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblformulae` (
  `ID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `FormulaName` varchar(30) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `Formula` varchar(100) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifyDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifyUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`FormulaName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbllocktable'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbllocktable` (
  `tblLockTableID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `TableName` varchar(40) COLLATE latin1_german1_ci DEFAULT NULL,
  `VouType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VouNumber` int(11) DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedBy` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifyBy` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'mastermessage'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `masterlmessage` (
  `messageID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `Message` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbloperator'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbloperator` (
  `OperatorID` varchar(32) NOT NULL DEFAULT '',
  `OperatorName` varchar(50) DEFAULT NULL,
  `Password` varchar(4) DEFAULT NULL,
  `IFInUse` tinyint(1) unsigned DEFAULT NULL,
  `OperatorDetails` varchar(50) DEFAULT NULL,
  `CreatedDate` varchar(8) DEFAULT NULL,
  `CreatedTime` varchar(8) DEFAULT NULL,
  `CreatedUserID` varchar(32) DEFAULT NULL,
  `ModifiedDate` varchar(8) DEFAULT NULL,
  `ModifiedTime` varchar(8) DEFAULT NULL,
  `ModifiedUserID` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`OperatorID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



#
# Table structure for table 'tblEcoMartlic'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblEcoMartlic` (
  `EcoMartID` varchar(32) NOT NULL DEFAULT '',
  `Data` text NOT NULL,
  PRIMARY KEY (`EcoMartID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



#
# Table structure for table 'tblsaleprescriptions'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblsaleprescriptions` (
  `SalePrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PrescriptionData` longblob,
  `FileExtension` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `SaleBillID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblscanprescriptions'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblscanprescriptions` (
  `ScanPrescriptionID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `PrescriptionData` longblob,
  `FileExtension` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `SaleBillID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ScanPrescriptionID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblschedule'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblschedule` (
  `ID` varchar(4) NOT NULL DEFAULT '',
  `ScheduleCode` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



#
# Table structure for table 'tblsettings'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblsettings` (
  `ID` varchar(4) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `setPurchaseRounding` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'if null or "Y" then do roundup of net amount in summary purchase otherwise if "N" then do not roundup purchase net amount',
  `setPurchaseIfCreditPurchase` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseAddVATinPurchaseRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseAddVATInSaleRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseReadPurchaseOrder` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseIfProductWithOctroi` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseOctroionZeroVAT` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'if  null or "Y" and setpurchaseoctroi = "Y" then calculate octroi amount on the purchase amount for vatpercentage = zero',
  `setPurchaseChangeSaleRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'if null or "Y" then profit percentage = salerate-purchaserate/sale rate other wise  /purchase rate',
  `setPurchaseMarginbyPurchaseRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseAllowExpiredItems` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseIncludeCreditPurchaseInStatements` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPurchaseUpdateVatInMaster` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleRoundOff` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleCreditStatement` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleAskDiscountinCounterSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'if null or "Y" then do roundup of net amount in summary purchase otherwise if "N" then do not roundup purchase net amount',
  `setSaleAskRoundingInSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleShowProfitInSaleBill` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleIPDOPD` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleDiscountWithoutVAT` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleIncludeCreditsaleInStatements` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleSaveCustomerInMaster` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleShowOnlyMRPInCounterSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleAllowDistributorSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleAllowSpecialDiscount` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleRoundingTo10Paise` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleOnlyCashSaleInCounterSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleF3KeyForPatientSaleEdit` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleEditRateInCounterSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleAllowNegativeStock` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleChangeCounterSaleType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleRoundingToPreviousRupee` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setSaleAllowPendingCashMemo` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setGeneralProfitPercentageByPurchaseRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setGeneralExpiryLast` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'If  null or "Y"  then expiry day is the first day of the month if "N" then expiry day is the last day of the month',
  `setGeneralBatchNumberRequired` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL COMMENT 'if null or "Y" then profit percentage = salerate-purchaserate/sale rate other wise  /purchase rate',
  `setGeneralExpiryDateRequired` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPrintSaleBillPrintedPaper` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPrintCRDBNotePrintedPaper` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPrintCashBankVoucherPrintedPaper` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setPrintPurchaseOrderPrintedPaper` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorVoucherSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorPurchase` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorOtherThanVoucherSale` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorCRDB` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorOpeningStock` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorCorrectionInRate` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorJV` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorCashBankReceipt` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setAskOperatorCashBankPayment` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setScanBarCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `setNumberOfLinesSaleBill` tinyint(3) unsigned DEFAULT NULL,
  `BackupPath1` varchar(25) COLLATE latin1_german1_ci DEFAULT NULL,
  `BackupPath2` varchar(25) COLLATE latin1_german1_ci DEFAULT NULL,
  `BackupPath3` varchar(25) COLLATE latin1_german1_ci DEFAULT NULL,
  `setEmailID` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL,
  `setEmailPassword` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `setEmailType` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `SpecialDiscount1` double(10,2) unsigned DEFAULT NULL,
  `SpecialDiscount2` double(10,2) unsigned DEFAULT NULL,
  `SpecialDiscount3` double(10,2) unsigned DEFAULT NULL,
  `setCreditNoteRemoveCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblstock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblstock` (
  `StockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ProductID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `BatchNumber` varchar(15) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `MRP` double(15,2) unsigned NOT NULL DEFAULT '0.00',
  `Expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `TradeRate` double(15,2) unsigned DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `SaleRate` double(15,2) unsigned DEFAULT NULL,
  `DistributorSaleRatePer` double(12,2) unsigned DEFAULT NULL,
  `DistributorSaleRate` double(12,2) unsigned DEFAULT NULL,
  `IFBreakageStock` tinyint(1) unsigned DEFAULT NULL,
  `BeginningStock` int(10) unsigned DEFAULT NULL,
  `OpeningStock` int(10) DEFAULT NULL,
  `ClosingStock` int(10) DEFAULT NULL,
  `PurchaseStock` int(10) DEFAULT NULL,
  `TransferInStock` int(10) DEFAULT NULL,
  `CreditNoteStock` int(10) DEFAULT NULL,
  `SaleStock` int(10) DEFAULT NULL,
  `TransferOutStock` int(10) DEFAULT NULL,
  `DebitNoteStock` int(10) DEFAULT NULL,
  `PurchaseSchemeStock` int(10) DEFAULT NULL,
  `PurchaseReplacementStock` int(10) DEFAULT NULL,
  `SaleSchemeStock` int(10) DEFAULT NULL,
  `IfRateCorrection` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductVATPercent` double(6,2) unsigned DEFAULT NULL,
  `PurchaseVATPercent` double(6,2) unsigned DEFAULT NULL,
  `LastPurchaseAccountId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `LastPurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `LastPurchaseDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `LastPurchaseVoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `LastPurchaseVoucherNumber` int(10) unsigned DEFAULT NULL,
  `Margin` double(9,2) unsigned DEFAULT NULL,
  `ScanCode` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  UNIQUE KEY `StockID` (`StockID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbltemppurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbltemppurchase` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `stockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `batchnumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `MRP` double(15,2) unsigned DEFAULT NULL,
  `quantity` int(10) DEFAULT NULL,
  `Clearedquantity` int(10) unsigned DEFAULT NULL,
  `expiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExpiryDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseRate` double(15,2) unsigned DEFAULT NULL,
  `ClearedInVoucherID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(12) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbltempstock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbltempstock` (
  `TempStockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `StockID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ProductID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `SoldQuantity` int(11) DEFAULT NULL,
  `ModuleNumber` int(11) DEFAULT NULL,
  `CompName` varchar(250) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `Mode` int(11) DEFAULT NULL,
  `CustomerNumber` int(11) DEFAULT NULL,
  UNIQUE KEY `TempStockID` (`TempStockID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbltrnac'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbltrnac` (
  `tblTrnacID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `Voucherseries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) DEFAULT NULL,
  `Credit` double(15,2) DEFAULT NULL,
  `AccAccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `TransactionDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReferenceVoucherID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeNumber` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `BranchName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`tblTrnacID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbluser'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbluser` (
  `UserID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `UserName` varchar(50) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `Password` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfInUse` tinyint(1) DEFAULT NULL,
  `MakeItDefault` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `Level` int(1) DEFAULT NULL,
  `UserDetails` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbluserlevel'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbluserlevel` (
  `ID` varchar(1) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `Type` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tbluserrights'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tbluserrights` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `FormName` varchar(500) COLLATE latin1_german1_ci DEFAULT NULL,
  `AddModule` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `DeleteModule` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `EditModule` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `ViewModule` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `PrintModule` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblvat'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblvat` (
  `ID` int(10) unsigned DEFAULT NULL,
  `VATPercentage` double unsigned DEFAULT NULL,
  `VATAmount` double unsigned DEFAULT NULL,
  `SaleAmount` double unsigned DEFAULT NULL,
  `PurchaseAmount` double unsigned DEFAULT NULL,
  `VATActive` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedBy` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifyDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifyBy` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblvouchernumbers'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblvouchernumbers` (
  `ID` varchar(4) COLLATE latin1_german1_ci NOT NULL,
  `PurchaseCredit` int(10) unsigned DEFAULT NULL,
  `PurchaseCashCredit` int(10) unsigned DEFAULT NULL,
  `PurchaseCash` int(10) unsigned DEFAULT NULL,
  `PurchaseOrder` int(10) unsigned DEFAULT NULL,
  `SaleChitNumber` int(10) unsigned DEFAULT NULL,
  `SaleCash` int(10) unsigned DEFAULT NULL,
  `SaleCredit` int(10) unsigned DEFAULT NULL,
  `SaleCashCredit` int(10) unsigned DEFAULT NULL,
  `DistributorSaleCash` int(10) unsigned DEFAULT NULL,
  `DistributorSaleCredit` int(10) unsigned DEFAULT NULL,
  `DistributorSaleCreditStatement` int(10) unsigned DEFAULT NULL,
  `SaleChallan` int(10) unsigned DEFAULT NULL,
  `DebitNote` int(10) unsigned DEFAULT NULL,
  `CreditNote` int(10) unsigned DEFAULT NULL,
  `CashReceipt` int(10) unsigned DEFAULT NULL,
  `BankReceipt` int(10) unsigned DEFAULT NULL,
  `CashPaid` int(10) unsigned DEFAULT NULL,
  `BankPaid` int(10) unsigned DEFAULT NULL,
  `BankExpenses` int(10) unsigned DEFAULT NULL,
  `CashExpenses` int(10) unsigned DEFAULT NULL,
  `StockIn` int(10) unsigned DEFAULT NULL,
  `StockOut` int(10) unsigned DEFAULT NULL,
  `OpeningStock` int(10) unsigned DEFAULT NULL,
  `TokenNumber` int(10) unsigned DEFAULT NULL,
  `CorrectionInRate` int(10) unsigned DEFAULT NULL,
  `ChequeReturn` int(10) unsigned DEFAULT NULL,
  `JournalVoucher` int(10) unsigned DEFAULT NULL,
  `StatementPurchase` int(10) unsigned DEFAULT NULL,
  `StatementSale` int(10) unsigned DEFAULT NULL,
  `ContraEntry` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'tblvouchertypes'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `tblvouchertypes` (
  `ID` varchar(2) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `Description` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `code` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'vouchercashbankexpenses'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `vouchercashbankexpenses` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'vouchercashbankpayment'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `vouchercashbankpayment` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ClearedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `JVNumber` int(10) unsigned DEFAULT NULL,
  `JVID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'vouchercashbankreceipt'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `vouchercashbankreceipt` (
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfChequeReturn` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `JVNumber` int(10) unsigned DEFAULT NULL,
  `JVID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'voucherchequereturn'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `voucherchequereturn` (
  `ChequeReturnID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `ChequeReturnVoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeReturnVoucherNumber` int(10) unsigned DEFAULT NULL,
  `ChequeReturnVoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeReturnVoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeReturnCharges` double(13,2) unsigned DEFAULT NULL,
  `CBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double unsigned DEFAULT NULL,
  `TotalDiscount` double unsigned DEFAULT NULL,
  `OnAccountAmount` double unsigned DEFAULT NULL,
  `ExtraAmount` double unsigned DEFAULT NULL,
  `ChequeNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ChequeDepositedBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CustomerBranchID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `BankSlipNumber` int(10) unsigned DEFAULT NULL,
  `BankSlipDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ChequeReturnID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'vouchercorrectioninrate'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `vouchercorrectioninrate` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `OldStockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `NewStockID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ProductID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OldBatch` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OldExpiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `OldMRP` double(15,2) DEFAULT NULL,
  `OldPurchaseRate` double(15,2) DEFAULT NULL,
  `OldSaleRate` double(15,2) DEFAULT NULL,
  `OldQuantity` int(10) DEFAULT NULL,
  `OldDistributorSaleRate` double(15,2) unsigned DEFAULT NULL,
  `NewBatch` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `NewExpiry` varchar(5) COLLATE latin1_german1_ci DEFAULT NULL,
  `NewMRP` double(15,2) DEFAULT NULL,
  `NewPurchaseRate` double(15,2) DEFAULT NULL,
  `NewSaleRate` double(15,2) DEFAULT NULL,
  `NewQuantity` int(10) DEFAULT NULL,
  `NewDistributorSaleRate` double(15,2) unsigned DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'vouchercreditdebitnote'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `vouchercreditdebitnote` (
  `CRDBID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `DiscountPer` double(5,2) unsigned DEFAULT NULL,
  `DiscountAmount` double(15,2) unsigned DEFAULT NULL,
  `RoundingAmount` double(5,2) DEFAULT NULL,
  `VAT5` double(15,2) unsigned DEFAULT NULL,
  `VAT12point5` double(15,2) unsigned DEFAULT NULL,
  `Amount` double(15,2) unsigned DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInVoucherNumber` int(10) unsigned DEFAULT NULL,
  `ClearedInVoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ClearedInPurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `Uploaded` char(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`CRDBID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'voucherjv'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `voucherjv` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Debit` double(15,2) unsigned DEFAULT NULL,
  `Credit` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `AmountBalance` double(15,2) unsigned DEFAULT NULL,
  `Narration` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `ReferenceVoucherID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'voucheropstock'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `voucheropstock` (
  `MasterID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(5) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserId` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`MasterID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'voucherpurchase'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `voucherpurchase` (
  `purchaseID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(5) DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `PurchaseBillNumber` varchar(15) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(15,2) unsigned DEFAULT NULL,
  `AmountClear` double(15,2) unsigned DEFAULT NULL,
  `AmountBalance` double(15,2) unsigned DEFAULT NULL,
  `AmountGross` double(15,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountSpecialDiscount` double(15,4) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(15,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(15,2) unsigned DEFAULT NULL,
  `CashDiscountPercentage` double(15,2) unsigned DEFAULT NULL,
  `SpecialDiscountPercentage` double(15,4) unsigned DEFAULT NULL,
  `AmountAddOnFreight` double(15,2) unsigned DEFAULT NULL,
  `AmountLess` double(15,2) unsigned DEFAULT NULL,
  `AmountCreditNote` double(15,2) unsigned DEFAULT NULL,
  `AmountDebitNote` double(15,2) unsigned DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `StatementID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OctroiPercentage` double(15,2) DEFAULT NULL,
  `AmountOctroi` double(15,2) unsigned DEFAULT NULL,
  `DueDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountPurchaseZeroVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchase5PercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVAT5Percent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchase12point5PercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVAT12point5Percent` double(15,2) unsigned DEFAULT NULL,
  `AmountPurchaseOPercentVAT` double(15,2) unsigned DEFAULT NULL,
  `AmountVATOPercent` double(15,2) unsigned DEFAULT NULL,
  `NumberofChallans` int(10) unsigned DEFAULT NULL,
  `EntryDate` varchar(25) COLLATE latin1_german1_ci DEFAULT NULL,
  `RoundUpAmount` double(15,2) DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `mscdacodeforaccount` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`purchaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'vouchersale'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `vouchersale` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `CounterSaleNumber` int(10) unsigned NOT NULL DEFAULT '0',
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherSubType` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountClear` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountBalance` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountGross` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `CashDiscountPercent` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountSpecialDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountCashDiscount` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountPMTDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountItemDiscount` double(13,2) unsigned DEFAULT NULL,
  `AmountSchemeDiscount` double(13,2) unsigned DEFAULT NULL,
  `AddOnFreight` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountCreditNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountDebitNote` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `OctroiPercentage` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountOctroi` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `Narration` varchar(80) COLLATE latin1_german1_ci DEFAULT NULL,
  `StatementNumber` int(10) unsigned DEFAULT NULL,
  `StatementID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `OperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScanPrescriptionID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreditCardBankID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ScanPrescriptionFileName` varchar(250) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress1` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientAddress2` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `PatientShortAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `Telephone` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorShortName` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `DoctorAddress` varchar(50) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderNumber` varchar(20) COLLATE latin1_german1_ci DEFAULT NULL,
  `OrderDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `IPDOPDCode` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT5Per` double(13,2) unsigned DEFAULT '0.00',
  `VAT12Point5Per` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT '0.00',
  `AmountForZeroVAT` double(13,2) unsigned NOT NULL DEFAULT '0.00',
  `RoundingAmount` double(13,2) NOT NULL DEFAULT '0.00',
  `DiscountAmountCB` double(13,2) unsigned DEFAULT NULL,
  `ProfitInRupees` double(13,2) DEFAULT NULL,
  `Enter column name` tinyint(4) DEFAULT NULL,
  `ProfitPercentBySaleRate` double(13,4) DEFAULT NULL,
  `ProfitPercentByPurchaseRate` double(8,4) DEFAULT NULL,
  `MySpecialDiscountPercent` double(10,2) DEFAULT NULL,
  `MySpecialDiscountAmount` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount12point5` double(12,2) unsigned DEFAULT NULL,
  `MySpecialDiscountAmount5` double(12,2) unsigned DEFAULT NULL,
  `AmountCashDiscount5` double(12,2) unsigned DEFAULT NULL,
  `AmountCashDiscount12point5` double(12,2) unsigned DEFAULT NULL,
  `CashierCheck` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `IfFullPayment` varchar(1) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `ModifiedOperatorID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `NewIndex` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;



#
# Table structure for table 'voucherstatement'
#

CREATE TABLE /*!32312 IF NOT EXISTS*/ `voucherstatement` (
  `ID` varchar(32) COLLATE latin1_german1_ci NOT NULL DEFAULT '',
  `VoucherSeries` varchar(4) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherType` varchar(3) COLLATE latin1_german1_ci DEFAULT NULL,
  `VoucherNumber` int(10) unsigned DEFAULT NULL,
  `VoucherDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `FromDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `ToDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `AccountID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  `AmountNet` double(13,2) unsigned DEFAULT NULL,
  `AmountClear` double(13,2) unsigned DEFAULT NULL,
  `AmountBalance` double(13,2) unsigned DEFAULT NULL,
  `NumberOfBills` int(10) unsigned DEFAULT NULL,
  `VAT5Per` double(13,2) unsigned DEFAULT NULL,
  `AmountVAT5Per` double(13,2) unsigned DEFAULT NULL,
  `VAT12Point5Per` double(13,2) unsigned DEFAULT NULL,
  `AmountVAT12Point5Per` double(13,2) unsigned DEFAULT NULL,
  `CreatedDate` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedTime` varchar(8) COLLATE latin1_german1_ci DEFAULT NULL,
  `CreatedUserID` varchar(32) COLLATE latin1_german1_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;*/
