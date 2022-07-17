USE [EcoMartDBCNF]
GO
/****** Object:  Table [dbo].[changeddetailcashbankexpenses]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailcashbankexpenses](
	[DetailCashBankExpensesID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NOT NULL,
	[MasterID] [varchar](32) NULL,
	[AccountID] [varchar](32) NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changeddetailcashbankpayment]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailcashbankpayment](
	[DetailCashBankPaymentID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[MasterID] [varchar](32) NOT NULL,
	[MasterPurchaseID] [varchar](32) NULL,
	[BillSeries] [varchar](4) NULL,
	[BillType] [varchar](3) NULL,
	[BillNumber] [int] NULL,
	[BillDate] [varchar](8) NULL,
	[BillSubType] [varchar](1) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [varchar](8) NULL,
	[ToDate] [varchar](8) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changeddetailcashbankreceipt]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailcashbankreceipt](
	[DetailCashBankReceiptID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[MasterID] [varchar](32) NULL,
	[MasterSaleID] [varchar](32) NULL,
	[BillSeries] [varchar](4) NULL,
	[BillType] [varchar](3) NULL,
	[BillNumber] [int] NULL,
	[BillDate] [varchar](8) NULL,
	[BillSubType] [varchar](1) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [varchar](8) NULL,
	[ToDate] [varchar](8) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changeddetailcreditdebitnoteamount]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailcreditdebitnoteamount](
	[DetailCreditDebitNoteAmountID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[CRDBID] [varchar](32) NULL,
	[Particulars] [varchar](30) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changeddetailcreditdebitnotestock]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailcreditdebitnotestock](
	[DetailCreditDebitNoteStockID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[MasterID] [varchar](32) NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](8) NULL,
	[ProductID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[ReturnRate] [decimal](18, 2) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[ReasonCode] [char](1) NULL,
	[AddVatInTradeRate] [varchar](1) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VatAmount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changeddetailpurchase]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailpurchase](
	[DetailPurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[PurchaseID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[ReplacementQuantity] [numeric](10, 0) NULL,
	[ItemDiscountPercent] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[SchemeDiscountPercent] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[SpecialDiscountPercent] [decimal](18, 2) NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[PurchaseVATPercent] [decimal](18, 2) NULL,
	[ProductVATPercent] [decimal](18, 2) NULL,
	[Margin] [decimal](18, 2) NULL,
	[MarginAfterDiscount] [decimal](18, 2) NULL,
	[AmountPurchaseVAT] [decimal](18, 2) NULL,
	[AmountProdVAT] [decimal](18, 2) NULL,
	[CSTPercent] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NULL,
	[AmountCST] [decimal](18, 2) NULL,
	[IfMRPInclusiveOfVAT] [char](1) NULL,
	[IfTradeRateInclusiveOfVAT] [char](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[scancode] [varchar](20) NULL,
 CONSTRAINT [PK_changeddetailpurchase] PRIMARY KEY CLUSTERED 
(
	[DetailPurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changeddetailsale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changeddetailsale](
	[DetailSaleID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[MasterSaleID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[Amount] [decimal](18, 2) NULL,
	[OctroiPer] [decimal](18, 2) NULL,
	[OctroiAmount] [decimal](18, 2) NULL,
	[CSTPer] [decimal](18, 2) NULL,
	[CSTAmount] [decimal](18, 2) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VATAmount] [decimal](18, 2) NULL,
	[InwardNumber] [varchar](15) NULL,
	[OperatorID] [varchar](32) NULL,
	[IPDOPDCode] [char](1) NULL,
	[IndentNumber] [numeric](10, 0) NULL,
	[PMTDiscount] [decimal](18, 2) NULL,
	[PMTAmount] [decimal](18, 2) NULL,
	[ItemDiscountPer] [decimal](18, 2) NULL,
	[ItemDiscountAmount] [decimal](18, 2) NULL,
	[IfProductDiscount] [varchar](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedspecialdetailsale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedspecialdetailsale](
	[DetailSaleID] [varchar](32) NOT NULL,
	[ChangedMasterID] [varchar](32) NULL,
	[MasterSaleID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[Amount] [decimal](18, 2) NULL,
	[OctroiPer] [decimal](18, 2) NULL,
	[OctroiAmount] [decimal](18, 2) NULL,
	[CSTPer] [decimal](18, 2) NULL,
	[CSTAmount] [decimal](18, 2) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VATAmount] [decimal](18, 2) NULL,
	[InwardNumber] [varchar](15) NULL,
	[OperatorID] [varchar](32) NULL,
	[IPDOPDCode] [char](1) NULL,
	[IndentNumber] [numeric](10, 0) NULL,
	[PMTDiscount] [decimal](18, 2) NULL,
	[PMTAmount] [decimal](18, 2) NULL,
	[ItemDiscountPer] [decimal](18, 2) NULL,
	[ItemDiscountAmount] [decimal](18, 2) NULL,
	[IfProductDiscount] [varchar](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedspecialvouchersale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedspecialvouchersale](
	[ID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherNumber] [numeric](10, 0) NOT NULL,
	[CounterSaleNumber] [numeric](10, 0) NOT NULL,
	[VoucherDate] [varchar](8) NULL,
	[VoucherSubType] [varchar](1) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NOT NULL,
	[AmountClear] [decimal](18, 2) NOT NULL,
	[AmountBalance] [decimal](18, 2) NOT NULL,
	[AmountGross] [decimal](18, 2) NOT NULL,
	[CashDiscountPercent] [decimal](18, 2) NOT NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NOT NULL,
	[AmountPMTDiscount] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AddOnFreight] [decimal](18, 2) NOT NULL,
	[AmountCreditNote] [decimal](18, 2) NOT NULL,
	[AmountDebitNote] [decimal](18, 2) NOT NULL,
	[OctroiPercentage] [decimal](18, 2) NOT NULL,
	[AmountOctroi] [decimal](18, 2) NOT NULL,
	[Narration] [varchar](80) NULL,
	[StatementNumber] [numeric](10, 0) NULL,
	[DoctorID] [varchar](32) NULL,
	[PatientID] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[SalePrescriptionID] [varchar](32) NULL,
	[PatientName] [varchar](50) NULL,
	[PatientAddress1] [varchar](50) NULL,
	[PatientAddress2] [varchar](50) NULL,
	[PatientShortName] [varchar](50) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderDate] [varchar](8) NULL,
	[IPDOPDCode] [varchar](1) NULL,
	[VAT5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT5Per] [decimal](18, 2) NULL,
	[VAT12Point5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT12Point5Per] [decimal](18, 2) NULL,
	[AmountForZeroVAT] [decimal](18, 2) NOT NULL,
	[RoundingAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmountCB] [decimal](18, 2) NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvouchercashbankexpenses]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvouchercashbankexpenses](
	[CBID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [varchar](8) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [varchar](8) NULL,
	[ChequeDepositedBankID] [varchar](32) NULL,
	[CustomerBankID] [varchar](32) NULL,
	[CustomerBranchID] [varchar](32) NULL,
	[Narration] [varchar](50) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [varchar](8) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvouchercashbankpayment]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvouchercashbankpayment](
	[CBID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [varchar](8) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [varchar](8) NULL,
	[ChequeDepositedBankID] [varchar](32) NULL,
	[CustomerBankID] [varchar](32) NULL,
	[CustomerBranchID] [varchar](32) NULL,
	[Narration] [varchar](50) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [varchar](8) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvouchercashbankreceipt]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvouchercashbankreceipt](
	[CBID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [varchar](8) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [varchar](8) NULL,
	[ChequeDepositedBankID] [varchar](32) NULL,
	[CustomerBankID] [varchar](32) NULL,
	[CustomerBranchID] [varchar](32) NULL,
	[Narration] [varchar](50) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [varchar](8) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvouchercreditdebitnote]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvouchercreditdebitnote](
	[CRDBID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [varchar](8) NULL,
	[AccountId] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[DiscountPer] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[RoundingAmount] [decimal](18, 2) NULL,
	[VAT5] [decimal](18, 2) NULL,
	[VAT12point5] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](50) NULL,
	[ClearedInID] [varchar](32) NULL,
	[ClearedInVoucherSeries] [varchar](4) NULL,
	[ClearedInVoucherType] [varchar](4) NULL,
	[ClearedInVoucherNumber] [numeric](10, 0) NULL,
	[ClearedInVoucherDate] [varchar](8) NULL,
	[ClearedInPurchaseBillNumber] [varchar](15) NULL,
	[OperatorID] [varchar](32) NULL,
	[Uploaded] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvoucherjv]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvoucherjv](
	[ID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[AccountID] [varchar](32) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [varchar](32) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [varchar](8) NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[Narration] [varchar](50) NULL,
	[ReferenceVoucherID] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvoucherpurchase]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvoucherpurchase](
	[ChangedID] [int] IDENTITY(1,1) NOT NULL,
	[purchaseID] [int] NOT NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](50) NULL,
	[PurchaseBillNumber] [varchar](15) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[AmountGross] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NULL,
	[CashDiscountPercentage] [decimal](18, 2) NULL,
	[SpecialDiscountPercentage] [decimal](18, 2) NULL,
	[AmountAddOnFreight] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
	[AmountDebitNote] [decimal](18, 2) NULL,
	[StatementNumber] [numeric](10, 0) NULL,
	[OctroiPercentage] [decimal](18, 2) NULL,
	[AmountOctroi] [decimal](18, 2) NULL,
	[DueDate] [varchar](50) NULL,
	[Narration] [varchar](80) NULL,
	[AmountPurchaseZeroVAT] [decimal](18, 2) NULL,
	[AmountPurchase5PercentVAT] [decimal](18, 2) NULL,
	[AmountVAT5Percent] [decimal](18, 2) NULL,
	[AmountPurchase12point5PercentVAT] [decimal](18, 2) NULL,
	[AmountVAT12point5Percent] [decimal](18, 2) NULL,
	[AmountPurchaseOPercentVAT] [decimal](18, 2) NULL,
	[AmountVATOPercent] [decimal](18, 2) NULL,
	[NumberofChallans] [numeric](10, 0) NULL,
	[EntryDate] [varchar](25) NULL,
	[RoundUpAmount] [decimal](18, 2) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL,
 CONSTRAINT [PK_changedvoucherpurchase] PRIMARY KEY CLUSTERED 
(
	[ChangedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changedvouchersale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changedvouchersale](
	[ID] [varchar](32) NOT NULL,
	[ChangedID] [varchar](32) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherNumber] [numeric](10, 0) NOT NULL,
	[CounterSaleNumber] [numeric](10, 0) NOT NULL,
	[VoucherDate] [varchar](50) NULL,
	[VoucherSubType] [varchar](1) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NOT NULL,
	[AmountClear] [decimal](18, 2) NOT NULL,
	[AmountBalance] [decimal](18, 2) NOT NULL,
	[AmountGross] [decimal](18, 2) NOT NULL,
	[CashDiscountPercent] [decimal](18, 2) NOT NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NOT NULL,
	[AmountPMTDiscount] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AddOnFreight] [decimal](18, 2) NOT NULL,
	[AmountCreditNote] [decimal](18, 2) NOT NULL,
	[AmountDebitNote] [decimal](18, 2) NOT NULL,
	[OctroiPercentage] [decimal](18, 2) NOT NULL,
	[AmountOctroi] [decimal](18, 2) NOT NULL,
	[Narration] [varchar](80) NULL,
	[StatementNumber] [numeric](10, 0) NULL,
	[DoctorID] [varchar](32) NULL,
	[PatientID] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[ScanPrescriptionID] [varchar](32) NULL,
	[ScanPrescriptionFileName] [varchar](250) NULL,
	[PatientName] [varchar](50) NULL,
	[PatientAddress1] [varchar](50) NULL,
	[PatientAddress2] [varchar](50) NULL,
	[PatientShortName] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[DoctorShortName] [varchar](50) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderDate] [varchar](50) NULL,
	[IPDOPDCode] [varchar](1) NULL,
	[VAT5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT5Per] [decimal](18, 2) NULL,
	[VAT12Point5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT12Point5Per] [decimal](18, 2) NULL,
	[AmountForZeroVAT] [decimal](18, 2) NOT NULL,
	[RoundingAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmountCB] [decimal](18, 2) NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL,
	[CreatedDate] [varchar](50) NULL,
	[CreatedTime] [varchar](50) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](50) NULL,
	[ModifiedTime] [varchar](50) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailcashbankexpenses]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailcashbankexpenses](
	[DetailCashBankExpensesID] [int] NULL,
	[MasterID] [int] NULL,
	[AccountID] [int] NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailcashbankpayment]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailcashbankpayment](
	[DetailCashBankPaymentID] [int] NULL,
	[MasterID] [int] NULL,
	[MasterPurchaseID] [int] NULL,
	[BillSeries] [varchar](4) NULL,
	[BillType] [varchar](3) NULL,
	[BillNumber] [varchar](11) NULL,
	[BillDate] [date] NULL,
	[BillSubType] [varchar](1) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[SerialNumber] [numeric](10, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailcashbankreceipt]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailcashbankreceipt](
	[DetailCashBankReceiptID] [int] NULL,
	[MasterID] [int] NULL,
	[MasterSaleID] [int] NULL,
	[BillSeries] [varchar](4) NULL,
	[BillType] [varchar](3) NULL,
	[BillNumber] [varchar](11) NULL,
	[BillDate] [date] NULL,
	[BillSubType] [varchar](1) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[SerialNumber] [numeric](10, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailcreditdebitnoteamount]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailcreditdebitnoteamount](
	[DetailCreditDebitNoteAmountID] [int] NULL,
	[ID] [int] NULL,
	[Particulars] [varchar](30) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailcreditdebitnotestock]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailcreditdebitnotestock](
	[DetailCreditDebitNoteStockID] [varchar](32) NOT NULL,
	[MasterID] [varchar](32) NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](8) NULL,
	[ProductID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[ReturnRate] [decimal](18, 2) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[ReasonCode] [char](1) NULL,
	[AddVatInTradeRate] [varchar](1) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VatAmount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailpurchase]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailpurchase](
	[DetailPurchaseID] [varchar](32) NOT NULL,
	[PurchaseID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[ReplacementQuantity] [numeric](10, 0) NULL,
	[ItemDiscountPercent] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[SchemeDiscountPercent] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[SpecialDiscountPercent] [decimal](18, 2) NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[PurchaseVATPercent] [decimal](18, 2) NULL,
	[ProductVATPercent] [decimal](18, 2) NULL,
	[Margin] [decimal](18, 2) NULL,
	[MarginAfterDiscount] [decimal](18, 2) NULL,
	[AmountPurchaseVAT] [decimal](18, 2) NULL,
	[AmountProdVAT] [decimal](18, 2) NULL,
	[CSTPercent] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NULL,
	[AmountCST] [decimal](18, 2) NULL,
	[IfMRPInclusiveOfVAT] [char](1) NULL,
	[IfTradeRateInclusiveOfVAT] [char](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[scancode] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deleteddetailsale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deleteddetailsale](
	[DetailSaleID] [varchar](32) NOT NULL,
	[MasterSaleID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[Amount] [decimal](18, 2) NULL,
	[OctroiPer] [decimal](18, 2) NULL,
	[OctroiAmount] [decimal](18, 2) NULL,
	[CSTPer] [decimal](18, 2) NULL,
	[CSTAmount] [decimal](18, 2) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VATAmount] [decimal](18, 2) NULL,
	[InwardNumber] [varchar](15) NULL,
	[OperatorID] [varchar](32) NULL,
	[IPDOPDCode] [char](1) NULL,
	[IndentNumber] [numeric](10, 0) NULL,
	[PMTDiscount] [decimal](18, 2) NULL,
	[PMTAmount] [decimal](18, 2) NULL,
	[ItemDiscountPer] [decimal](18, 2) NULL,
	[ItemDiscountAmount] [decimal](18, 2) NULL,
	[IfProductDiscount] [varchar](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedspecialdetailsale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedspecialdetailsale](
	[DetailSaleID] [varchar](32) NOT NULL,
	[MasterSaleID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[StockID] [varchar](32) NULL,
	[BatchNumber] [varchar](15) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](8) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[Amount] [decimal](18, 2) NULL,
	[OctroiPer] [decimal](18, 2) NULL,
	[OctroiAmount] [decimal](18, 2) NULL,
	[CSTPer] [decimal](18, 2) NULL,
	[CSTAmount] [decimal](18, 2) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VATAmount] [decimal](18, 2) NULL,
	[InwardNumber] [varchar](15) NULL,
	[OperatorID] [varchar](32) NULL,
	[IPDOPDCode] [char](1) NULL,
	[IndentNumber] [numeric](10, 0) NULL,
	[PMTDiscount] [decimal](18, 2) NULL,
	[PMTAmount] [decimal](18, 2) NULL,
	[ItemDiscountPer] [decimal](18, 2) NULL,
	[ItemDiscountAmount] [decimal](18, 2) NULL,
	[IfProductDiscount] [varchar](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedspecialvouchersale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedspecialvouchersale](
	[ID] [varchar](32) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherNumber] [numeric](10, 0) NOT NULL,
	[CounterSaleNumber] [numeric](10, 0) NOT NULL,
	[VoucherDate] [varchar](8) NULL,
	[VoucherSubType] [varchar](1) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NOT NULL,
	[AmountClear] [decimal](18, 2) NOT NULL,
	[AmountBalance] [decimal](18, 2) NOT NULL,
	[AmountGross] [decimal](18, 2) NOT NULL,
	[CashDiscountPercent] [decimal](18, 2) NOT NULL,
	[AmountSpecialDiscount] [bigint] NULL,
	[AmountCashDiscount] [decimal](18, 2) NOT NULL,
	[AmountPMTDiscount] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AddOnFreight] [decimal](18, 2) NOT NULL,
	[AmountCreditNote] [decimal](18, 2) NOT NULL,
	[AmountDebitNote] [decimal](18, 2) NOT NULL,
	[OctroiPercentage] [decimal](18, 2) NOT NULL,
	[AmountOctroi] [decimal](18, 2) NOT NULL,
	[Narration] [varchar](80) NULL,
	[StatementNumber] [numeric](10, 0) NULL,
	[DoctorID] [varchar](32) NULL,
	[PatientID] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[SalePrescriptionID] [varchar](32) NULL,
	[PatientName] [varchar](50) NULL,
	[PatientAddress1] [varchar](50) NULL,
	[PatientAddress2] [varchar](50) NULL,
	[PatientShortName] [varchar](50) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderDate] [varchar](8) NULL,
	[IPDOPDCode] [varchar](1) NULL,
	[VAT5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT5Per] [decimal](18, 2) NULL,
	[VAT12Point5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT12Point5Per] [decimal](18, 2) NULL,
	[AmountForZeroVAT] [decimal](18, 2) NOT NULL,
	[RoundingAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmountCB] [decimal](18, 2) NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvouchercashbankexpenses]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvouchercashbankexpenses](
	[CBID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [date] NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [date] NULL,
	[ChequeDepositedBankID] [int] NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](50) NULL,
	[BankSlipNumber] [int] NULL,
	[BankSlipDate] [date] NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [date] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvouchercashbankpayment]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvouchercashbankpayment](
	[CBID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [date] NULL,
	[ChequeDepositedBankID] [int] NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](50) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [date] NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvouchercashbankreceipt]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvouchercashbankreceipt](
	[CBID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [date] NULL,
	[ChequeDepositedBankID] [int] NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](50) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [date] NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvouchercreditdebitnote]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvouchercreditdebitnote](
	[CRDBID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[AccountId] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[DiscountPer] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[RoundingAmount] [decimal](18, 2) NULL,
	[VAT5] [decimal](18, 2) NULL,
	[VAT12point5] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](50) NULL,
	[ClearedInID] [int] NULL,
	[ClearedInVoucherSeries] [varchar](4) NULL,
	[ClearedInVoucherType] [varchar](4) NULL,
	[ClearedInVoucherNumber] [numeric](10, 0) NULL,
	[ClearedInVoucherDate] [date] NULL,
	[ClearedInPurchaseBillNumber] [varchar](15) NULL,
	[OperatorID] [int] NULL,
	[Uploaded] [char](1) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvoucherjv]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvoucherjv](
	[ID] [varchar](32) NOT NULL,
	[AccountID] [varchar](32) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [varchar](32) NULL,
	[VoucherNumber] [numeric](10, 0) NULL,
	[VoucherDate] [varchar](8) NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[Narration] [varchar](50) NULL,
	[ReferenceVoucherID] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvoucherpurchase]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvoucherpurchase](
	[purchaseID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[PurchaseBillNumber] [varchar](15) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[AmountGross] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NULL,
	[CashDiscountPercentage] [decimal](18, 2) NULL,
	[SpecialDiscountPercentage] [decimal](18, 2) NULL,
	[AmountAddOnFreight] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
	[AmountDebitNote] [decimal](18, 2) NULL,
	[StatementNumber] [int] NULL,
	[OctroiPercentage] [decimal](18, 2) NULL,
	[AmountOctroi] [decimal](18, 2) NULL,
	[DueDate] [varchar](8) NULL,
	[Narration] [varchar](80) NULL,
	[AmountPurchaseZeroVAT] [decimal](18, 2) NULL,
	[AmountPurchase5PercentVAT] [decimal](18, 2) NULL,
	[AmountVAT5Percent] [decimal](18, 2) NULL,
	[AmountPurchase12point5PercentVAT] [decimal](18, 2) NULL,
	[AmountVAT12point5Percent] [decimal](18, 2) NULL,
	[AmountPurchaseOPercentVAT] [decimal](18, 2) NULL,
	[AmountVATOPercent] [decimal](18, 2) NULL,
	[NumberofChallans] [int] NULL,
	[EntryDate] [date] NULL,
	[RoundUpAmount] [decimal](18, 2) NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deletedvouchersale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deletedvouchersale](
	[ID] [varchar](32) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherNumber] [numeric](10, 0) NOT NULL,
	[CounterSaleNumber] [numeric](10, 0) NOT NULL,
	[VoucherDate] [varchar](8) NULL,
	[VoucherSubType] [varchar](1) NULL,
	[AccountID] [varchar](32) NULL,
	[AmountNet] [decimal](18, 2) NOT NULL,
	[AmountClear] [decimal](18, 2) NOT NULL,
	[AmountBalance] [decimal](18, 2) NOT NULL,
	[AmountGross] [decimal](18, 2) NOT NULL,
	[CashDiscountPercent] [decimal](18, 2) NOT NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NOT NULL,
	[AmountPMTDiscount] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AddOnFreight] [bigint] NOT NULL,
	[AmountCreditNote] [decimal](18, 2) NOT NULL,
	[AmountDebitNote] [decimal](18, 2) NOT NULL,
	[OctroiPercentage] [decimal](18, 2) NOT NULL,
	[AmountOctroi] [decimal](18, 2) NOT NULL,
	[Narration] [varchar](80) NULL,
	[StatementNumber] [numeric](10, 0) NULL,
	[DoctorID] [varchar](32) NULL,
	[PatientID] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[ScanPrescriptionID] [varchar](32) NULL,
	[ScanPrescriptionFileName] [varchar](250) NULL,
	[PatientName] [varchar](50) NULL,
	[PatientAddress1] [varchar](50) NULL,
	[PatientAddress2] [varchar](50) NULL,
	[PatientShortName] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[DoctorShortName] [varchar](50) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderDate] [varchar](8) NULL,
	[IPDOPDCode] [varchar](1) NULL,
	[VAT5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT5Per] [decimal](18, 2) NULL,
	[VAT12Point5Per] [decimal](18, 2) NOT NULL,
	[AmountVAT12Point5Per] [decimal](18, 2) NULL,
	[AmountForZeroVAT] [decimal](18, 2) NOT NULL,
	[RoundingAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmountCB] [decimal](18, 2) NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailbreakageexpiry]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailbreakageexpiry](
	[DetailBreakageExpiryID] [int] NULL,
	[MasterID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[ProductID] [int] NULL,
	[StockID] [int] NULL,
	[BatchNumber] [varchar](15) NULL,
	[Quantity] [int] NULL,
	[SchemeQuantity] [int] NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[ReturnRate] [decimal](18, 2) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[Expiry] [varchar](max) NULL,
	[ExpiryDate] [date] NULL,
	[ReasonCode] [char](1) NULL,
	[AddVatInTradeRate] [varchar](1) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VatAmount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [numeric](3, 0) NULL,
	[ReplacementQuantity] [int] NULL,
	[LessPer] [decimal](18, 2) NULL,
	[TransferBrekageexpiry] [varchar](1) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailcashbankexpenses]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailcashbankexpenses](
	[DetailCashBankExpensesID] [int] NOT NULL,
	[MasterID] [int] NULL,
	[AccountID] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [numeric](10, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailcashbankpayment]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailcashbankpayment](
	[DetailCashBankPaymentID] [int] IDENTITY(1,1) NOT NULL,
	[MasterID] [int] NULL,
	[MasterPurchaseID] [int] NULL,
	[BillSeries] [varchar](50) NULL,
	[BillType] [varchar](50) NULL,
	[BillNumber] [varchar](50) NULL,
	[BillDate] [date] NULL,
	[BillSubType] [varchar](50) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [varchar](8) NULL,
	[ToDate] [varchar](8) NULL,
	[SerialNumber] [int] NULL,
 CONSTRAINT [PK_detailcashbankpayment] PRIMARY KEY CLUSTERED 
(
	[DetailCashBankPaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailcashbankreceipt]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailcashbankreceipt](
	[DetailCashBankReceiptID] [int] IDENTITY(1,1) NOT NULL,
	[MasterID] [int] NULL,
	[MasterSaleID] [int] NULL,
	[BillSeries] [varchar](50) NULL,
	[BillType] [varchar](50) NULL,
	[BillNumber] [varchar](50) NULL,
	[BillDate] [date] NULL,
	[BillSubType] [varchar](50) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [varchar](8) NULL,
	[ToDate] [varchar](8) NULL,
	[SerialNumber] [int] NULL,
 CONSTRAINT [PK_detailcashbankreceipt] PRIMARY KEY CLUSTERED 
(
	[DetailCashBankReceiptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailchequereturn]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailchequereturn](
	[DetailChequeReturnID] [int] NOT NULL,
	[MasterChequeReturnID] [int] NULL,
	[MasterID] [int] NULL,
	[MasterSaleID] [int] NULL,
	[BillSeries] [varchar](50) NULL,
	[BillType] [varchar](50) NULL,
	[BillNumber] [int] NULL,
	[BillDate] [date] NULL,
	[BillSubType] [varchar](50) NULL,
	[BillAmount] [decimal](18, 2) NULL,
	[BalanceAmount] [decimal](18, 2) NULL,
	[ClearAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[SerialNumber] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailcollectionnote]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailcollectionnote](
	[ID] [int] NOT NULL,
	[SaleId] [int] NULL,
	[MasterId] [int] NULL,
	[SaleVoucherNumber] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailcreditdebitnoteamount]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailcreditdebitnoteamount](
	[DetailCreditDebitNoteAmountID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[Particulars] [varchar](5000) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [int] NULL,
 CONSTRAINT [PK_detailcreditdebitnoteamount] PRIMARY KEY CLUSTERED 
(
	[DetailCreditDebitNoteAmountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailcreditdebitnotestock]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailcreditdebitnotestock](
	[DetailCreditDebitNoteStockID] [int] IDENTITY(1,1) NOT NULL,
	[MasterID] [int] NULL,
	[VoucherSeries] [varchar](4) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[ProductID] [int] NULL,
	[StockID] [int] NULL,
	[BatchNumber] [varchar](15) NULL,
	[Quantity] [int] NULL,
	[SchemeQuantity] [int] NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[ReturnRate] [decimal](18, 2) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [date] NULL,
	[ReasonCode] [char](1) NULL,
	[AddVatInTradeRate] [varchar](1) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VatAmount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [int] NULL,
	[ReplacementQuantity] [int] NULL,
	[LessPer] [decimal](18, 2) NULL,
	[BreakageExpiryNo] [int] NULL,
	[TransferBrekageExpiry] [varchar](1) NULL,
	[GSTAmountZero] [decimal](18, 2) NULL,
	[GSTSAmount] [decimal](18, 2) NULL,
	[GSTCAmount] [decimal](18, 2) NULL,
	[GSTIAmount] [decimal](18, 2) NULL,
	[GSTS] [decimal](18, 2) NULL,
	[GSTC] [decimal](18, 2) NULL,
	[GSTI] [decimal](18, 2) NULL,
 CONSTRAINT [PK_detailcreditdebitnotestock] PRIMARY KEY CLUSTERED 
(
	[DetailCreditDebitNoteStockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detaildeliverynote]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detaildeliverynote](
	[ID] [int] NOT NULL,
	[SaleId] [int] NOT NULL,
	[MasterId] [int] NOT NULL,
	[SaleVoucherNumber] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailopstock]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailopstock](
	[DetailOpStockID] [int] NOT NULL,
	[MasterID] [int] NULL,
	[ProductID] [int] NULL,
	[StockID] [int] NULL,
	[BatchNumber] [varchar](15) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [date] NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SchemeQuantity] [numeric](10, 0) NULL,
	[ReplacementQuantity] [numeric](10, 0) NULL,
	[PurchaseVATPercent] [decimal](18, 2) NULL,
	[ProductVATPercent] [decimal](18, 2) NULL,
	[AmountPurchaseVAT] [varbinary](50) NULL,
	[AmountProdVAT] [decimal](18, 2) NULL,
	[CSTPercent] [decimal](18, 2) NULL,
	[AmountCST] [decimal](18, 2) NULL,
	[IfMRPInclusiveOfVAT] [char](1) NULL,
	[IfTradeRateInclusiveOfVAT] [char](1) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailpurchase]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailpurchase](
	[DetailPurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseID] [int] NULL,
	[StockID] [int] NULL,
	[ProductID] [int] NULL,
	[BatchNumber] [varchar](15) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[EcoMartRate] [decimal](18, 2) NULL,
	[CNFRate] [decimal](18, 2) NULL,
	[StockistRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](32) NULL,
	[Quantity] [int] NULL,
	[SchemeQuantity] [int] NULL,
	[ReplacementQuantity] [int] NULL,
	[ItemDiscountPercent] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[SchemeDiscountPercent] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[PurchaseVATPercent] [decimal](18, 2) NULL,
	[ProductVATPercent] [decimal](18, 2) NULL,
	[AmountPurchaseVAT] [decimal](18, 2) NULL,
	[AmountProdVAT] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NULL,
	[TODAmount] [decimal](18, 2) NULL,
	[TODPercent] [decimal](18, 2) NULL,
	[SurchargePercent] [decimal](18, 2) NULL,
	[SurchargeAmount] [decimal](18, 2) NULL,
	[AddPercent] [decimal](18, 2) NULL,
	[Margin] [decimal](18, 2) NULL,
	[MarginAfterDiscount] [decimal](18, 2) NULL,
	[SerialNumber] [int] NULL,
	[scancode] [varchar](20) NULL,
	[GSTAmountZero] [decimal](18, 2) NULL,
	[GSTSAmount] [decimal](18, 2) NULL,
	[GSTCAmount] [decimal](18, 2) NULL,
	[GSTC] [decimal](18, 2) NULL,
	[GSTS] [decimal](18, 2) NULL,
	[PriceToRetailer] [decimal](18, 2) NULL,
	[ProfitPercent] [decimal](18, 2) NULL,
	[GSTiAmount] [decimal](18, 2) NULL,
	[GSTI] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
 CONSTRAINT [PK_detailpurchase] PRIMARY KEY CLUSTERED 
(
	[DetailPurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailpurchaseordercnf]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailpurchaseordercnf](
	[DSLID] [int] IDENTITY(1,1) NOT NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
	[MasterID] [int] NULL,
	[StockistOrderNumber] [numeric](10, 0) NULL,
	[StockistOrderDate] [varchar](32) NULL,
	[CNFOrderNumber] [numeric](10, 0) NULL,
	[CNFOrderDate] [varchar](32) NULL,
	[EcoMartOrderNumber] [numeric](10, 0) NULL,
	[EcoMartOrderDate] [varchar](32) NULL,
	[EcoMartBillNumber] [numeric](10, 0) NULL,
	[EcoMartBillDate] [varchar](32) NULL,
	[CNFBillNumber] [numeric](10, 0) NULL,
	[CNFBillDate] [varchar](32) NULL,
	[StockistOrderQuantity] [int] NULL,
	[StockistSchemeQuantity] [int] NULL,
	[StockistSaleQuantity] [int] NULL,
	[StockistClosingStock] [int] NULL,
	[StockistReceivedQuantity] [int] NULL,
	[StockistReceivedSchemeQuantity] [int] NULL,
	[CNFOrderQuantity] [int] NULL,
	[CNFSchemeQuantity] [int] NULL,
	[CNFSaleQuantity] [int] NULL,
	[CNFClosingStock] [int] NULL,
	[CNFReceivedQuantity] [int] NULL,
	[CNFReceivedSchemeQuantity] [int] NULL,
	[EcoMartOrderQuantity] [int] NULL,
	[EcoMartSchemeQuantity] [int] NULL,
	[ProductId] [int] NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[StockistAccountID] [int] NULL,
	[CNFAccountID] [int] NULL,
	[EcoMartAccountID] [int] NULL,
	[ShortListDate] [varchar](32) NULL,
	[ShortListTime] [varchar](32) NULL,
	[IfSave] [char](1) NULL,
	[IfDailyShortList] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_detailpurchaseordercnf] PRIMARY KEY CLUSTERED 
(
	[DSLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailpurchaseorderecomart]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailpurchaseorderecomart](
	[DSLID] [int] IDENTITY(1,1) NOT NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
	[MasterID] [int] NULL,
	[StockistOrderNumber] [numeric](10, 0) NULL,
	[StockistOrderDate] [varchar](32) NULL,
	[CNFOrderNumber] [numeric](10, 0) NULL,
	[CNFOrderDate] [varchar](32) NULL,
	[EcoMartOrderNumber] [numeric](10, 0) NULL,
	[EcoMartOrderDate] [varchar](32) NULL,
	[EcoMartBillNumber] [numeric](10, 0) NULL,
	[EcoMartBillDate] [varchar](32) NULL,
	[CNFBillNumber] [numeric](10, 0) NULL,
	[CNFBillDate] [varchar](32) NULL,
	[StockistOrderQuantity] [int] NULL,
	[StockistSchemeQuantity] [int] NULL,
	[StockistSaleQuantity] [int] NULL,
	[StockistClosingStock] [int] NULL,
	[StockistReceivedQuantity] [int] NULL,
	[StockistReceivedSchemeQuantity] [int] NULL,
	[CNFOrderQuantity] [int] NULL,
	[CNFSchemeQuantity] [int] NULL,
	[CNFSaleQuantity] [int] NULL,
	[CNFClosingStock] [int] NULL,
	[EcoMartOrderQuantity] [int] NULL,
	[EcoMartSchemeQuantity] [int] NULL,
	[ProductId] [int] NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[StockistAccountID] [int] NULL,
	[CNFAccountID] [int] NULL,
	[EcoMartAccountID] [int] NULL,
	[ShortListDate] [varchar](32) NULL,
	[ShortListTime] [varchar](32) NULL,
	[IfSave] [char](1) NULL,
	[IfDailyShortList] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_detailpurchaseorderecomart] PRIMARY KEY CLUSTERED 
(
	[DSLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailpurchaseorderfromCNF]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailpurchaseorderfromCNF](
	[DSLID] [int] IDENTITY(1,1) NOT NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
	[MasterID] [int] NULL,
	[StockistOrderNumber] [numeric](10, 0) NULL,
	[StockistOrderDate] [varchar](32) NULL,
	[CNFOrderNumber] [numeric](10, 0) NULL,
	[CNFOrderDate] [varchar](32) NULL,
	[EcoMartOrderNumber] [numeric](10, 0) NULL,
	[EcoMartOrderDate] [varchar](32) NULL,
	[EcoMartBillNumber] [numeric](10, 0) NULL,
	[EcoMartBillDate] [varchar](32) NULL,
	[CNFBillNumber] [numeric](10, 0) NULL,
	[CNFBillDate] [varchar](32) NULL,
	[StockistOrderQuantity] [int] NULL,
	[StockistSchemeQuantity] [int] NULL,
	[StockistSaleQuantity] [int] NULL,
	[StockistClosingStock] [int] NULL,
	[StockistReceivedQuantity] [int] NULL,
	[StockistReceivedSchemeQuantity] [int] NULL,
	[CNFOrderQuantity] [int] NULL,
	[CNFSchemeQuantity] [int] NULL,
	[CNFSaleQuantity] [int] NULL,
	[CNFClosingStock] [int] NULL,
	[CNFReceivedQuantity] [int] NULL,
	[CNFReceivedScheme] [int] NULL,
	[EcoMartOrderQuantity] [int] NULL,
	[EcoMartSchemeQuantity] [int] NULL,
	[ProductId] [int] NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[StockistAccountID] [int] NULL,
	[CNFAccountID] [int] NULL,
	[EcoMartAccountID] [int] NULL,
	[ShortListDate] [varchar](32) NULL,
	[ShortListTime] [varchar](32) NULL,
	[IfSave] [char](1) NULL,
	[IfDailyShortList] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_detailpurchaseorderfromCNF] PRIMARY KEY CLUSTERED 
(
	[DSLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailpurchaseorderfromstockist]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailpurchaseorderfromstockist](
	[DSLID] [int] IDENTITY(1,1) NOT NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
	[MasterID] [int] NULL,
	[StockistOrderNumber] [numeric](10, 0) NULL,
	[StockistOrderDate] [varchar](32) NULL,
	[CNFOrderNumber] [numeric](10, 0) NULL,
	[CNFOrderDate] [varchar](32) NULL,
	[EcoMartOrderNumber] [numeric](10, 0) NULL,
	[EcoMartOrderDate] [varchar](32) NULL,
	[EcoMartBillNumber] [numeric](10, 0) NULL,
	[EcoMartBillDate] [varchar](32) NULL,
	[CNFBillNumber] [numeric](10, 0) NULL,
	[CNFBillDate] [varchar](32) NULL,
	[StockistOrderQuantity] [int] NULL,
	[StockistSchemeQuantity] [int] NULL,
	[StockistSaleQuantity] [int] NULL,
	[StockistClosingStock] [int] NULL,
	[StockistReceivedQuantity] [int] NULL,
	[StockistReceivedSchemeQuantity] [int] NULL,
	[CNFOrderQuantity] [int] NULL,
	[CNFSchemeQuantity] [int] NULL,
	[CNFSaleQuantity] [int] NULL,
	[CNFClosingStock] [int] NULL,
	[EcoMartOrderQuantity] [int] NULL,
	[EcoMartSchemeQuantity] [int] NULL,
	[ProductId] [int] NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[StockistAccountID] [int] NULL,
	[CNFAccountID] [int] NULL,
	[EcoMartAccountID] [int] NULL,
	[ShortListDate] [varchar](32) NULL,
	[ShortListTime] [varchar](32) NULL,
	[IfSave] [char](1) NULL,
	[IfDailyShortList] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_detailpurchaseorderfromstockist] PRIMARY KEY CLUSTERED 
(
	[DSLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailpurchaseorderstockist]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailpurchaseorderstockist](
	[DSLID] [int] IDENTITY(1,1) NOT NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
	[MasterID] [int] NULL,
	[StockistOrderNumber] [numeric](10, 0) NULL,
	[StockistOrderDate] [varchar](32) NULL,
	[CNFOrderNumber] [numeric](10, 0) NULL,
	[CNFOrderDate] [varchar](32) NULL,
	[EcoMartOrderNumber] [numeric](10, 0) NULL,
	[EcoMartOrderDate] [varchar](32) NULL,
	[EcoMartBillNumber] [numeric](10, 0) NULL,
	[EcoMartBillDate] [varchar](32) NULL,
	[CNFBillNumber] [numeric](10, 0) NULL,
	[CNFBillDate] [varchar](32) NULL,
	[StockistOrderQuantity] [int] NULL,
	[StockistSchemeQuantity] [int] NULL,
	[StockistSaleQuantity] [int] NULL,
	[StockistClosingStock] [int] NULL,
	[StockistReceivedQuantity] [int] NULL,
	[StockistReceivedSchemeQuantity] [int] NULL,
	[CNFOrderQuantity] [int] NULL,
	[CNFSchemeQuantity] [int] NULL,
	[CNFSaleQuantity] [int] NULL,
	[CNFClosingStock] [int] NULL,
	[EcoMartOrderQuantity] [int] NULL,
	[EcoMartSchemeQuantity] [int] NULL,
	[ProductId] [int] NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[StockistAccountID] [int] NULL,
	[CNFAccountID] [int] NULL,
	[EcoMartAccountID] [int] NULL,
	[ShortListDate] [varchar](32) NULL,
	[ShortListTime] [varchar](32) NULL,
	[IfSave] [char](1) NULL,
	[IfDailyShortList] [char](1) NULL,
	[IfUploaded] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_detailpurchaseorder] PRIMARY KEY CLUSTERED 
(
	[DSLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailquotation]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailquotation](
	[DetailQuotationID] [int] NOT NULL,
	[MasterID] [int] NULL,
	[ProductID] [int] NULL,
	[StockID] [int] NULL,
	[Quantity] [int] NULL,
	[SchemeQuantity] [int] NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[Remark] [varchar](50) NULL,
	[Amount] [decimal](18, 2) NULL,
	[SerialNumber] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailsale]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailsale](
	[DetailSaleID] [int] IDENTITY(1,1) NOT NULL,
	[MasterSaleID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[StockID] [int] NULL,
	[BatchNumber] [varchar](15) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[EcoMartRate] [decimal](18, 2) NULL,
	[CNFRate] [decimal](18, 2) NULL,
	[StockistRate] [decimal](18, 2) NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](32) NULL,
	[Quantity] [int] NULL,
	[SchemeQuantity] [int] NULL,
	[SchemeDiscountAmount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[OctroiPer] [decimal](6, 2) NULL,
	[OctroiAmount] [decimal](13, 2) NULL,
	[CSTPer] [decimal](6, 2) NULL,
	[CSTAmount] [decimal](18, 2) NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VATAmount] [decimal](18, 2) NULL,
	[InwardNumber] [varchar](15) NULL,
	[OperatorID] [int] NULL,
	[IPDOPDCode] [char](1) NULL,
	[IndentNumber] [int] NULL,
	[PMTDiscount] [decimal](6, 2) NULL,
	[PMTAmount] [decimal](18, 2) NULL,
	[ItemDiscountPer] [decimal](6, 2) NULL,
	[ItemDiscountAmount] [decimal](18, 2) NULL,
	[CashDiscountAmount] [decimal](18, 2) NULL,
	[IfProductDiscount] [varchar](1) NULL,
	[SerialNumber] [tinyint] NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](8, 4) NULL,
	[ProfitPercentByPurchaseRate] [decimal](8, 4) NULL,
	[AccountID] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[SalAmntOn12P5Vat] [decimal](18, 2) NULL,
	[SalAmntOnZeroVat] [decimal](18, 2) NULL,
	[SalAmntOn18Vat] [decimal](18, 2) NULL,
	[VatAmntFor12P5vat] [decimal](18, 2) NULL,
	[VatAmntFor18Pvat] [decimal](18, 2) NULL,
	[GSTAmountZero] [decimal](18, 2) NULL,
	[GSTSAmount] [decimal](18, 2) NULL,
	[GSTCAmount] [decimal](18, 2) NULL,
	[GSTIAmount] [decimal](18, 2) NULL,
	[GSTS] [decimal](18, 2) NULL,
	[GSTC] [decimal](18, 2) NULL,
	[GSTI] [decimal](18, 2) NULL,
	[ActualBatchNumber] [varchar](15) NULL,
	[ActualMRP] [decimal](18, 2) NULL,
	[ActualSaleRate] [decimal](18, 2) NULL,
	[PONumber] [int] NULL,
	[POId] [int] NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
 CONSTRAINT [PK_detailsale] PRIMARY KEY CLUSTERED 
(
	[DetailSaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailuserrole]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailuserrole](
	[ID] [int] NOT NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[inity]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inity](
	[InityID] [varchar](32) NULL,
	[CompName] [varchar](50) NULL,
	[CompAddress] [varchar](50) NULL,
	[CompTelephone] [varchar](50) NULL,
	[CompEmailID] [varchar](50) NULL,
	[CompDLN] [varchar](50) NULL,
	[CompVATTIN] [varchar](50) NULL,
	[CompJurisdiction] [varchar](50) NULL,
	[CompLicNumber] [varchar](50) NULL,
	[CompStartDate] [varchar](8) NULL,
	[CompEndDate] [varchar](8) NULL,
	[CompIFYearEndOver] [varchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[linkdebtorproduct]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[linkdebtorproduct](
	[LinkDebtorProductID] [numeric](18, 0) NOT NULL,
	[AccountID] [numeric](18, 0) NULL,
	[ProductID] [numeric](18, 0) NULL,
	[Quantity] [int] NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[linkdruggrouping]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[linkdruggrouping](
	[LinkDrugGroupingID] [int] IDENTITY(1,1) NOT NULL,
	[GenericCategoryID] [int] NULL,
	[ProductID] [int] NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[SerialNumber] [tinyint] NULL,
 CONSTRAINT [PK_linkdruggrouping] PRIMARY KEY CLUSTERED 
(
	[LinkDrugGroupingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[linkpartycompany]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[linkpartycompany](
	[LinkPartyCompanyID] [varchar](32) NOT NULL,
	[AccountID] [varchar](32) NULL,
	[CompID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[linkpatientproduct]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[linkpatientproduct](
	[LinkPatientProductID] [varchar](32) NOT NULL,
	[patientID] [varchar](32) NULL,
	[productID] [varchar](32) NULL,
	[quantity] [numeric](10, 0) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[linkprescription]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[linkprescription](
	[LinkPrescriptionID] [varchar](32) NOT NULL,
	[PrescriptionID] [varchar](32) NULL,
	[ProductID] [varchar](32) NULL,
	[Quantity] [numeric](10, 0) NULL,
	[SerialNumber] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logindetails]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logindetails](
	[Email] [varchar](50) NULL,
	[Password] [varchar](20) NULL,
	[RememberMe] [numeric](3, 0) NULL,
	[RoleId] [int] NULL,
	[UserId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masteraccount]    Script Date: 15/07/2022 16:40:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masteraccount](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccCode] [varchar](1) NULL,
	[AccName] [varchar](50) NULL,
	[AccOpeningDebit] [decimal](18, 2) NULL,
	[AccOpeningCredit] [decimal](18, 2) NULL,
	[AccAddress1] [varchar](50) NULL,
	[AccAddress2] [varchar](50) NULL,
	[AccTelephone] [varchar](50) NULL,
	[MobileNumberForSMS] [varchar](13) NULL,
	[AccContactPerson] [varchar](50) NULL,
	[Pharmscist] [varchar](50) NULL,
	[AccDiscountOffered] [decimal](18, 2) NULL,
	[AccTransactionType] [int] NULL,
	[AccIFOctroi] [varchar](1) NULL,
	[AccOctroiPer] [decimal](18, 2) NULL,
	[AccBankId] [int] NULL,
	[AccBranchID] [int] NULL,
	[AccGroupID] [int] NULL,
	[AccDoctorID] [int] NULL,
	[AccAreaID] [int] NULL,
	[AccSalesmanID] [int] NULL,
	[AccDelivaryBoyID] [int] NULL,
	[AccDelivarySalesmanID] [int] NULL,
	[BranchCity] [varchar](50) NULL,
	[AccTransactionDebit] [decimal](18, 2) NULL,
	[AccTransactionCredit] [decimal](18, 2) NULL,
	[AccClosingDebit] [decimal](18, 2) NULL,
	[AccClosingCredit] [decimal](18, 2) NULL,
	[AccClearedAmount] [decimal](18, 2) NULL,
	[AccLastVoucherNumber] [decimal](18, 2) NULL,
	[AccVoucherDate] [varchar](32) NULL,
	[AccVATTIN] [varchar](50) NULL,
	[AccDLN] [varchar](50) NULL,
	[AccPAN] [varchar](50) NULL,
	[AccLBT] [varchar](50) NULL,
	[AccScheduleXLICNumber] [varchar](50) NULL,
	[AccEmailID] [varchar](40) NULL,
	[AccIfOMS] [varchar](50) NULL,
	[AccIfLocalParty] [varchar](1) NULL,
	[AccIfScheduleXSale] [varchar](1) NULL,
	[IPartyID] [int] NULL,
	[AccountNumber] [varchar](32) NULL,
	[AccountType] [int] NULL,
	[IFSCCode] [varchar](32) NULL,
	[AccRemark1] [varchar](max) NULL,
	[AccRemark2] [varchar](max) NULL,
	[AccBankAccountNumber] [varchar](20) NULL,
	[IFFIX] [varchar](1) NULL,
	[IFLBT] [varchar](1) NULL,
	[LockDays] [int] NULL,
	[LockStatements] [int] NULL,
	[HoldingAmount] [decimal](18, 2) NULL,
	[HoldCreditNote] [varchar](1) NULL,
	[PutInBlackList] [varchar](1) NULL,
	[SetAsDefault] [varchar](50) NULL,
	[Remark] [varchar](max) NULL,
	[TAG] [varchar](1) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[AccountCode] [varchar](10) NULL,
	[GlobalID] [varchar](32) NULL,
	[MSCDACode] [varchar](50) NULL,
	[AlliedCode] [varchar](50) NULL,
	[ScorgCode] [varchar](50) NULL,
	[PurchaseBillFormat] [varchar](20) NULL,
	[CreditDays] [int] NULL,
	[PTSper] [decimal](18, 2) NULL,
	[AccBankAccountType] [varchar](25) NULL,
	[AccTokenNumber] [numeric](18, 0) NULL,
	[AccBirthDay] [numeric](2, 0) NULL,
	[AccBirthMonth] [numeric](2, 0) NULL,
	[AccBirthYear] [numeric](4, 0) NULL,
	[AccCrVisitDays] [varchar](50) NULL,
	[AccShortName] [varchar](50) NULL,
	[AccHistory] [varchar](50) NULL,
	[AccDbVisitDay1] [int] NULL,
	[AccDbVisitDay2] [int] NULL,
	[AccDbVisitDay3] [int] NULL,
	[AccStatement15Days] [varchar](50) NULL,
	[AccLessPercentInDebitNote] [numeric](2, 0) NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
 CONSTRAINT [PK_masteraccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterarea]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterarea](
	[AreaID] [int] IDENTITY(1,1) NOT NULL,
	[AreaName] [varchar](450) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_masterarea_1] PRIMARY KEY CLUSTERED 
(
	[AreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterbank]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterbank](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [varchar](50) NOT NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [varchar](45) NULL,
 CONSTRAINT [PK_masterbank] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterbanktype]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterbanktype](
	[ID] [int] NOT NULL,
	[AccountTypeName] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterbranch]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterbranch](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [varchar](50) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_masterbranch] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastercity]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastercity](
	[cityID] [int] IDENTITY(1,1) NOT NULL,
	[cityName] [varchar](450) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_mastercity_1] PRIMARY KEY CLUSTERED 
(
	[cityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastercompany]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastercompany](
	[CompID] [int] NOT NULL,
	[CompName] [varchar](50) NULL,
	[CompShortName] [varchar](3) NULL,
	[CompTelephone] [varchar](50) NULL,
	[CompMailId] [varchar](40) NULL,
	[CompContactPerson] [varchar](50) NULL,
	[CompAddress] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[PartyID_1] [int] NULL,
	[PartyID_2] [int] NULL,
	[PartyID_3] [int] NULL,
	[PartyID_4] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[Companycode] [varchar](32) NULL,
	[GlobalID] [varchar](32) NULL,
	[Tag] [varchar](5) NULL,
	[MaincompID] [int] NULL,
 CONSTRAINT [PK_mastercompany] PRIMARY KEY CLUSTERED 
(
	[CompID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastercustomer]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastercustomer](
	[CustomerID] [int] NOT NULL,
	[ID] [varchar](32) NULL,
	[CustomerNameAddress] [varchar](50) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterdelivaryboy]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterdelivaryboy](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[TelephoneNumber] [varchar](50) NULL,
	[MobileNumberForSMS] [varchar](13) NULL,
	[Email] [varchar](100) NULL,
	[Remarks] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](45) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterdelivarysalesman]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterdelivarysalesman](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[TelephoneNumber] [varchar](50) NULL,
	[MobileNumberForSMS] [varchar](13) NULL,
	[Email] [varchar](100) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](45) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterdoctor]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterdoctor](
	[DocID] [int] IDENTITY(1,1) NOT NULL,
	[DocName] [varchar](50) NULL,
	[DocAddress1] [nvarchar](50) NULL,
	[DocAddress2] [varchar](50) NULL,
	[DocTelephone] [varchar](50) NULL,
	[DocEmailID] [varchar](50) NULL,
	[DocShortNameAddress] [varchar](50) NULL,
	[DocDegree] [varchar](50) NULL,
	[DocRegistrationNumber] [varchar](20) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[DoctorCode] [varchar](10) NULL,
	[MobileNumberForSMS] [varchar](20) NULL,
 CONSTRAINT [PK_masterdoctor_1] PRIMARY KEY CLUSTERED 
(
	[DocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masteremail]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masteremail](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[EmailName] [varchar](50) NULL,
	[Description] [varchar](20) NULL,
	[CreatedUserId] [varchar](20) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](50) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
 CONSTRAINT [PK_masteremail] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastergenericcategory]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastergenericcategory](
	[GenericCategoryID] [int] NOT NULL,
	[ID] [varchar](32) NULL,
	[GenericCategoryName] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[DrugCode] [int] NULL,
 CONSTRAINT [PK_mastergenericcategory] PRIMARY KEY CLUSTERED 
(
	[GenericCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastergroup]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastergroup](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) NULL,
	[GroupCode] [varchar](1) NULL,
	[UnderGroupId] [varchar](32) NULL,
	[UnderGroupIDParentID] [varchar](32) NULL,
	[IFFIX] [varchar](1) NULL,
	[IFMainGroup] [varchar](1) NULL,
	[IfSubGroup] [varchar](1) NULL,
	[SerialNumber] [int] NULL,
	[LevelNumber] [decimal](18, 2) NULL,
	[BalanceSheetCode] [varchar](1) NULL,
	[ShowInBalanceSheet] [varchar](1) NULL,
	[BalanceSheetSrNumber] [numeric](10, 0) NULL,
	[OpeningDebit] [decimal](18, 2) NULL,
	[OpeningCredit] [decimal](18, 2) NULL,
	[TransactionDebit] [decimal](18, 2) NULL,
	[TransactionCredit] [decimal](18, 2) NULL,
	[ClosingDebit] [decimal](18, 2) NULL,
	[ClosingCredit] [decimal](18, 2) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_mastergroup] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterhospitalpatient]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterhospitalpatient](
	[ID] [int] NOT NULL,
	[InwardNumber] [varchar](20) NULL,
	[PatientName] [varchar](30) NULL,
	[Address1] [varchar](30) NULL,
	[Address2] [varchar](30) NULL,
	[ShortNameAddress] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[RoomNumber] [varchar](15) NULL,
	[IDNumber] [varchar](50) NULL,
	[BirthDay] [numeric](10, 0) NULL,
	[BirthMonth] [numeric](10, 0) NULL,
	[BirthYear] [numeric](10, 0) NULL,
	[AgeYears] [numeric](10, 0) NULL,
	[AgeMonths] [numeric](10, 0) NULL,
	[AgeDays] [numeric](10, 0) NULL,
	[Gender] [varchar](1) NULL,
	[WardID] [varchar](32) NULL,
	[DoctorID] [varchar](32) NULL,
	[PatientCategoryID] [varchar](32) NULL,
	[DateOfAdmission] [varchar](8) NULL,
	[DateofDischarge] [varchar](8) NULL,
	[AccountID] [varchar](32) NULL,
	[Remark1] [varchar](50) NULL,
	[Remark2] [varchar](50) NULL,
	[Remark3] [varchar](50) NULL,
	[StatementNumber] [numeric](10, 0) NULL,
	[StatementDate] [varchar](8) NULL,
	[StatementAmount] [decimal](18, 2) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastermaincompany]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastermaincompany](
	[MaincompID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](50) NULL,
	[MaincompAddress] [varchar](50) NULL,
	[MobilenumberSMS] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[EmailId] [varchar](max) NULL,
	[ContactPerson] [varchar](50) NULL,
	[AIOCDAnumber] [varchar](20) NULL,
	[Globalnumber] [varchar](20) NULL,
	[Gallinumber] [varchar](20) NULL,
	[CreatedID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[ModifiedID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
 CONSTRAINT [PK_mastermaincompany] PRIMARY KEY CLUSTERED 
(
	[MaincompID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastermessage]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastermessage](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](50) NULL,
	[Fromdate] [datetime2](7) NULL,
	[Todate] [datetime2](7) NULL,
	[CreatedUserID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
 CONSTRAINT [PK_mastermessage] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterpack]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterpack](
	[PackID] [int] NOT NULL,
	[PackName] [varchar](50) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_masterpack] PRIMARY KEY CLUSTERED 
(
	[PackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterpacktype]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterpacktype](
	[ID] [int] NOT NULL,
	[PackTypeName] [varchar](20) NULL,
 CONSTRAINT [PK_masterpacktype] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterproduct]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterproduct](
	[ProductID] [int] NOT NULL,
	[ProdName] [varchar](50) NULL,
	[ProdLoosePack] [int] NULL,
	[ProdPack] [varchar](6) NULL,
	[ProdPackType] [varchar](30) NULL,
	[ProdBoxQuantity] [int] NULL,
	[ProdCompShortName] [varchar](3) NULL,
	[ProdVATPercent] [decimal](18, 2) NULL,
	[ProdCST] [decimal](18, 2) NULL,
	[ProdCSTPercent] [decimal](18, 2) NULL,
	[ProdGrade] [char](1) NULL,
	[ProdScheduleDrugCode] [varchar](4) NULL,
	[ProdDPCOCode] [varchar](4) NULL,
	[ProdIfSchedule] [varchar](1) NULL,
	[ProdIfShortListed] [varchar](1) NULL,
	[ProdIfSaleDisc] [varchar](1) NULL,
	[ProdIfPurchaseRateInclusive] [varchar](1) NULL,
	[ProdIfMRPInclusive] [varchar](1) NULL,
	[ProdIfBarCodeRequired] [varchar](4) NULL,
	[ProdIfOctroi] [varchar](1) NULL,
	[ProdRequireColdStorage] [varchar](4) NULL,
	[ProdMinLevel] [int] NULL,
	[ProdMaxLevel] [int] NULL,
	[ProdMargin] [decimal](18, 2) NULL,
	[ProdLastPurchaseBillNumber] [varchar](32) NULL,
	[ProdLastPurchaseDate] [varchar](8) NULL,
	[ProdLastPurchasePartyId] [varchar](32) NULL,
	[ProdLastPurchaseVoucherType] [varchar](3) NULL,
	[ProdLastPurchaseVoucherNumber] [varchar](32) NULL,
	[ProdLastPurchaseRate] [decimal](18, 2) NULL,
	[ProdLastPurchaseTradeRate] [decimal](18, 2) NULL,
	[ProdLastPurchaseSaleRate] [decimal](18, 2) NULL,
	[ProdLastPurchasePTR] [decimal](18, 2) NULL,
	[ProdLastPurchaseCNF] [decimal](18, 2) NULL,
	[ProdLastPurchaseEcoMart] [decimal](18, 2) NULL,
	[ProdLastPurchaseMRP] [decimal](18, 2) NULL,
	[ProdLastPurchaseVATPer] [decimal](18, 2) NULL,
	[ProdLastPurchaseCSTPer] [decimal](18, 2) NULL,
	[ProdLastPurchaseCST] [decimal](18, 2) NULL,
	[ProdLastPurchaseSCMPer] [decimal](18, 2) NULL,
	[ProdLastPurchaseSCM] [decimal](18, 2) NULL,
	[ProdLastPurchaseItemDiscPer] [decimal](18, 2) NULL,
	[ProdLastPurchaseLocalTaxPer] [decimal](18, 2) NULL,
	[ProdLastPurchaseLocalTaxAmt] [decimal](18, 2) NULL,
	[ProdLastPurchaseExpiry] [varchar](5) NULL,
	[ProdLastPurchaseExpiryDate] [varchar](8) NULL,
	[ProdLastPurchaseBatchNumber] [varchar](15) NULL,
	[ProdLastPurchaseStockID] [varchar](32) NULL,
	[ProdOpeningStock] [int] NULL,
	[ProdClosingStock] [int] NULL,
	[ProdUserDefineCode] [varchar](4) NULL,
	[ProdSchemeOpeningQty] [int] NULL,
	[ProdSchemePurchaseQty] [int] NULL,
	[ProdSchemeSaleQty] [int] NULL,
	[ProdSchemeCRQty] [int] NULL,
	[ProdSchemeDBQty] [int] NULL,
	[ProdOctroiPer] [decimal](18, 2) NULL,
	[ProdLastSaleBillType] [varchar](3) NULL,
	[ProdLastSaleBillNumber] [int] NULL,
	[ProdLastSaleDate] [varchar](8) NULL,
	[ProdLastSalePartyId] [int] NULL,
	[ProdLastSaleStockID] [int] NULL,
	[ProdLastSaleScanID] [varchar](32) NULL,
	[TAG] [varchar](5) NULL,
	[MSCDACode] [varchar](32) NULL,
	[SSOpeningStock] [int] NULL,
	[SSPurchaseStock] [int] NULL,
	[SSSaleStock] [int] NULL,
	[SSCRStock] [int] NULL,
	[SSDRStock] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[productCode] [varchar](32) NULL,
	[companyCode] [varchar](32) NULL,
	[GlobalID] [int] NULL,
	[opstock] [int] NULL,
	[purstock] [int] NULL,
	[salestock] [int] NULL,
	[crstock] [int] NULL,
	[dbstock] [int] NULL,
	[PacktypeId] [int] NULL,
	[ProdCompID] [int] NULL,
	[ProdShelfID] [int] NULL,
	[ProdDrugID] [int] NULL,
	[ProdCategoryID] [int] NULL,
	[ProdLBTID] [int] NULL,
	[ProdPartyId_1] [int] NULL,
	[ProdPartyId_2] [int] NULL,
	[ProdTaxID] [int] NULL,
	[prodmrp] [decimal](18, 0) NULL,
	[HSNNumber] [numeric](18, 0) NULL,
	[ScannedBarcode] [numeric](18, 0) NULL,
 CONSTRAINT [PK_masterproduct] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterproductcategory]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterproductcategory](
	[ProductCategoryID] [int] NOT NULL,
	[ID] [varchar](32) NULL,
	[ProductCategoryName] [varchar](50) NULL,
	[SaleDiscount] [varchar](20) NULL,
	[LBTPercent] [decimal](18, 2) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[catcode] [varchar](32) NULL,
 CONSTRAINT [PK_masterproductcategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterpurchaseordercnf]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterpurchaseordercnf](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountID] [int] NULL,
	[CompanyID] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[if_uploaded] [varchar](1) NULL,
	[CreatedUserID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
 CONSTRAINT [PK_masterpurchaseordercnf] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterpurchaseorderecomart]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterpurchaseorderecomart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[AccountID] [int] NULL,
	[CompanyID] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[if_uploaded] [varchar](1) NULL,
	[CreatedUserID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
 CONSTRAINT [PK_masterpurchaseorderecomart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterpurchaseorderstockist]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterpurchaseorderstockist](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountID] [int] NULL,
	[CompanyID] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[if_uploaded] [varchar](1) NULL,
	[CreatedUserID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
 CONSTRAINT [PK_masterpurchaseorder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastersalesman]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastersalesman](
	[SalesmanID] [int] IDENTITY(1,1) NOT NULL,
	[SalesmanName] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[TelephoneNumber] [varchar](50) NULL,
	[MobileNumberForSMS] [varchar](13) NULL,
	[Email] [varchar](100) NULL,
	[Remarks] [varchar](200) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](45) NULL,
 CONSTRAINT [PK_mastersalesman] PRIMARY KEY CLUSTERED 
(
	[SalesmanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastersaletype]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastersaletype](
	[SaleTypeId] [int] NOT NULL,
	[SaleTypeName] [varchar](25) NULL,
	[Status] [varchar](25) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterscheduleddrug]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterscheduleddrug](
	[ScheduleID] [int] NOT NULL,
	[ScheduleCode] [varchar](1) NULL,
	[ScheduleName] [varchar](20) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[masterscheme]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[masterscheme](
	[ID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[ProductQuantity1] [int] NULL,
	[SchemeQuantity1] [int] NULL,
	[ProductQuantity2] [int] NULL,
	[SchemeQuantity2] [int] NULL,
	[ProductQuantity3] [int] NULL,
	[SchemeQuantity3] [int] NULL,
	[SchemeDiscountPercent] [decimal](18, 2) NULL,
	[StartingDate] [date] NULL,
	[ClosingDate] [date] NULL,
	[IfSchemeClosed] [varchar](1) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastershelf]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastershelf](
	[ShelfID] [int] IDENTITY(1,1) NOT NULL,
	[ShelfCode] [varchar](8) NOT NULL,
	[ShelfDescription] [varchar](50) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [varchar](32) NULL,
 CONSTRAINT [PK_mastershelf] PRIMARY KEY CLUSTERED 
(
	[ShelfID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastertransactiontype]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastertransactiontype](
	[TransactionTypeId] [int] NOT NULL,
	[TransactionTypeName] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mastertransport]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mastertransport](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[TransportAddress] [varchar](50) NULL,
	[MobileNumberSMS] [varchar](20) NULL,
	[Telephone] [varchar](20) NULL,
	[EmailId] [varchar](20) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMode]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMode](
	[PayModeID] [int] NOT NULL,
	[PayModeOption] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentMode] PRIMARY KEY CLUSTERED 
(
	[PayModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblaccountingyear]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblaccountingyear](
	[VoucherSeries] [varchar](4) NOT NULL,
	[FromDate] [varchar](8) NULL,
	[ToDate] [varchar](8) NULL,
	[YearEndOver] [varchar](1) NULL,
	[CurrentYear] [varchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblaccounttype]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblaccounttype](
	[AccountTypeID] [int] NOT NULL,
	[AccountType] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblbillimportlink]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblbillimportlink](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[CompanyProductID] [int] NULL,
	[EcoMartProductID] [int] NULL,
 CONSTRAINT [PK_tblbillimportlink] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblcollectionnote]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcollectionnote](
	[ID] [int] NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [int] NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[SalesmanId] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbldeliverynote]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbldeliverynote](
	[ID] [int] NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [int] NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[SalesmanId] [int] NULL,
	[AreaId] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblemailtype]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblemailtype](
	[EmailTypeID] [int] NOT NULL,
	[Status] [varchar](25) NULL,
	[EmailName] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblfavourite]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblfavourite](
	[FavouriteId] [varchar](32) NOT NULL,
	[FavName] [varchar](250) NULL,
	[FavControlName] [varchar](250) NULL,
	[FavOperationMode] [int] NULL,
	[FavIndex] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblfixaccounts]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblfixaccounts](
	[AccountID] [varchar](32) NOT NULL,
	[AccCode] [char](1) NULL,
	[AccName] [varchar](50) NULL,
	[AccOpeningDebit] [decimal](18, 2) NULL,
	[AccOpeningCredit] [decimal](18, 2) NULL,
	[AccGroupID] [varchar](32) NULL,
	[AccDoctorID] [varchar](32) NULL,
	[AccAreaID] [varchar](32) NULL,
	[AccTransactionDebit] [decimal](18, 2) NULL,
	[AccTransactionCredit] [decimal](18, 2) NULL,
	[AccClosingDebit] [decimal](18, 2) NULL,
	[AccClosingCredit] [decimal](18, 2) NULL,
	[AccClearedAmount] [decimal](18, 2) NULL,
	[AccLastVoucherNumber] [decimal](18, 2) NULL,
	[AccVoucherDate] [varchar](8) NULL,
	[AccShortName] [varchar](3) NULL,
	[AccNameAddress] [varchar](50) NULL,
	[AccBirthDay] [numeric](10, 0) NULL,
	[AccBirthMonth] [numeric](10, 0) NULL,
	[AccBirthYear] [numeric](10, 0) NULL,
	[AccHistory] [varchar](250) NULL,
	[AccVATTinNumber] [varchar](50) NULL,
	[AccDLN] [varchar](50) NULL,
	[AccPAN] [varchar](50) NULL,
	[AccEmailID] [varchar](40) NULL,
	[AccTokenNumber] [int] NULL,
	[IPartyID] [numeric](10, 0) NULL,
	[AccNumber] [varchar](32) NULL,
	[AccRemark] [varchar](255) NULL,
	[AccBankAccountNumber] [varchar](20) NULL,
	[AccDbVisitDay1] [numeric](10, 0) NULL,
	[AccDbVisitDay2] [numeric](10, 0) NULL,
	[AccDbVisitDay3] [numeric](10, 0) NULL,
	[IFFIX] [varchar](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[AccountCode] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblformulae]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblformulae](
	[ID] [varchar](32) NULL,
	[FormulaName] [varchar](30) NOT NULL,
	[Formula] [varchar](100) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifyDate] [varchar](8) NULL,
	[ModifyUserID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbloperator]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbloperator](
	[OperatorID] [varchar](32) NOT NULL,
	[OperatorName] [varchar](50) NULL,
	[Password] [varchar](4) NULL,
	[IFInUse] [tinyint] NULL,
	[OperatorDetails] [varchar](50) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblpharmasysdistpluslic]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblpharmasysdistpluslic](
	[EcoMartID] [varchar](32) NOT NULL,
	[Data] [text] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblschedule]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblschedule](
	[ID] [varchar](4) NOT NULL,
	[ScheduleCode] [varchar](4) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblsettings]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblsettings](
	[Id] [int] NOT NULL,
	[setPurchaseCopyPurchaseOrder] [varchar](1) NULL,
	[setPurchaseRounding] [varchar](1) NULL,
	[setPurchaseReadPurchaseOrder] [varchar](1) NULL,
	[setPurchaseChangeSaleRate] [varchar](1) NULL,
	[setPurchaseAllowExpiredItems] [varchar](1) NULL,
	[setPurchaseHold] [varchar](1) NULL,
	[setPurchaseIfPTR] [varchar](1) NULL,
	[setSaleRoundOff] [varchar](1) NULL,
	[setSaleCreditStatement] [varchar](1) NULL,
	[setSaleAllowBackDate] [varchar](1) NULL,
	[setSaleTenderAmount] [varchar](1) NULL,
	[setSaleShowProfitInSaleBill] [varchar](1) NULL,
	[setSaleMaxDiscount] [decimal](18, 2) NULL,
	[setSaleGetOrderNumberDate] [varchar](1) NULL,
	[setSaleGetDoctor] [varchar](1) NULL,
	[setSaleGetDelivaryBoy] [varchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblshopdetails]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblshopdetails](
	[ShopID] [int] NOT NULL,
	[ShopOwnerName] [varchar](30) NULL,
	[ShopName] [varchar](30) NULL,
	[AIOCDACode] [varchar](30) NULL,
	[SCORGCode] [varchar](30) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[MobileNumber] [varchar](50) NULL,
	[EmailID] [varchar](50) NULL,
	[SGST] [varchar](30) NULL,
	[CGST] [varchar](30) NULL,
	[DrugLicNo] [varchar](100) NULL,
	[Jurisdication] [varchar](50) NULL,
	[CreatedDate] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblstock]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblstock](
	[StockID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[BatchNumber] [varchar](15) NOT NULL,
	[MRP] [decimal](18, 2) NOT NULL,
	[Expiry] [varchar](5) NULL,
	[ExpiryDate] [varchar](32) NULL,
	[TradeRate] [decimal](18, 2) NULL,
	[PurchaseRate] [decimal](18, 2) NULL,
	[SaleRate] [decimal](18, 2) NULL,
	[EcoMartRate] [decimal](18, 2) NULL,
	[CNFRate] [decimal](18, 2) NULL,
	[StockistRate] [decimal](18, 2) NULL,
	[IFBreakageStock] [bit] NULL,
	[OpeningStock] [int] NULL,
	[ClosingStock] [int] NULL,
	[PurchaseStock] [int] NULL,
	[TransferInStock] [int] NULL,
	[CreditNoteStock] [int] NULL,
	[SaleStock] [int] NULL,
	[TransferOutStock] [int] NULL,
	[DebitNoteStock] [int] NULL,
	[PurchaseSchemeStock] [int] NULL,
	[PurchaseReplacementStock] [int] NULL,
	[SaleSchemeStock] [int] NULL,
	[IfRateCorrection] [char](1) NULL,
	[ProductVATPercent] [decimal](18, 2) NULL,
	[PurchaseVATPercent] [decimal](18, 2) NULL,
	[LastPurchaseAccountId] [int] NULL,
	[LastPurchaseBillNumber] [varchar](15) NULL,
	[LastPurchaseDate] [varchar](32) NULL,
	[LastPurchaseVoucherType] [varchar](3) NULL,
	[LastPurchaseVoucherNumber] [int] NULL,
	[PriceToRetailer] [decimal](18, 2) NULL,
	[ProfitPercent] [decimal](18, 2) NULL,
	[Margin] [decimal](18, 2) NULL,
	[ScanCode] [varchar](20) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[IsHold] [varchar](50) NULL,
	[IsLastPusrchseBatch] [varchar](50) NULL,
	[LastSaleDate] [varchar](32) NULL,
	[PurchaseId] [int] NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
 CONSTRAINT [PK_tblstock] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbltempstock]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbltempstock](
	[TempStockID] [varchar](32) NOT NULL,
	[StockID] [varchar](32) NOT NULL,
	[ProductID] [varchar](32) NOT NULL,
	[SoldQuantity] [int] NULL,
	[ModuleNumber] [int] NULL,
	[CompName] [varchar](250) NOT NULL,
	[Mode] [int] NULL,
	[CustomerNumber] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbltrnac]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbltrnac](
	[tblTrnacID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherID] [int] NULL,
	[AccountID] [int] NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[AccAccountID] [int] NULL,
	[TransactionDate] [varchar](32) NULL,
	[ReferenceVoucherID] [int] NULL,
	[ReferenceVoucherType] [varchar](32) NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](6) NULL,
	[VoucherSubType] [varchar](1) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[Narration] [varchar](50) NULL,
	[ShortName] [varchar](50) NULL,
	[ChequeNumber] [varchar](50) NULL,
	[ChequeDate] [varchar](32) NULL,
	[ClearedDate] [varchar](32) NULL,
	[BankID] [int] NULL,
	[BankName] [varchar](80) NULL,
	[BranchID] [int] NULL,
	[BranchName] [varchar](80) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
 CONSTRAINT [PK_tbltrnac] PRIMARY KEY CLUSTERED 
(
	[tblTrnacID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbluser]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbluser](
	[UserID] [int] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](8) NULL,
	[IfInUse] [bit] NULL,
	[Level] [int] NULL,
	[makeitdefault] [varchar](1) NULL,
	[UserDetails] [varchar](50) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[MobileNumber] [varchar](32) NULL,
	[EmailID] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbluserlevel]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbluserlevel](
	[ID] [int] NULL,
	[Type] [varchar](15) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbluserrights]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbluserrights](
	[ID] [int] NULL,
	[FormName] [varchar](500) NULL,
	[AddModule] [varchar](3) NULL,
	[DeleteModule] [varchar](3) NULL,
	[EditModule] [varchar](3) NULL,
	[ViewModule] [varchar](3) NULL,
	[PrintModule] [varchar](3) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedTime] [varchar](8) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](8) NULL,
	[ModifiedTime] [varchar](8) NULL,
	[ModifiedUserID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblvat]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblvat](
	[ID] [int] NULL,
	[VATPercentage] [decimal](18, 2) NULL,
	[VATAmount] [decimal](18, 2) NULL,
	[SaleAmount] [decimal](18, 2) NULL,
	[PurchaseAmount] [decimal](18, 2) NULL,
	[VATActive] [char](1) NULL,
	[CreatedDate] [varchar](8) NULL,
	[CreatedBy] [varchar](32) NULL,
	[ModifyDate] [varchar](8) NULL,
	[ModifyBy] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblvouchernumbers]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblvouchernumbers](
	[ID] [int] NOT NULL,
	[PurchaseCredit] [int] NULL,
	[PurchaseCashCredit] [int] NULL,
	[PurchaseCash] [int] NULL,
	[PurchaseOrder] [int] NULL,
	[SaleChitNumber] [int] NULL,
	[SaleCash] [int] NULL,
	[SaleCredit] [int] NULL,
	[SaleCashCredit] [int] NULL,
	[SaleChallan] [int] NULL,
	[DebitNote] [int] NULL,
	[CreditNote] [int] NULL,
	[CashReceipt] [int] NULL,
	[BankReceipt] [int] NULL,
	[CashPaid] [int] NULL,
	[BankPaid] [int] NULL,
	[BankExpenses] [int] NULL,
	[CashExpenses] [int] NULL,
	[StockIn] [int] NULL,
	[StockOut] [int] NULL,
	[OpeningStock] [int] NULL,
	[TokenNumber] [int] NULL,
	[CorrectionInRate] [int] NULL,
	[ChequeReturn] [int] NULL,
	[JournalVoucher] [int] NULL,
	[StatementPurchase] [int] NULL,
	[StatementSale] [int] NULL,
	[ContraEntry] [int] NULL,
	[SlipNumber] [int] NULL,
	[Quation] [int] NULL,
	[CreditNoteBreakageExpiry] [int] NULL,
	[CollectionNote] [int] NULL,
	[DeliveryNote] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblvoucherpurchasetax]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblvoucherpurchasetax](
	[tblVoucherPurchaseTax_id] [int] NULL,
	[VoucherPurchaseID] [int] NULL,
	[MasterTaxID] [int] NULL,
	[VoucherType] [varchar](10) NULL,
	[TaxValue] [decimal](18, 2) NULL,
	[TaxAmount] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblvouchertypes]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblvouchertypes](
	[ID] [varchar](2) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[Description] [varchar](50) NULL,
	[code] [varchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucherbreakageexpiry]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucherbreakageexpiry](
	[ID] [int] NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountId] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[DiscountPer] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[RoundingAmount] [decimal](18, 2) NULL,
	[VAT5] [decimal](18, 2) NULL,
	[VAT12point5] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](50) NULL,
	[ClearedInID] [varchar](32) NULL,
	[ClearedInVoucherSeries] [varchar](4) NULL,
	[ClearedInVoucherType] [varchar](4) NULL,
	[ClearedInVoucherNumber] [int] NULL,
	[ClearedInVoucherDate] [varchar](8) NULL,
	[ClearedInPurchaseBillNumber] [varchar](15) NULL,
	[OperatorID] [varchar](32) NULL,
	[Uploaded] [char](1) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vouchercashbankexpenses]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vouchercashbankexpenses](
	[ID] [int] NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [varchar](32) NULL,
	[ChequeDepositedBankID] [int] NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](100) NULL,
	[BankSlipNumber] [int] NULL,
	[BankSlipDate] [varchar](32) NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL,
	[BillNO] [int] NULL,
	[BillDate] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vouchercashbankpayment]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vouchercashbankpayment](
	[ID] [int] NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](50) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ClearedDate] [varchar](8) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](50) NULL,
	[ChequeDate] [varchar](32) NULL,
	[ChequeDepositedBankID] [int] NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](500) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [varchar](32) NULL,
	[OperatorID] [varchar](32) NULL,
	[JVNumber] [int] NULL,
	[JVID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL,
	[CBID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_vouchercashbankpayment] PRIMARY KEY CLUSTERED 
(
	[CBID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vouchercashbankreceipt]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vouchercashbankreceipt](
	[ID] [int] NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [varchar](32) NULL,
	[ChequeDepositedBankID] [int] NULL,
	[ClearedDate] [varchar](32) NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](100) NULL,
	[JVNumber] [int] NULL,
	[JVID] [int] NULL,
	[BankSlipNumber] [int] NULL,
	[BankSlipDate] [varchar](32) NULL,
	[IfChequeReturn] [varchar](4) NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL,
	[CBID] [int] IDENTITY(1,1) NOT NULL,
	[CBPaymodeID] [int] NULL,
 CONSTRAINT [PK_vouchercashbankreceipt] PRIMARY KEY CLUSTERED 
(
	[CBID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucherchequereturn]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucherchequereturn](
	[ID] [int] NOT NULL,
	[ChequeReturnVoucherSeries] [int] NULL,
	[ChequeReturnVoucherNumber] [int] NULL,
	[ChequeReturnVoucherType] [varchar](3) NULL,
	[ChequeReturnVoucherDate] [varchar](32) NULL,
	[ChequeReturnCharges] [decimal](18, 2) NULL,
	[BKRID] [int] NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](5) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[OnAccountAmount] [decimal](18, 2) NULL,
	[ExtraAmount] [decimal](18, 2) NULL,
	[ChequeNumber] [varchar](20) NULL,
	[ChequeDate] [varchar](32) NULL,
	[ChequeDepositedBankID] [int] NULL,
	[CustomerBankID] [int] NULL,
	[CustomerBranchID] [int] NULL,
	[Narration] [varchar](100) NULL,
	[BankSlipNumber] [numeric](10, 0) NULL,
	[BankSlipDate] [varchar](32) NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vouchercorrectioninrate]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vouchercorrectioninrate](
	[ID] [int] NULL,
	[OldStockID] [int] NULL,
	[NewStockID] [int] NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[VoucherSeries] [int] NULL,
	[ProductID] [int] NULL,
	[OldBatch] [varchar](15) NULL,
	[OldExpiry] [varchar](5) NULL,
	[OldMRP] [decimal](18, 0) NULL,
	[OldPurchaseRate] [decimal](18, 0) NULL,
	[OldSaleRate] [decimal](18, 0) NULL,
	[OldQuantity] [int] NULL,
	[NewBatch] [varchar](15) NULL,
	[NewExpiry] [varchar](5) NULL,
	[NewMRP] [decimal](18, 0) NULL,
	[NewPurchaseRate] [decimal](18, 0) NULL,
	[NewSaleRate] [decimal](18, 0) NULL,
	[NewQuantity] [int] NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vouchercreditdebitnote]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vouchercreditdebitnote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[AccountId] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[DiscountPer] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[RoundingAmount] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[ClearedInID] [int] NULL,
	[ClearedInVoucherSeries] [int] NULL,
	[ClearedInVoucherType] [varchar](4) NULL,
	[ClearedInVoucherNumber] [int] NULL,
	[ClearedInVoucherDate] [varchar](32) NULL,
	[ClearedInPurchaseBillNumber] [int] NULL,
	[OperatorID] [varchar](32) NULL,
	[Uploaded] [char](1) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL,
	[IsHold] [char](32) NULL,
	[TransferToAcc] [varchar](2) NULL,
	[AmountGST0] [decimal](18, 2) NULL,
	[AmountGSTS5] [decimal](18, 2) NULL,
	[AmountGSTS12] [decimal](18, 2) NULL,
	[AmountGSTS18] [decimal](18, 2) NULL,
	[AmountGSTS28] [decimal](18, 2) NULL,
	[AmountGSTC5] [decimal](18, 2) NULL,
	[AmountGSTC12] [decimal](18, 2) NULL,
	[AmountGSTC18] [decimal](18, 2) NULL,
	[AmountGSTC28] [decimal](18, 2) NULL,
	[GSTS5] [decimal](18, 2) NULL,
	[GSTS12] [decimal](18, 2) NULL,
	[GSTS18] [decimal](18, 2) NULL,
	[GSTS28] [decimal](18, 2) NULL,
	[GSTC5] [decimal](18, 2) NULL,
	[GSTC12] [decimal](18, 2) NULL,
	[GSTC18] [decimal](18, 2) NULL,
	[GSTC28] [decimal](18, 2) NULL,
	[AmountGSTI5] [decimal](18, 2) NULL,
	[AmountGSTI12] [decimal](18, 2) NULL,
	[AmountGSTI18] [decimal](18, 2) NULL,
	[AmountGSTI28] [decimal](18, 2) NULL,
	[GSTI5] [decimal](18, 2) NULL,
	[GSTI12] [decimal](18, 2) NULL,
	[GSTI18] [decimal](18, 2) NULL,
	[GSTI28] [decimal](18, 2) NULL,
 CONSTRAINT [PK_vouchercreditdebitnote] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucherjv]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucherjv](
	[ID] [int] NOT NULL,
	[AccountID] [int] NULL,
	[VoucherType] [varchar](6) NULL,
	[VoucherSeries] [int] NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[Debit] [decimal](18, 2) NULL,
	[Credit] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[ReferenceVoucherID] [int] NULL,
	[OperatorID] [varchar](32) NULL,
	[CreatedUserID] [varchar](32) NULL,
	[CreatedDate] [varchar](20) NULL,
	[CreatedTime] [varchar](20) NULL,
	[ModifiedUserID] [varchar](32) NULL,
	[ModifiedDate] [varchar](20) NULL,
	[ModifiedTime] [varchar](20) NULL,
	[ModifiedOperatorID] [varchar](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucheropstock]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucheropstock](
	[MasterID] [int] NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](0) NULL,
	[CreatedUserId] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](0) NULL,
	[ModifiedUserId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucherpurchase]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucherpurchase](
	[purchaseID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherSeries] [int] NULL,
	[VoucherType] [varchar](4) NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[PurchaseBillNumber] [varchar](20) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[AmountGross] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NULL,
	[CashDiscountPercentage] [decimal](18, 2) NULL,
	[SchemeDiscountPercentage] [decimal](18, 2) NULL,
	[SurchargePercentage] [decimal](18, 2) NULL,
	[AmountAddOn] [decimal](18, 2) NULL,
	[AmountFreight] [decimal](18, 2) NULL,
	[AmountExcise] [decimal](18, 2) NULL,
	[AmountTOD] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
	[AmountDebitNote] [decimal](18, 2) NULL,
	[DueDate] [varchar](32) NULL,
	[Narration] [varchar](100) NULL,
	[EntryDate] [varchar](32) NULL,
	[RoundUpAmount] [decimal](18, 2) NULL,
	[IsHold] [varchar](1) NULL,
	[AmountGST0] [decimal](18, 2) NULL,
	[AmountGSTS5] [decimal](18, 2) NULL,
	[AmountGSTS12] [decimal](18, 2) NULL,
	[AmountGSTS18] [decimal](18, 2) NULL,
	[AmountGSTS28] [decimal](18, 2) NULL,
	[AmountGSTS48] [decimal](18, 2) NULL,
	[AmountGSTC5] [decimal](18, 2) NULL,
	[AmountGSTC12] [decimal](18, 2) NULL,
	[AmountGSTC18] [decimal](18, 2) NULL,
	[AmountGSTC28] [decimal](18, 2) NULL,
	[AmountGSTC48] [decimal](18, 2) NULL,
	[GSTS5] [decimal](18, 2) NULL,
	[GSTS12] [decimal](18, 2) NULL,
	[GSTS18] [decimal](18, 2) NULL,
	[GSTS28] [decimal](18, 2) NULL,
	[GSTS48] [decimal](18, 2) NULL,
	[GSTC5] [decimal](18, 2) NULL,
	[GSTC12] [decimal](18, 2) NULL,
	[GSTC18] [decimal](18, 2) NULL,
	[GSTC28] [decimal](18, 2) NULL,
	[GSTC48] [decimal](18, 2) NULL,
	[AmountGSTI5] [decimal](18, 2) NULL,
	[AmountGSTI12] [decimal](18, 2) NULL,
	[AmountGSTI18] [decimal](18, 2) NULL,
	[AmountGSTI28] [decimal](18, 2) NULL,
	[AmountGSTI48] [decimal](18, 2) NULL,
	[GSTI5] [decimal](18, 2) NULL,
	[GSTI12] [decimal](18, 2) NULL,
	[GSTI18] [decimal](18, 2) NULL,
	[GSTI28] [decimal](18, 2) NULL,
	[GSTI48] [decimal](18, 2) NULL,
	[OperatorID] [int] NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](32) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL,
	[MSCDACodeForAccount] [varchar](50) NULL,
 CONSTRAINT [PK_voucherpurchase] PRIMARY KEY CLUSTERED 
(
	[purchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[voucherquotaion]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[voucherquotaion](
	[ID] [int] NOT NULL,
	[AccountID] [int] NULL,
	[PartyName] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [int] NULL,
	[VoucherNumber] [int] NULL,
	[VoucherDate] [date] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountGross] [decimal](18, 2) NULL,
	[AmountDiscount] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[OperatorID] [int] NULL,
	[CreatedUserID] [int] NULL,
	[CreatedDate] [date] NULL,
	[CreatedTime] [time](7) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedTime] [time](7) NULL,
	[ModifiedOperatorID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vouchersale]    Script Date: 15/07/2022 16:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vouchersale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherType] [varchar](3) NULL,
	[VoucherSeries] [int] NULL,
	[VoucherNumber] [int] NULL,
	[CounterSaleNumber] [int] NULL,
	[VoucherDate] [varchar](32) NULL,
	[VoucherSubType] [varchar](1) NULL,
	[AccountID] [int] NULL,
	[AmountNet] [decimal](18, 2) NULL,
	[AmountClear] [decimal](18, 2) NULL,
	[AmountBalance] [decimal](18, 2) NULL,
	[AmountGross] [decimal](18, 2) NULL,
	[CashDiscountPercent] [decimal](18, 2) NULL,
	[AmountCashDiscount5] [decimal](18, 2) NULL,
	[AmountCashDiscount12point5] [decimal](18, 2) NULL,
	[AmountSpecialDiscount] [decimal](18, 2) NULL,
	[AmountSchemeDiscount] [decimal](18, 2) NULL,
	[AmountCashDiscount] [decimal](18, 2) NOT NULL,
	[AmountPMTDiscount] [decimal](18, 2) NULL,
	[AmountItemDiscount] [decimal](18, 2) NULL,
	[AddOnFreight] [decimal](18, 2) NULL,
	[AmountCreditNote] [decimal](18, 2) NULL,
	[AmountDebitNote] [decimal](18, 2) NULL,
	[OctroiPercentage] [decimal](18, 2) NULL,
	[AmountOctroi] [decimal](18, 2) NULL,
	[Narration] [varchar](100) NULL,
	[StatementNumber] [int] NULL,
	[StatementID] [int] NULL,
	[DoctorID] [int] NULL,
	[PatientID] [int] NULL,
	[OperatorID] [int] NULL,
	[ScanPrescriptionID] [int] NULL,
	[ScanPrescriptionFileName] [varchar](250) NULL,
	[PatientName] [varchar](50) NULL,
	[PatientAddress1] [varchar](50) NULL,
	[PatientAddress2] [varchar](50) NULL,
	[PatientShortName] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[DoctorShortName] [varchar](50) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderDate] [varchar](32) NULL,
	[IPDOPDCode] [varchar](1) NULL,
	[VAT5Per] [decimal](18, 2) NULL,
	[AmountVAT5Per] [decimal](18, 2) NULL,
	[VAT12Point5Per] [decimal](18, 2) NULL,
	[AmountVAT12Point5Per] [decimal](18, 2) NULL,
	[AmountForZeroVAT] [decimal](18, 2) NULL,
	[RoundingAmount] [decimal](18, 2) NULL,
	[DiscountAmountCB] [decimal](18, 2) NULL,
	[ProfitInRupees] [decimal](18, 2) NULL,
	[ProfitPercentBySaleRate] [decimal](18, 2) NULL,
	[ProfitPercentByPurchaseRate] [decimal](18, 2) NULL,
	[CashierCheck] [varchar](4) NULL,
	[CreatedDate] [varchar](32) NULL,
	[CreatedTime] [varchar](50) NULL,
	[CreatedUserID] [int] NULL,
	[ModifiedDate] [varchar](32) NULL,
	[ModifiedTime] [varchar](32) NULL,
	[ModifiedUserID] [int] NULL,
	[ModifiedOperatorID] [int] NULL,
	[VoucherSaleID] [int] NULL,
	[TransactionalCode] [varchar](45) NULL,
	[SurchargePercentage] [decimal](18, 2) NULL,
	[LRNumber] [varchar](45) NULL,
	[LRDate] [varchar](32) NULL,
	[NoOfCases] [int] NULL,
	[AmountSplDicount] [decimal](18, 2) NULL,
	[Narration2] [varchar](45) NULL,
	[DelivaryBoyId] [int] NULL,
	[TransporterID] [int] NULL,
	[Addper] [decimal](18, 2) NULL,
	[SalesManID] [int] NULL,
	[CompanyID] [int] NULL,
	[TotalFinalVat] [decimal](18, 2) NULL,
	[NumberOfTimesPrinted] [smallint] NULL,
	[DeliveryDate] [varchar](32) NULL,
	[DeliverySalsmanID] [int] NULL,
	[ISsentByEmail] [int] NULL,
	[AmountCreditnoteTAXFirst] [decimal](18, 2) NULL,
	[AmountCreditnoteTAXSecond] [decimal](18, 2) NULL,
	[CollectionNoteNumber] [int] NULL,
	[DeliveryNotenumber] [int] NULL,
	[DeliveryNoteDate] [varchar](32) NULL,
	[DeliveryorCounter] [varchar](50) NULL,
	[AmountCreditNoteStock] [decimal](18, 2) NULL,
	[AmountGST0] [decimal](18, 2) NULL,
	[AmountGSTS5] [decimal](18, 2) NULL,
	[AmountGSTS12] [decimal](18, 2) NULL,
	[AmountGSTS18] [decimal](18, 2) NULL,
	[AmountGSTS28] [decimal](18, 2) NULL,
	[AmountGSTS48] [decimal](18, 2) NULL,
	[AmountGSTC5] [decimal](18, 2) NULL,
	[AmountGSTC12] [decimal](18, 2) NULL,
	[AmountGSTC18] [decimal](18, 2) NULL,
	[AmountGSTC28] [decimal](18, 2) NULL,
	[AmountGSTC48] [decimal](18, 2) NULL,
	[GSTS5] [decimal](18, 2) NULL,
	[GSTS12] [decimal](18, 2) NULL,
	[GSTS18] [decimal](18, 2) NULL,
	[GSTS28] [decimal](18, 2) NULL,
	[GSTS48] [decimal](18, 2) NULL,
	[GSTC5] [decimal](18, 2) NULL,
	[GSTC12] [decimal](18, 2) NULL,
	[GSTC18] [decimal](18, 2) NULL,
	[GSTC28] [decimal](18, 2) NULL,
	[GSTC48] [decimal](18, 2) NULL,
	[AmountGSTI5] [decimal](18, 2) NULL,
	[AmountGSTI12] [decimal](18, 2) NULL,
	[AmountGSTI18] [decimal](18, 2) NULL,
	[AmountGSTI28] [decimal](18, 2) NULL,
	[AmountGSTI48] [decimal](18, 2) NULL,
	[GSTI5] [decimal](18, 2) NULL,
	[GSTI12] [decimal](18, 2) NULL,
	[GSTI18] [decimal](18, 2) NULL,
	[GSTI28] [decimal](18, 2) NULL,
	[GSTI48] [decimal](18, 2) NULL,
	[EcoMartID] [int] NULL,
	[CNFID] [int] NULL,
	[StockistID] [int] NULL,
	[IFDownLoaded] [char](1) NULL,
 CONSTRAINT [PK_vouchersale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[changeddetailpurchase] ON 

INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (1, N'8F1FBA42717D4DD79463537C060B2776', N'518A935301664928949E7C8FDFC36CF2', N'365BB869611D489EB7F4B404A700DE29', N'1A79D71FF3D948648645B4EA61A30C57', N'0000', CAST(18.00 AS Decimal(18, 2)), CAST(18.18 AS Decimal(18, 2)), CAST(75.00 AS Decimal(18, 2)), CAST(20.70 AS Decimal(18, 2)), N'00/00', N'', CAST(10 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.50 AS Decimal(18, 2)), CAST(12.50 AS Decimal(18, 2)), CAST(12.17 AS Decimal(18, 2)), CAST(13.86 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', N' ', 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (2, N'6A6A924B3C1447C6A6C2869A521B2F5B', N'838B45D5A1A84874A5C5618D1C52D7FE', N'6A618B0126DD4E679D5FB9C045803B66', N'CA8ED3F152F34F71A07D29BAB159FAB8', N'433', CAST(78.00 AS Decimal(18, 2)), CAST(78.00 AS Decimal(18, 2)), CAST(312.00 AS Decimal(18, 2)), CAST(87.36 AS Decimal(18, 2)), N'10/18', N'20181001', CAST(10 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(10.71 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', N' ', 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (3, N'8F1FBA42717D4DD79463537C060B2776', N'518A935301664928949E7C8FDFC36CF2', N'D35B72E1BF224EFEA4D5895E56AC5450', N'56B9EBCF1C8B409D8223EF67E64294F0', N'MLYGCI01', CAST(90.85 AS Decimal(18, 2)), CAST(89.49 AS Decimal(18, 2)), CAST(275.00 AS Decimal(18, 2)), CAST(101.75 AS Decimal(18, 2)), N'07/16', N'20160701', CAST(10 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(2.00 AS Decimal(18, 2)), CAST(1.82 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(12.05 AS Decimal(18, 2)), CAST(13.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', N' ', 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (4, N'B40783FEA72243EBBFB8DC01E7BDE905', N'518A935301664928949E7C8FDFC36CF2', N'D35B72E1BF224EFEA4D5895E56AC5450', N'56B9EBCF1C8B409D8223EF67E64294F0', N'MLYGCI01', CAST(90.85 AS Decimal(18, 2)), CAST(89.49 AS Decimal(18, 2)), CAST(275.00 AS Decimal(18, 2)), CAST(101.75 AS Decimal(18, 2)), N'07/16', N'20160701', CAST(10 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(2.00 AS Decimal(18, 2)), CAST(1.82 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(12.05 AS Decimal(18, 2)), CAST(13.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', N' ', 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (5, N'B40783FEA72243EBBFB8DC01E7BDE905', N'518A935301664928949E7C8FDFC36CF2', N'365BB869611D489EB7F4B404A700DE29', N'1A79D71FF3D948648645B4EA61A30C57', N'0000', CAST(18.00 AS Decimal(18, 2)), CAST(18.18 AS Decimal(18, 2)), CAST(75.00 AS Decimal(18, 2)), CAST(20.70 AS Decimal(18, 2)), N'00/00', N'', CAST(10 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.50 AS Decimal(18, 2)), CAST(12.50 AS Decimal(18, 2)), CAST(12.17 AS Decimal(18, 2)), CAST(13.86 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', N' ', 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (6, N'', N'14', N'3', N'4', N'QQQ', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(1000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(21.43 AS Decimal(18, 2)), CAST(27.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (7, N'', N'14', N'4', N'2', N'110', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(100 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (8, N'', N'16', N'3', N'4', N'QQQ', CAST(100.00 AS Decimal(18, 2)), CAST(76.01 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(100 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (9, N'', N'20', N'9', N'4', N'OOO', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(6777 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(21.43 AS Decimal(18, 2)), CAST(27.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (10, N'', N'19', N'8', N'2', N'230', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(10000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (11, N'', N'18', N'4', N'2', N'110', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(10000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (12, N'', N'18', N'3', N'4', N'QQQ', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(1000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(21.43 AS Decimal(18, 2)), CAST(27.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (13, N'', N'18', N'5', N'3', N'WER', CAST(100.00 AS Decimal(18, 2)), CAST(98.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(4000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(2.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(12.50 AS Decimal(18, 2)), CAST(14.29 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 3, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (14, N'', N'18', N'6', N'5', N'ERER', CAST(120.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(122.00 AS Decimal(18, 2)), CAST(134.40 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(6000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(10.72 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 4, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (15, N'', N'18', N'7', N'6', N'ERTR', CAST(124.00 AS Decimal(18, 2)), CAST(124.00 AS Decimal(18, 2)), CAST(233.00 AS Decimal(18, 2)), CAST(138.88 AS Decimal(18, 2)), N'10/23', N'20231001', CAST(7000 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(10.72 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 5, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (16, N'', N'21', N'8', N'2', N'230', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(100 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (17, N'', N'21', N'3', N'4', N'QQQ', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(1200 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(21.43 AS Decimal(18, 2)), CAST(27.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (18, N'', N'21', N'8', N'2', N'230', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(100 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (19, N'', N'21', N'3', N'4', N'QQQ', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(1200 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(21.43 AS Decimal(18, 2)), CAST(27.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (20, N'', N'21', N'8', N'2', N'230', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(100 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (21, N'', N'21', N'3', N'4', N'QQQ', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(1200 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(21.43 AS Decimal(18, 2)), CAST(27.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 2, NULL)
INSERT [dbo].[changeddetailpurchase] ([DetailPurchaseID], [ChangedMasterID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (22, N'', N'16', N'4', N'2', N'110', CAST(11.00 AS Decimal(18, 2)), CAST(8.47 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(122 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(23.76 AS Decimal(18, 2)), CAST(31.17 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 2, NULL)
SET IDENTITY_INSERT [dbo].[changeddetailpurchase] OFF
GO
SET IDENTITY_INSERT [dbo].[changedvoucherpurchase] ON 

INSERT [dbo].[changedvoucherpurchase] ([ChangedID], [purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (1, 18, N'2223', N'PCR', 39, N'6/8/2022 12:00:00 AM', N'6734764', N'2', CAST(2376060.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2376060.00 AS Decimal(18, 2)), CAST(2198000.00 AS Decimal(18, 2)), CAST(32100.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'32324', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), N'', CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, N'20220611', N'18:53:28', N'1', NULL)
INSERT [dbo].[changedvoucherpurchase] ([ChangedID], [purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (2, 21, N'2223', N'PCR', 42, N'6/11/2022 12:00:00 AM', N'32347', N'2', CAST(106579.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(106579.00 AS Decimal(18, 2)), CAST(121100.00 AS Decimal(18, 2)), CAST(14521.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), N'', CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, N'20220611', N'18:57:00', N'1', NULL)
INSERT [dbo].[changedvoucherpurchase] ([ChangedID], [purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (3, 21, N'2223', N'PCR', 42, N'6/11/2022 12:00:00 AM', N'32347', N'2', CAST(106579.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(106579.00 AS Decimal(18, 2)), CAST(121100.00 AS Decimal(18, 2)), CAST(14521.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), N'', CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, N'20220611', N'19:10:48', N'1', NULL)
INSERT [dbo].[changedvoucherpurchase] ([ChangedID], [purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (4, 21, N'2223', N'PCR', 42, N'6/11/2022 12:00:00 AM', N'32347', N'2', CAST(106579.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(106579.00 AS Decimal(18, 2)), CAST(121100.00 AS Decimal(18, 2)), CAST(14521.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Numeric(10, 0)), N'', CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, N'20220611', N'19:12:01', N'1', NULL)
SET IDENTITY_INSERT [dbo].[changedvoucherpurchase] OFF
GO
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (1, 1, N'dddffd', CAST(100.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (2, 1, N'gghhg', CAST(100.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (3, 1, N'medicin', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (4, 1, N'gfhg', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (5, 1, N'medicin2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (6, 1, N'medicin', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (7, 2, N'kkkkk', CAST(300.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (8, 2, N'jkjhkhjk', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (9, 3, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (10, 3, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (11, 4, N'h2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (12, 4, N'h', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (13, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (14, 1, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (15, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (16, 1, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (17, 1, N'm1', CAST(300.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (18, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (19, 1, N'gfhgf', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (20, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (21, 1, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (22, 1, N'ty1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (23, 1, N'ty', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (24, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (25, 1, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (26, 3, N'm1', CAST(100.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (27, 3, N'm2', CAST(400.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (28, 2, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (29, 2, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (30, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (31, 1, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (32, 1, N'm2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (33, 1, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (34, 2, N'm3', CAST(300.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (35, 2, N'm1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (36, 3, N'n2', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (37, 3, N'n1', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (38, 1, N'm3', CAST(500.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (39, 1, N'm1', CAST(300.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (40, 1, N'm2', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (41, 1, N'm1', CAST(800.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (42, 1, N'm2', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (43, 1, N'm1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (44, 1, N'm1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (45, 1, N'm1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (46, 1, N'm1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (47, 1, N'm1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (48, 1, N'm2', CAST(300.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (49, 1, N'm1', CAST(400.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (50, 1, N'k1', CAST(400.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (51, 1, N'k2', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (52, 1, N'k1', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (53, 1, N'fdfsd', CAST(45.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (54, 1, N'fdfsd', CAST(45.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (55, 1, N'fdfsd', CAST(45.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (56, 1, N'fdfsd', CAST(45.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (57, 1, N'fdfsd', CAST(45.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (58, 1, N'fdfsd', CAST(45.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (59, 3, N'we1', CAST(400.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (60, 1, N'meedicicin2', CAST(100.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (61, 1, N'medicin', CAST(200.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (62, 2, N'uu', CAST(800.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (63, 2, N'uop', CAST(700.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[deleteddetailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (64, 2, N'uu', CAST(800.00 AS Decimal(18, 2)), 2)
GO
INSERT [dbo].[deleteddetailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (N'90A8BD0FD98E4DD28267F3F5FEEDB6D9', N'15', N'4', N'2', N'110', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(111 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.88 AS Decimal(18, 2)), CAST(13.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[deleteddetailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [SpecialDiscountPercent], [AmountSpecialDiscount], [PurchaseVATPercent], [ProductVATPercent], [Margin], [MarginAfterDiscount], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCreditNote], [AmountCashDiscount], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber], [scancode]) VALUES (N'95DFD1D85819468F9A905306A15F557C', N'17', N'4', N'2', N'110', CAST(11.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(12.32 AS Decimal(18, 2)), N'09/23', N'20230901', CAST(10 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(0 AS Numeric(10, 0)), CAST(11.00 AS Decimal(18, 2)), CAST(1.21 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(20.54 AS Decimal(18, 2)), CAST(25.84 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 1, NULL)
GO
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (15, N'2223', N'PCR', 36, CAST(N'2022-05-28' AS Date), N'76648', 3, CAST(1087.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1087.00 AS Decimal(18, 2)), CAST(1221.00 AS Decimal(18, 2)), CAST(134.31 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.31 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (17, N'2223', N'PCR', 38, CAST(N'2022-06-05' AS Date), N'8991', 2, CAST(97.90 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(97.90 AS Decimal(18, 2)), CAST(110.00 AS Decimal(18, 2)), CAST(12.10 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (20, N'2223', N'PCR', 41, CAST(N'2022-06-09' AS Date), N'SQWEEW', 2, CAST(752495.04 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(752495.04 AS Decimal(18, 2)), CAST(855108.00 AS Decimal(18, 2)), CAST(102612.96 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (19, N'2223', N'PCR', 40, CAST(N'2022-06-09' AS Date), N'8787', 2, CAST(97900.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(97900.00 AS Decimal(18, 2)), CAST(110000.00 AS Decimal(18, 2)), CAST(12100.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (18, N'2223', N'PCR', 39, CAST(N'2022-06-08' AS Date), N'6734764', 2, CAST(2376060.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2376060.00 AS Decimal(18, 2)), CAST(2198000.00 AS Decimal(18, 2)), CAST(32100.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'32324', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (14, N'2223', N'PCR', 35, CAST(N'2022-05-27' AS Date), N'12324', 2, CAST(88979.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(88979.00 AS Decimal(18, 2)), CAST(101100.00 AS Decimal(18, 2)), CAST(12121.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (16, N'2223', N'PCR', 37, CAST(N'2022-06-05' AS Date), N'139', 2, CAST(8795.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(8795.00 AS Decimal(18, 2)), CAST(11342.00 AS Decimal(18, 2)), CAST(1347.62 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(1199.33 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'132', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(-0.05 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
INSERT [dbo].[deletedvoucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SpecialDiscountPercentage], [AmountAddOnFreight], [AmountCreditNote], [AmountDebitNote], [StatementNumber], [OctroiPercentage], [AmountOctroi], [DueDate], [Narration], [AmountPurchaseZeroVAT], [AmountPurchase5PercentVAT], [AmountVAT5Percent], [AmountPurchase12point5PercentVAT], [AmountVAT12point5Percent], [AmountPurchaseOPercentVAT], [AmountVATOPercent], [NumberofChallans], [EntryDate], [RoundUpAmount], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID]) VALUES (25, N'2223', N'PCR', 46, CAST(N'2022-06-11' AS Date), N'E34', 2, CAST(643278.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(643278.30 AS Decimal(18, 2)), CAST(600380.00 AS Decimal(18, 2)), CAST(6406.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'1900-01-01' AS Date), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(N'1900-01-01' AS Date), CAST(N'00:00:00' AS Time), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[detailcashbankreceipt] ON 

INSERT [dbo].[detailcashbankreceipt] ([DetailCashBankReceiptID], [MasterID], [MasterSaleID], [BillSeries], [BillType], [BillNumber], [BillDate], [BillSubType], [BillAmount], [BalanceAmount], [ClearAmount], [DiscountAmount], [FromDate], [ToDate], [SerialNumber]) VALUES (4, 1, 2, N'2223', N'SCR', N'2', NULL, N'T', CAST(242.00 AS Decimal(18, 2)), CAST(242.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 1)
INSERT [dbo].[detailcashbankreceipt] ([DetailCashBankReceiptID], [MasterID], [MasterSaleID], [BillSeries], [BillType], [BillNumber], [BillDate], [BillSubType], [BillAmount], [BalanceAmount], [ClearAmount], [DiscountAmount], [FromDate], [ToDate], [SerialNumber]) VALUES (5, 2, 2, N'2223', N'SCR', N'2', NULL, N'T', CAST(242.00 AS Decimal(18, 2)), CAST(237.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 1)
INSERT [dbo].[detailcashbankreceipt] ([DetailCashBankReceiptID], [MasterID], [MasterSaleID], [BillSeries], [BillType], [BillNumber], [BillDate], [BillSubType], [BillAmount], [BalanceAmount], [ClearAmount], [DiscountAmount], [FromDate], [ToDate], [SerialNumber]) VALUES (8, 3, 3, N'2223', N'SCR', N'4', NULL, N'T', CAST(350.00 AS Decimal(18, 2)), CAST(350.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 1)
INSERT [dbo].[detailcashbankreceipt] ([DetailCashBankReceiptID], [MasterID], [MasterSaleID], [BillSeries], [BillType], [BillNumber], [BillDate], [BillSubType], [BillAmount], [BalanceAmount], [ClearAmount], [DiscountAmount], [FromDate], [ToDate], [SerialNumber]) VALUES (9, 4, 3, N'2223', N'SCR', N'4', NULL, N'T', CAST(350.00 AS Decimal(18, 2)), CAST(340.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[detailcashbankreceipt] OFF
GO
INSERT [dbo].[detailcollectionnote] ([ID], [SaleId], [MasterId], [SaleVoucherNumber]) VALUES (1, 77, 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[detailcreditdebitnoteamount] ON 

INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (3, 1, N'tt', CAST(78.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (4, 1, N'uyuy', CAST(66.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (6, 0, N'2', CAST(120.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (7, 0, N'3', CAST(123.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (8, 0, N'4', CAST(55.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (9, 0, N'88', CAST(667.00 AS Decimal(18, 2)), 4)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (10, 0, N'2', CAST(100.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (13, 31, N'TEST1', CAST(100.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (14, 31, N'2', CAST(100.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[detailcreditdebitnoteamount] ([DetailCreditDebitNoteAmountID], [ID], [Particulars], [Amount], [SerialNumber]) VALUES (15, 43, N'DFDFDF', CAST(100.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[detailcreditdebitnoteamount] OFF
GO
SET IDENTITY_INSERT [dbo].[detailcreditdebitnotestock] ON 

INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2755, 27, N'2223', N'DNS', 6, CAST(N'2022-06-09' AS Date), 4, 3, N'QQQ', 10, 1, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), NULL, CAST(1.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'E', N'N', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(990.00 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2756, 27, N'2223', N'DNS', 6, CAST(N'2022-06-09' AS Date), 2, 4, N'110', 5, 1, CAST(11.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), NULL, CAST(1.00 AS Decimal(18, 2)), CAST(5.55 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'E', N'N', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(549.45 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2760, 32, N'2223', N'DNS', 9, CAST(N'2022-06-13' AS Date), 2, 8, N'230', 100, 0, CAST(11.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'E', N'N', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11100.00 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2761, 32, N'2223', N'DNS', 9, CAST(N'2022-06-13' AS Date), 4, 3, N'QQQ', 100, 0, CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(112.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'E', N'N', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2765, 0, NULL, NULL, NULL, NULL, 2, 4, N'110', 100, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(19.58 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(959.42 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.71 AS Decimal(18, 2)), CAST(479.71 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2766, 0, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2769, 0, NULL, NULL, NULL, NULL, 2, 4, N'110', 100, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(969.21 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(484.60 AS Decimal(18, 2)), CAST(484.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2770, 0, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2772, 0, NULL, NULL, NULL, NULL, 2, 4, N'110', 11, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(1.08 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(106.61 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2773, 0, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2775, 0, NULL, NULL, NULL, NULL, 2, 4, N'110', 11, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(1.08 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(106.61 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2776, 0, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2778, 0, NULL, NULL, NULL, NULL, 2, 4, N'110', 11, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(1.08 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(106.61 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2779, 0, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2780, 0, NULL, NULL, NULL, NULL, 2, 4, N'110', 11, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(1.08 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(106.61 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2784, 38, NULL, NULL, NULL, NULL, 2, 4, N'110', 11, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(1.08 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(106.61 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(53.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2785, 38, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 2, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2786, 38, NULL, NULL, NULL, NULL, 3, 5, N'WER', 11, 0, CAST(98.00 AS Decimal(18, 2)), CAST(98.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(10.78 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(5.00 AS Decimal(18, 2)), NULL, CAST(1067.22 AS Decimal(18, 2)), 3, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(533.61 AS Decimal(18, 2)), CAST(533.61 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(26.68 AS Decimal(18, 2)), CAST(26.68 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2789, 39, NULL, NULL, NULL, NULL, 2, 8, N'230', 100, 0, CAST(9.79 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.79 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(969.21 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(484.60 AS Decimal(18, 2)), CAST(484.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2791, 40, NULL, NULL, NULL, NULL, 4, 3, N'QQQ', 11, 0, CAST(88.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(9.68 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'S', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(958.32 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(479.16 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2793, 41, N'2223', N'DNS', 10, CAST(N'2022-06-15' AS Date), 2, 4, N'110', 11, 1, CAST(11.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(11.11 AS Decimal(18, 2)), NULL, CAST(1.00 AS Decimal(18, 2)), CAST(12.21 AS Decimal(18, 2)), N'09/23', CAST(N'2023-09-01' AS Date), N'E', N'N', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1208.79 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailcreditdebitnotestock] ([DetailCreditDebitNoteStockID], [MasterID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [ProductID], [StockID], [BatchNumber], [Quantity], [SchemeQuantity], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [ReturnRate], [DiscountPercent], [DiscountAmount], [Expiry], [ExpiryDate], [ReasonCode], [AddVatInTradeRate], [VATPer], [VatAmount], [Amount], [SerialNumber], [ReplacementQuantity], [LessPer], [BreakageExpiryNo], [TransferBrekageExpiry], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI]) VALUES (2795, 42, N'2223', N'DNS', 1, CAST(N'2022-06-20' AS Date), 3, 16, N'P-234', 2, 0, CAST(78.00 AS Decimal(18, 2)), CAST(78.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(81.90 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'09/25', CAST(N'2025-09-01' AS Date), N'B', N'Y', CAST(5.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailcreditdebitnotestock] OFF
GO
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (1, 1, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(200 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(246.40 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (2, 2, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(100 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(123.20 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (3, 3, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(100 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(123.20 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (4, 4, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(120 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(147.84 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (5, 5, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(250 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(308.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (6, 5, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(150 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(184.80 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (7, 6, 6, 8, N'xr', CAST(15.40 AS Decimal(18, 2)), CAST(15.40 AS Decimal(18, 2)), CAST(22.00 AS Decimal(18, 2)), CAST(17.60 AS Decimal(18, 2)), N'05/17', CAST(N'0001-01-01' AS Date), CAST(33 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(27.95 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (8, 7, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(200 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(246.40 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (9, 8, 25, 6, N'AKL-6209', CAST(22.40 AS Decimal(18, 2)), CAST(22.40 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), CAST(25.60 AS Decimal(18, 2)), N'05/17', CAST(N'1970-01-01' AS Date), CAST(2 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(2.46 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (10, 9, 44, 9, N'test1', CAST(140.00 AS Decimal(18, 2)), CAST(140.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), N'12/20', CAST(N'2020-12-01' AS Date), CAST(299 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(2302.30 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (11, 10, 25, 1, N'ewdr', CAST(86.10 AS Decimal(18, 2)), CAST(86.10 AS Decimal(18, 2)), CAST(123.00 AS Decimal(18, 2)), CAST(98.40 AS Decimal(18, 2)), N'12/12', CAST(N'2016-12-11' AS Date), CAST(1 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(4.74 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (12, 11, 44, 12, N'test3', CAST(140.00 AS Decimal(18, 2)), CAST(140.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), N'12/20', CAST(N'2020-01-12' AS Date), CAST(200 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(1540.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (13, 12, 44, 10, N'test2', CAST(140.00 AS Decimal(18, 2)), CAST(140.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), N'12/20', CAST(N'2020-01-12' AS Date), CAST(250 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(1925.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (14, 13, 44, 9, N'test1', CAST(140.00 AS Decimal(18, 2)), CAST(140.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), N'12/20', CAST(N'2020-01-12' AS Date), CAST(10 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(77.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (15, 14, 44, 10, N'test2', CAST(140.00 AS Decimal(18, 2)), CAST(140.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), N'12/20', CAST(N'2020-01-12' AS Date), CAST(15 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(115.50 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (16, 15, 42, 13, N'asdweq', CAST(154.70 AS Decimal(18, 2)), CAST(154.70 AS Decimal(18, 2)), CAST(221.00 AS Decimal(18, 2)), CAST(176.80 AS Decimal(18, 2)), N'22/12', CAST(N'0001-01-01' AS Date), CAST(22 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(187.19 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (17, 16, 15, 14, N'www', CAST(23.10 AS Decimal(18, 2)), CAST(23.10 AS Decimal(18, 2)), CAST(33.00 AS Decimal(18, 2)), CAST(26.40 AS Decimal(18, 2)), N'22/12', CAST(N'0001-01-01' AS Date), CAST(22 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(12.50 AS Decimal(18, 2)), NULL, CAST(63.53 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (18, 17, 45, 15, N'dfdf', CAST(238.00 AS Decimal(18, 2)), CAST(238.00 AS Decimal(18, 2)), CAST(340.00 AS Decimal(18, 2)), CAST(272.00 AS Decimal(18, 2)), N'09/20', CAST(N'2020-09-01' AS Date), CAST(1 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(13.09 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (19, 18, 53, 22, N'sss', CAST(70.00 AS Decimal(18, 2)), CAST(70.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), N'10/21', CAST(N'2020-09-01' AS Date), CAST(12 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(46.20 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (20, 19, 59, 44, N'aaa', CAST(155.40 AS Decimal(18, 2)), CAST(155.40 AS Decimal(18, 2)), CAST(222.00 AS Decimal(18, 2)), CAST(177.60 AS Decimal(18, 2)), N'12/12', CAST(N'0001-01-01' AS Date), CAST(22 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(188.03 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[detailopstock] ([DetailOpStockID], [MasterID], [ProductID], [StockID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [CSTPercent], [AmountCST], [IfMRPInclusiveOfVAT], [IfTradeRateInclusiveOfVAT], [SerialNumber]) VALUES (21, 20, 59, 44, N'aaa', CAST(155.40 AS Decimal(18, 2)), CAST(155.40 AS Decimal(18, 2)), CAST(222.00 AS Decimal(18, 2)), CAST(177.60 AS Decimal(18, 2)), N'12/12', CAST(N'2001-01-01' AS Date), CAST(10 AS Numeric(10, 0)), NULL, NULL, NULL, CAST(5.50 AS Decimal(18, 2)), NULL, CAST(85.47 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[detailpurchase] ON 

INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1041, 0, 15, 2, N'P-123', CAST(23.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'', CAST(23000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1042, 0, 16, 3, N'P-234', CAST(78.00 AS Decimal(18, 2)), CAST(76.44 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(81.90 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 1000, 0, 0, CAST(2.00 AS Decimal(18, 2)), CAST(1.56 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(3.82 AS Decimal(18, 2)), CAST(4.10 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(6.67 AS Decimal(18, 2)), CAST(7.15 AS Decimal(18, 2)), 2, N'', CAST(0.00 AS Decimal(18, 2)), CAST(38220.00 AS Decimal(18, 2)), CAST(38220.00 AS Decimal(18, 2)), CAST(1911.00 AS Decimal(18, 2)), CAST(1911.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1043, 0, 17, 4, N'B-123', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(678.00 AS Decimal(18, 2)), CAST(105.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/24', N'20240901', 100, 0, 0, CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(16.19 AS Decimal(18, 2)), CAST(19.32 AS Decimal(18, 2)), 1, N'', CAST(8800.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1044, 0, 18, 5, N'B-456', CAST(50.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), CAST(122.00 AS Decimal(18, 2)), CAST(52.50 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(6.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(25000.00 AS Decimal(18, 2)), CAST(25000.00 AS Decimal(18, 2)), CAST(3000.00 AS Decimal(18, 2)), CAST(3000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1045, 0, 19, 6, N'B-567', CAST(124.00 AS Decimal(18, 2)), CAST(124.00 AS Decimal(18, 2)), CAST(233.00 AS Decimal(18, 2)), CAST(130.20 AS Decimal(18, 2)), NULL, NULL, NULL, N'10/23', N'20231001', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(14.88 AS Decimal(18, 2)), CAST(15.62 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 2, N'', CAST(0.00 AS Decimal(18, 2)), CAST(62000.00 AS Decimal(18, 2)), CAST(62000.00 AS Decimal(18, 2)), CAST(7440.00 AS Decimal(18, 2)), CAST(7440.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1047, 30, 21, 2, N'P-123', CAST(23.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'', CAST(23000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1048, 30, 22, 4, N'B-123', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(678.00 AS Decimal(18, 2)), CAST(105.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/24', N'20240901', 1000, 0, 0, CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(16.19 AS Decimal(18, 2)), CAST(19.32 AS Decimal(18, 2)), 2, N'', CAST(88000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1049, 30, 23, 7, N'P-654', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(105.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 3, N'', CAST(0.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), CAST(6000.00 AS Decimal(18, 2)), CAST(6000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1050, 31, 24, 8, N'DDDD', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), CAST(105.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/26', N'20260901', 100, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1051, 32, 25, 2, N'P-123', CAST(23.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'', CAST(23000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1052, 32, 26, 4, N'B-123', CAST(100.00 AS Decimal(18, 2)), CAST(88.00 AS Decimal(18, 2)), CAST(678.00 AS Decimal(18, 2)), CAST(105.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/24', N'20240901', 1000, 0, 0, CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(16.19 AS Decimal(18, 2)), CAST(19.32 AS Decimal(18, 2)), 2, N'', CAST(88000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1053, 33, 0, 2, N'DD-234', CAST(230.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), CAST(459.00 AS Decimal(18, 2)), CAST(241.50 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/24', N'20240901', 1000, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(27.60 AS Decimal(18, 2)), CAST(28.98 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(4.76 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(115000.00 AS Decimal(18, 2)), CAST(115000.00 AS Decimal(18, 2)), CAST(13800.00 AS Decimal(18, 2)), CAST(13800.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1054, 34, 28, 2, N'B123', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 25, 25, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(27.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(2875.00 AS Decimal(18, 2)), CAST(2875.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1055, 34, 29, 3, N'B-234', CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'00/00', N'', 40, 40, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(3.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'', CAST(0.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1056, 35, 32, 2, N'B123', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 25, 25, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(27.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(2875.00 AS Decimal(18, 2)), CAST(2875.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1057, 35, 33, 3, N'B-234', CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'00/00', N'', 40, 40, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(3.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'', CAST(0.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1058, 36, 34, 2, N'B123', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 25, 25, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(27.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(2875.00 AS Decimal(18, 2)), CAST(2875.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(345.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1059, 36, 35, 3, N'B-234', CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'00/00', N'', 40, 40, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(3.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'', CAST(0.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1060, 37, 36, 2, N'T-876', CAST(150.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 25, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(18.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'', CAST(0.00 AS Decimal(18, 2)), CAST(1875.00 AS Decimal(18, 2)), CAST(1875.00 AS Decimal(18, 2)), CAST(225.00 AS Decimal(18, 2)), CAST(225.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
INSERT [dbo].[detailpurchase] ([DetailPurchaseID], [PurchaseID], [StockID], [ProductID], [BatchNumber], [TradeRate], [PurchaseRate], [MRP], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [ReplacementQuantity], [ItemDiscountPercent], [AmountItemDiscount], [SchemeDiscountPercent], [AmountSchemeDiscount], [PurchaseVATPercent], [ProductVATPercent], [AmountPurchaseVAT], [AmountProdVAT], [AmountCashDiscount], [TODAmount], [TODPercent], [SurchargePercent], [SurchargeAmount], [AddPercent], [Margin], [MarginAfterDiscount], [SerialNumber], [scancode], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTC], [GSTS], [PriceToRetailer], [ProfitPercent], [GSTiAmount], [GSTI], [AmountCreditNote]) VALUES (1061, 37, 37, 3, N'T-654', CAST(30.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), CAST(35.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'00/00', N'', 40, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(3.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'', CAST(0.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(72.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailpurchase] OFF
GO
SET IDENTITY_INSERT [dbo].[detailpurchaseordercnf] ON 

INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (9, 1, 0, 2, 8, NULL, NULL, CAST(56 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:00:15', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (10, 1, 0, 2, 8, NULL, NULL, CAST(56 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:00:37', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (11, 1, 0, 2, 9, NULL, NULL, CAST(57 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:08:09', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (12, 1, 0, 2, 9, NULL, NULL, CAST(57 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:08:10', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (13, 1, 0, 2, 10, NULL, NULL, CAST(58 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:48:41', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (14, 1, 0, 2, 10, NULL, NULL, CAST(58 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:48:43', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (15, 1, 0, 2, 11, NULL, NULL, CAST(59 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:53:06', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (16, 1, 0, 2, 11, NULL, NULL, CAST(59 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:53:07', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (17, 1, 0, 2, 12, NULL, NULL, CAST(60 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:58:06', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (18, 1, 0, 2, 12, NULL, NULL, CAST(60 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'19:58:07', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (19, 1, 0, 2, 13, NULL, NULL, CAST(61 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'20:08:47', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (20, 1, 0, 2, 13, NULL, NULL, CAST(61 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'20:08:48', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (21, 1, 0, 2, 14, NULL, NULL, CAST(62 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'20:19:05', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (22, 1, 0, 2, 14, NULL, NULL, CAST(62 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'20:19:06', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (23, 1, 0, 2, 15, NULL, NULL, CAST(63 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'20:28:02', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (24, 1, 0, 2, 15, NULL, NULL, CAST(63 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220708', NULL, N'Y', N'T', N'20220708', N'20:28:04', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (25, 1, 0, 2, 16, NULL, NULL, CAST(64 AS Numeric(10, 0)), N'20220709', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220709', NULL, N'Y', N'T', N'20220709', N'10:07:10', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseordercnf] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedSchemeQuantity], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (26, 1, 0, 2, 16, NULL, NULL, CAST(64 AS Numeric(10, 0)), N'20220709', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), NULL, 1, NULL, N'20220709', NULL, N'Y', N'T', N'20220709', N'10:07:10', N'1', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailpurchaseordercnf] OFF
GO
SET IDENTITY_INSERT [dbo].[detailpurchaseorderfromCNF] ON 

INSERT [dbo].[detailpurchaseorderfromCNF] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedScheme], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1, 1, 2, 0, NULL, NULL, NULL, CAST(63 AS Numeric(10, 0)), N'20220708', CAST(0 AS Numeric(10, 0)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 0, 0, NULL, NULL, NULL, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseorderfromCNF] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [CNFReceivedQuantity], [CNFReceivedScheme], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, 1, 2, 0, NULL, NULL, NULL, CAST(63 AS Numeric(10, 0)), N'20220708', CAST(0 AS Numeric(10, 0)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 0, 0, NULL, NULL, NULL, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailpurchaseorderfromCNF] OFF
GO
SET IDENTITY_INSERT [dbo].[detailpurchaseorderfromstockist] ON 

INSERT [dbo].[detailpurchaseorderfromstockist] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (20, 1, 2, 3, NULL, CAST(54 AS Numeric(10, 0)), N'20220709', CAST(64 AS Numeric(10, 0)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 40, -73, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseorderfromstockist] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (21, 1, 2, 3, NULL, CAST(54 AS Numeric(10, 0)), N'20220709', CAST(64 AS Numeric(10, 0)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 25, 975, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailpurchaseorderfromstockist] OFF
GO
SET IDENTITY_INSERT [dbo].[detailpurchaseorderstockist] ON 

INSERT [dbo].[detailpurchaseorderstockist] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [IfUploaded], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (98, 1, 2, 3, 37, CAST(54 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 40, -73, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), 19, NULL, NULL, N'20220708', NULL, N'Y', N'T', NULL, N'20220708', N'18:21:12', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseorderstockist] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [IfUploaded], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (99, 1, 2, 3, 37, CAST(54 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 0, 25, 975, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), 19, NULL, NULL, N'20220708', NULL, N'Y', N'T', NULL, N'20220708', N'18:21:13', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseorderstockist] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [IfUploaded], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (100, 1, 2, 6, 38, CAST(55 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 0, 40, -73, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, CAST(0.00 AS Decimal(18, 2)), 20, NULL, NULL, N'20220708', NULL, N'Y', N'T', NULL, N'20220708', N'18:33:13', N'1', NULL, NULL, NULL)
INSERT [dbo].[detailpurchaseorderstockist] ([DSLID], [EcoMartID], [CNFID], [StockistID], [MasterID], [StockistOrderNumber], [StockistOrderDate], [CNFOrderNumber], [CNFOrderDate], [EcoMartOrderNumber], [EcoMartOrderDate], [EcoMartBillNumber], [EcoMartBillDate], [CNFBillNumber], [CNFBillDate], [StockistOrderQuantity], [StockistSchemeQuantity], [StockistSaleQuantity], [StockistClosingStock], [StockistReceivedQuantity], [StockistReceivedSchemeQuantity], [CNFOrderQuantity], [CNFSchemeQuantity], [CNFSaleQuantity], [CNFClosingStock], [EcoMartOrderQuantity], [EcoMartSchemeQuantity], [ProductId], [PurchaseRate], [StockistAccountID], [CNFAccountID], [EcoMartAccountID], [ShortListDate], [ShortListTime], [IfSave], [IfDailyShortList], [IfUploaded], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (101, 1, 2, 6, 38, CAST(55 AS Numeric(10, 0)), N'20220708', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 31, 0, 31, 969, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, CAST(230.00 AS Decimal(18, 2)), 20, NULL, NULL, N'20220708', NULL, N'Y', N'T', NULL, N'20220708', N'18:33:13', N'1', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailpurchaseorderstockist] OFF
GO
SET IDENTITY_INSERT [dbo].[detailsale] ON 

INSERT [dbo].[detailsale] ([DetailSaleID], [MasterSaleID], [ProductID], [StockID], [BatchNumber], [PurchaseRate], [MRP], [SaleRate], [TradeRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [SchemeDiscountAmount], [Amount], [OctroiPer], [OctroiAmount], [CSTPer], [CSTAmount], [VATPer], [VATAmount], [InwardNumber], [OperatorID], [IPDOPDCode], [IndentNumber], [PMTDiscount], [PMTAmount], [ItemDiscountPer], [ItemDiscountAmount], [CashDiscountAmount], [IfProductDiscount], [SerialNumber], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [AccountID], [VoucherDate], [SalAmntOn12P5Vat], [SalAmntOnZeroVat], [SalAmntOn18Vat], [VatAmntFor12P5vat], [VatAmntFor18Pvat], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI], [ActualBatchNumber], [ActualMRP], [ActualSaleRate], [PONumber], [POId], [EcoMartID], [CNFID], [StockistID]) VALUES (1061, 25, 2, 15, N'P-123', CAST(0.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 25, 0, CAST(0.00 AS Decimal(18, 2)), CAST(603.75 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(12.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.0000 AS Decimal(8, 4)), CAST(0.0000 AS Decimal(8, 4)), 7, N'20220708', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(301.88 AS Decimal(18, 2)), CAST(301.88 AS Decimal(18, 2)), NULL, CAST(36.22 AS Decimal(18, 2)), CAST(36.22 AS Decimal(18, 2)), NULL, N'P-123', CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailsale] ([DetailSaleID], [MasterSaleID], [ProductID], [StockID], [BatchNumber], [PurchaseRate], [MRP], [SaleRate], [TradeRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [SchemeDiscountAmount], [Amount], [OctroiPer], [OctroiAmount], [CSTPer], [CSTAmount], [VATPer], [VATAmount], [InwardNumber], [OperatorID], [IPDOPDCode], [IndentNumber], [PMTDiscount], [PMTAmount], [ItemDiscountPer], [ItemDiscountAmount], [CashDiscountAmount], [IfProductDiscount], [SerialNumber], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [AccountID], [VoucherDate], [SalAmntOn12P5Vat], [SalAmntOnZeroVat], [SalAmntOn18Vat], [VatAmntFor12P5vat], [VatAmntFor18Pvat], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI], [ActualBatchNumber], [ActualMRP], [ActualSaleRate], [PONumber], [POId], [EcoMartID], [CNFID], [StockistID]) VALUES (1062, 25, 3, 16, N'P-234', CAST(0.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(81.90 AS Decimal(18, 2)), CAST(78.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/25', N'20250901', 40, 0, CAST(0.00 AS Decimal(18, 2)), CAST(3276.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(12.00 AS Decimal(18, 2)), CAST(393.12 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', 2, CAST(0.00 AS Decimal(18, 2)), CAST(0.0000 AS Decimal(8, 4)), CAST(0.0000 AS Decimal(8, 4)), 7, N'20220708', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(1638.00 AS Decimal(18, 2)), CAST(1638.00 AS Decimal(18, 2)), NULL, CAST(196.56 AS Decimal(18, 2)), CAST(196.56 AS Decimal(18, 2)), NULL, N'P-234', CAST(120.00 AS Decimal(18, 2)), CAST(81.90 AS Decimal(18, 2)), 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailsale] ([DetailSaleID], [MasterSaleID], [ProductID], [StockID], [BatchNumber], [PurchaseRate], [MRP], [SaleRate], [TradeRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [SchemeDiscountAmount], [Amount], [OctroiPer], [OctroiAmount], [CSTPer], [CSTAmount], [VATPer], [VATAmount], [InwardNumber], [OperatorID], [IPDOPDCode], [IndentNumber], [PMTDiscount], [PMTAmount], [ItemDiscountPer], [ItemDiscountAmount], [CashDiscountAmount], [IfProductDiscount], [SerialNumber], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [AccountID], [VoucherDate], [SalAmntOn12P5Vat], [SalAmntOnZeroVat], [SalAmntOn18Vat], [VatAmntFor12P5vat], [VatAmntFor18Pvat], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI], [ActualBatchNumber], [ActualMRP], [ActualSaleRate], [PONumber], [POId], [EcoMartID], [CNFID], [StockistID]) VALUES (1063, 26, 2, 15, N'P-123', CAST(0.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 6, 0, CAST(0.00 AS Decimal(18, 2)), CAST(144.90 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(12.00 AS Decimal(18, 2)), CAST(17.39 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.0000 AS Decimal(8, 4)), CAST(0.0000 AS Decimal(8, 4)), 7, N'20220708', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), NULL, CAST(8.70 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), NULL, N'P-123', CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[detailsale] ([DetailSaleID], [MasterSaleID], [ProductID], [StockID], [BatchNumber], [PurchaseRate], [MRP], [SaleRate], [TradeRate], [EcoMartRate], [CNFRate], [StockistRate], [Expiry], [ExpiryDate], [Quantity], [SchemeQuantity], [SchemeDiscountAmount], [Amount], [OctroiPer], [OctroiAmount], [CSTPer], [CSTAmount], [VATPer], [VATAmount], [InwardNumber], [OperatorID], [IPDOPDCode], [IndentNumber], [PMTDiscount], [PMTAmount], [ItemDiscountPer], [ItemDiscountAmount], [CashDiscountAmount], [IfProductDiscount], [SerialNumber], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [AccountID], [VoucherDate], [SalAmntOn12P5Vat], [SalAmntOnZeroVat], [SalAmntOn18Vat], [VatAmntFor12P5vat], [VatAmntFor18Pvat], [GSTAmountZero], [GSTSAmount], [GSTCAmount], [GSTIAmount], [GSTS], [GSTC], [GSTI], [ActualBatchNumber], [ActualMRP], [ActualSaleRate], [PONumber], [POId], [EcoMartID], [CNFID], [StockistID]) VALUES (1064, 27, 2, 15, N'P-123', CAST(0.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'09/23', N'20230901', 6, 0, CAST(0.00 AS Decimal(18, 2)), CAST(144.90 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(12.00 AS Decimal(18, 2)), CAST(17.39 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(6, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.0000 AS Decimal(8, 4)), CAST(0.0000 AS Decimal(8, 4)), 7, N'20220708', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), NULL, CAST(8.70 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), NULL, N'P-123', CAST(111.00 AS Decimal(18, 2)), CAST(24.15 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detailsale] OFF
GO
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (2, 21, 6)
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (3, 1, 4)
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (4, 23, 6)
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (5, 25, 6)
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (8, 26, 6)
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (9, 26, 7)
INSERT [dbo].[detailuserrole] ([ID], [UserId], [RoleId]) VALUES (10, 27, 6)
GO
SET IDENTITY_INSERT [dbo].[linkdruggrouping] ON 

INSERT [dbo].[linkdruggrouping] ([LinkDrugGroupingID], [GenericCategoryID], [ProductID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (1, 715, 1, N'20210908', N'22:04:29', N'1', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[linkdruggrouping] OFF
GO
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'375DFBCE6AC7407D997595FD9775F21A', N'2', N'', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'AE619BEE41884DE7BAD7452ACAA90171', N'2', N'', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'14BD6F86F31546A39E4DBFC5B63D7F1A', N'', N'1001', N'20210908', N'22:26:29', N'1', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'8E0659E10D77423CBC24E89D4D23D494', N'', N'1001', N'20210908', N'22:26:29', N'1', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'942B960B147D4B56B73FE0B64FDB32B2', N'', N'1001', N'20210908', N'22:26:29', N'1', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'73A329F72EBA4E2194B29C0DF33C4A83', N'2', N'', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'CA591CC9F3444B2C80C79EF5A46DCAB9', N'2', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'6637DC586DD84CA89A81E8CEF8BD2B82', N'0', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'7668FDF734E1448996C47EA6256885D8', N'0', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'50BA455D9E3B4036A4D4F4E8CCC40738', N'0', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'108F6FC415D74F889D336DE3E4C4A994', N'2', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'F5C37DB898E24F90B102A4188E7B7183', N'0', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'C5074BD523F34C02B8B457A739719682', N'0', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'6D700FF0285A4AAB8C26E51672089E42', N'0', N'1006', N'', N'', N'', N'', N'', N'', NULL)
INSERT [dbo].[linkpartycompany] ([LinkPartyCompanyID], [AccountID], [CompID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [SerialNumber]) VALUES (N'A9B365A1C7294574AF38F788781FD176', N'', N'1001', N'20210908', N'22:26:29', N'1', N'', N'', N'', NULL)
GO
INSERT [dbo].[logindetails] ([Email], [Password], [RememberMe], [RoleId], [UserId]) VALUES (N'naga@gmail.com', N'naga123', CAST(1 AS Numeric(3, 0)), 1, 1)
INSERT [dbo].[logindetails] ([Email], [Password], [RememberMe], [RoleId], [UserId]) VALUES (N'scorg@gmail.com', N'scorg123', CAST(1 AS Numeric(3, 0)), 2, 2)
GO
SET IDENTITY_INSERT [dbo].[masteraccount] ON 

INSERT [dbo].[masteraccount] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccAddress1], [AccAddress2], [AccTelephone], [MobileNumberForSMS], [AccContactPerson], [Pharmscist], [AccDiscountOffered], [AccTransactionType], [AccIFOctroi], [AccOctroiPer], [AccBankId], [AccBranchID], [AccGroupID], [AccDoctorID], [AccAreaID], [AccSalesmanID], [AccDelivaryBoyID], [AccDelivarySalesmanID], [BranchCity], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccVATTIN], [AccDLN], [AccPAN], [AccLBT], [AccScheduleXLICNumber], [AccEmailID], [AccIfOMS], [AccIfLocalParty], [AccIfScheduleXSale], [IPartyID], [AccountNumber], [AccountType], [IFSCCode], [AccRemark1], [AccRemark2], [AccBankAccountNumber], [IFFIX], [IFLBT], [LockDays], [LockStatements], [HoldingAmount], [HoldCreditNote], [PutInBlackList], [SetAsDefault], [Remark], [TAG], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode], [GlobalID], [MSCDACode], [AlliedCode], [ScorgCode], [PurchaseBillFormat], [CreditDays], [PTSper], [AccBankAccountType], [AccTokenNumber], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccCrVisitDays], [AccShortName], [AccHistory], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [AccStatement15Days], [AccLessPercentInDebitNote], [EcoMartID], [CNFID], [StockistID]) VALUES (7, N'D', N'DEBTOR 1', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'PUNE', N'', N'', N'', N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, 0, 0, 40, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, N'', NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-05-29', N'20:14:47', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(10 AS Numeric(18, 0)), CAST(0 AS Numeric(2, 0)), CAST(0 AS Numeric(2, 0)), CAST(0 AS Numeric(4, 0)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masteraccount] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccAddress1], [AccAddress2], [AccTelephone], [MobileNumberForSMS], [AccContactPerson], [Pharmscist], [AccDiscountOffered], [AccTransactionType], [AccIFOctroi], [AccOctroiPer], [AccBankId], [AccBranchID], [AccGroupID], [AccDoctorID], [AccAreaID], [AccSalesmanID], [AccDelivaryBoyID], [AccDelivarySalesmanID], [BranchCity], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccVATTIN], [AccDLN], [AccPAN], [AccLBT], [AccScheduleXLICNumber], [AccEmailID], [AccIfOMS], [AccIfLocalParty], [AccIfScheduleXSale], [IPartyID], [AccountNumber], [AccountType], [IFSCCode], [AccRemark1], [AccRemark2], [AccBankAccountNumber], [IFFIX], [IFLBT], [LockDays], [LockStatements], [HoldingAmount], [HoldCreditNote], [PutInBlackList], [SetAsDefault], [Remark], [TAG], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode], [GlobalID], [MSCDACode], [AlliedCode], [ScorgCode], [PurchaseBillFormat], [CreditDays], [PTSper], [AccBankAccountType], [AccTokenNumber], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccCrVisitDays], [AccShortName], [AccHistory], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [AccStatement15Days], [AccLessPercentInDebitNote], [EcoMartID], [CNFID], [StockistID]) VALUES (8, N'B', N'BANK OF BARODA', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masteraccount] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccAddress1], [AccAddress2], [AccTelephone], [MobileNumberForSMS], [AccContactPerson], [Pharmscist], [AccDiscountOffered], [AccTransactionType], [AccIFOctroi], [AccOctroiPer], [AccBankId], [AccBranchID], [AccGroupID], [AccDoctorID], [AccAreaID], [AccSalesmanID], [AccDelivaryBoyID], [AccDelivarySalesmanID], [BranchCity], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccVATTIN], [AccDLN], [AccPAN], [AccLBT], [AccScheduleXLICNumber], [AccEmailID], [AccIfOMS], [AccIfLocalParty], [AccIfScheduleXSale], [IPartyID], [AccountNumber], [AccountType], [IFSCCode], [AccRemark1], [AccRemark2], [AccBankAccountNumber], [IFFIX], [IFLBT], [LockDays], [LockStatements], [HoldingAmount], [HoldCreditNote], [PutInBlackList], [SetAsDefault], [Remark], [TAG], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode], [GlobalID], [MSCDACode], [AlliedCode], [ScorgCode], [PurchaseBillFormat], [CreditDays], [PTSper], [AccBankAccountType], [AccTokenNumber], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccCrVisitDays], [AccShortName], [AccHistory], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [AccStatement15Days], [AccLessPercentInDebitNote], [EcoMartID], [CNFID], [StockistID]) VALUES (23, N'C', N'EcoMart', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'A1', N'A2', N'T1', N'M1', N'Owner1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 31, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'GST1', N'DLN1', NULL, NULL, NULL, N'E1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[masteraccount] OFF
GO
SET IDENTITY_INSERT [dbo].[masterarea] ON 

INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1, N'KOTHRUD', N'2021-09-13', N'19:45:03', N'1', NULL, NULL, NULL)
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, N'GURUWAR PETH ', N'2021-09-15', N'14:02:05', N'1', N'2022-05-11', N'18:01:54', N'1')
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (3, N'SADASHIV PETH', N'2022-05-09', N'18:23:08', N'1', NULL, NULL, NULL)
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, N'M.G. Road', N'2022-05-10', N'22:17:43', N'1', N'2022-05-12', N'15:33:28', N'1')
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (6, N'SUBHASH NAGAR', N'2022-05-11', N'17:49:02', N'1', NULL, NULL, NULL)
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (7, N'HADAPSAR', N'2022-05-11', N'17:49:32', N'1', NULL, NULL, NULL)
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (8, N'JANGLI MAHARAJ  ROAD', N'2022-05-12', N'15:28:39', N'1', N'2022-05-12', N'15:46:29', N'1')
INSERT [dbo].[masterarea] ([AreaID], [AreaName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (9, N'Katraj', NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[masterarea] OFF
GO
SET IDENTITY_INSERT [dbo].[masterbank] ON 

INSERT [dbo].[masterbank] ([BankId], [BankName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (11, N'BANK OF BARODA', N'2022-05-12', N'15:53:19', N'1', NULL, NULL, NULL)
INSERT [dbo].[masterbank] ([BankId], [BankName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (12, N'BANK OF INDIA', N'2022-05-14', N'12:00:48', N'1', NULL, NULL, NULL)
INSERT [dbo].[masterbank] ([BankId], [BankName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (13, N'RUPEE BANK', N'2022-05-14', N'12:01:03', N'1', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[masterbank] OFF
GO
INSERT [dbo].[masterbanktype] ([ID], [AccountTypeName]) VALUES (1, N'SAVING ')
INSERT [dbo].[masterbanktype] ([ID], [AccountTypeName]) VALUES (2, N'CURRENT')
INSERT [dbo].[masterbanktype] ([ID], [AccountTypeName]) VALUES (3, N'RECURRING ')
INSERT [dbo].[masterbanktype] ([ID], [AccountTypeName]) VALUES (4, N'FIXED ')
GO
SET IDENTITY_INSERT [dbo].[masterbranch] ON 

INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1, N'Kalyaninagar', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, N'DHOLE PATIL ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (3, N'KASBAPETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, N'AGAKHAN PALACE PUNE1', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, N'NARHE,PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (6, N'YAWAT-PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (7, N'WADIA COLLEGE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (8, N'VIDYA NAGAR PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (9, N'PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (10, N'Kunals', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (11, N'CHINCHWAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (12, N'SADASHIV PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (13, N'RAMBAUGH COLONY', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (14, N'MUNDAWA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (15, N'GULTEKDI MARKET YARD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (16, N'NAVI PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (17, N'KOREGAON PARK', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (18, N'THEUR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (19, N'BALEWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (20, N'SAHAKAR NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (21, N'SANGAVI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (22, N'WARJE EXTN.COUNTER', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (23, N'DATTAWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (24, N'UNIVERSITY PUNE 411007', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (25, N'PUNE CANTONMENT', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (26, N'MANJARI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (27, N'S.S.I.PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (28, N'JEDHE NAGAR PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (29, N'NASHIK', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (30, N'KEDGAON', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (31, N'SHIVAJI NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (32, N'COMMERCIAL BRANCH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (33, N'BHUSARI COLONY', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (34, N'MODEL COLONY', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (35, N'SWARGATE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (36, N'DHYARI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (37, N'GHORPADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (38, N'SHANKARSETH ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (39, N'GIDNEY PARK', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (40, N'SHANIWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (41, N'HINGNE KHURD PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (42, N'RAVIWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (43, N'WADGAON BK.', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (44, N'NEW B', CAST(N'2016-06-17' AS Date), CAST(N'08:55:29' AS Time), N'USER007', NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (45, N'GANESH PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (46, N'KUNJIRWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (47, N'NIGDI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (48, N'NARAYAN PETH PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (49, N'KASARWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (50, N'TILAK ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (51, N'VISHRANT WADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (52, N'RAJENDRANAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (53, N'ENGINEERING COLLEGE PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (54, N'BHAWANI PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (55, N'BANER ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (56, N'DAPODI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (57, N'PANSHET', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (58, N'AKHAN PALACE PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (59, N'FERGUSSON ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (60, N'MOMINPURA,PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (61, N'GANESH NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (62, N'BHOSARI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (63, N'KOTHRUD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (64, N'BAWADHAN-PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (65, N'LOKMANYA NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (66, N'SHUKRAWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (67, N'SARAS BAUG', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (68, N'NEW D.P.ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (69, N'AMBEGAON', NULL, NULL, NULL, CAST(N'2022-05-14' AS Date), CAST(N'12:03:23' AS Time), N'1')
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (70, N'GOLIBAR MAIDAN', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (71, N'MUKUND NAGAR PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (72, N'SOLAPUR BAZAAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (73, N'AGAKHAN999', NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'12:52:49' AS Time), NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (74, N'BIBVEWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (75, N'LAW COLLEGE ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (76, N'SHIVAJI ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (77, N'SHIRUR 412210', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (78, N'SUBHASH NAGAR PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (79, N'WAGHOLI POONA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (80, N'SOMWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (81, N'Test', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (82, N'MY SALES MAN 2', CAST(N'2016-06-15' AS Date), CAST(N'07:54:50' AS Time), N'USER007', NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (83, N'kkkk', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (84, N'PUNE SATARA RD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (85, N'RAHATANI-PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (86, N'KHARADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (87, N'VIMAN NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (88, N'KARVE ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (89, N'BARAMATI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (90, N'GUJARAT COLONY', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (91, N'KATRAJ', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (92, N'WADGAON DHAIRY PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (93, N'CAMP', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (94, N'CHATURSHRUNGI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (95, N'BHANDARKAR ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (96, N'VIJAYNAGAR BRANCH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (97, N'ALANDI', NULL, NULL, NULL, CAST(N'2022-05-14' AS Date), CAST(N'12:02:50' AS Time), N'1')
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (98, N'SATARA ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (99, N'PARVATI VITHALWADI RD DHA', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (100, N'OPP.SARAS BAUG', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (101, N'PASHAN', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (102, N'J.M.ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (103, N'PUNE NAGAR ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (104, N'MAGARPATTA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (105, N'SPISER COLLEGE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (106, N'SALISBURY PARK PUNE 1', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (107, N'M.G.ROAD,PUNE CAMP', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (108, N'BUND GARDEN PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (109, N'TMV COLONY PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (110, N'INDIRA VASAHAT', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (111, N'KIRKEE PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (112, N'RAMWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (113, N'GOKHALE NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (114, N'DECCAN GYMKHANA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (115, N'PADMAVATI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (116, N'ANAND NAGAR PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (117, N'PERUGATE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (118, N'SYNAGOGUE STREE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (119, N'SASANE NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (120, N'KHUTBAV', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (121, N'NANA PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (122, N'SHANIPAR CHOWK', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (123, N'VADGAON MAWAL', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (124, N'PARVATI IND ESTATE PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (125, N'CHANDAN NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (126, N'PULGATE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (127, N'YERAWDA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (128, N'WANAWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (129, N'KHADKI BRANCH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (130, N'MARKETYARD PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (131, N'WADGAON SHERI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (132, N'PHURSUNGI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (133, N'DHANKAWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (134, N'SENAPATI BAPAT', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (135, N'URALI KANCHAN', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (136, N'WARJE MALWADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (137, N'UTTAM NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (138, N'EAST STREET PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (139, N'WARJE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (140, N'NEW SANGHVI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (141, N'PAUD ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (142, N'FURSUNGI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (143, N'ERANDAVANA PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (144, N'PHULE MARKET', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (145, N'MULSHI (POUD)', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (146, N'CHAKAN', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (147, N'SHANTINAGAR,ALANDI ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (148, N'KALYANI NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (149, N'LOHAGAON PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (150, N'MAHATMA PHULE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (151, N'DSK VISHWA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (152, N'AGRICULTURAL M/', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (153, N'HINGNE-VADGAON', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (154, N'AUNDH,PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (155, N'SEVEN LOVES CHOWK', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (156, N'VITHALWADI PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (157, N'LONI KALBHOR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (158, N'VARJE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (159, N'KHADAKWASALA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (160, N'Kunal', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (161, N'KARVENAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (162, N'AKURDI-NIGDI PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (163, N'SANGAM WADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (164, N'RASTA PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (165, N'DASHBUJA GANPATI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (166, N'N C L CAMPUS', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (167, N'TALEGAON PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (168, N'MAYUR COLONY', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (169, N'PREM NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (170, N'MOLIDINA ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (171, N'PIMPRI PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (172, N'MANIK BAUG', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (173, N'NANDED PHATA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (174, N'NETAJINAGAR (WANAWADI)', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (175, N'MUMBAI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (176, N'CHANDANNAGAR KHARADI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (177, N'SINHAGAD ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (178, N'KONDHWA', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (179, N'BUDHWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (180, N'INDUSTRIAL FINANCIAL BR.', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (181, N'MY BRANCH', CAST(N'2016-06-15' AS Date), CAST(N'07:49:03' AS Time), N'USER007', NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (182, N'SASWAD BR PUNE', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (183, N'GURUWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (184, N'LAXMI ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (185, N'PARVATI', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (186, N'BAJIRAO ROAD', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (187, N'ASHOK NAGAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (188, N'HADAPSAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (189, N'ARANYESHWAR', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (190, N'MANGALWAR PETH', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (191, N'NIBM', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (192, N'vishal', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (193, N'12345678901234567890123456789012345678901234567890', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (194, N'BHANDARKAR ROAD NASHIK', CAST(N'2017-05-30' AS Date), CAST(N'14:38:10' AS Time), NULL, CAST(N'2017-05-30' AS Date), CAST(N'14:38:28' AS Time), NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (195, N'JJJ', CAST(N'2017-05-30' AS Date), CAST(N'14:55:22' AS Time), NULL, NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (196, N'COMPANY BRANCH12', CAST(N'2017-06-02' AS Date), CAST(N'16:56:18' AS Time), NULL, CAST(N'2017-06-02' AS Date), CAST(N'16:57:00' AS Time), NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (197, N'TEST BRANCH EDIT', CAST(N'2021-09-08' AS Date), CAST(N'13:18:18' AS Time), N'1', CAST(N'2021-09-08' AS Date), CAST(N'13:18:40' AS Time), N'1')
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (198, N'LULLA NAGAR', CAST(N'2021-09-10' AS Date), CAST(N'17:57:46' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (199, N'WAKAD', CAST(N'2021-09-11' AS Date), CAST(N'11:09:42' AS Time), N'1', NULL, NULL, NULL)
GO
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (200, N'BALAJI NAGAR', CAST(N'2021-09-15' AS Date), CAST(N'14:40:28' AS Time), N'1', CAST(N'2021-09-15' AS Date), CAST(N'14:41:01' AS Time), N'1')
INSERT [dbo].[masterbranch] ([BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (201, N'JAMNAGAR', CAST(N'2021-09-15' AS Date), CAST(N'18:41:14' AS Time), N'1', CAST(N'2021-09-15' AS Date), CAST(N'18:41:34' AS Time), N'1')
SET IDENTITY_INSERT [dbo].[masterbranch] OFF
GO
INSERT [dbo].[mastercompany] ([CompID], [CompName], [CompShortName], [CompTelephone], [CompMailId], [CompContactPerson], [CompAddress], [Address2], [PartyID_1], [PartyID_2], [PartyID_3], [PartyID_4], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [Companycode], [GlobalID], [Tag], [MaincompID]) VALUES (1, N'CIPLA', N'CIP', N'', N'', N'', N'MUMBAI', NULL, 0, 0, 0, 0, CAST(N'2022-07-06' AS Date), CAST(N'09:56:32' AS Time), N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1, N'vishal nikam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, N'kunal1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (3, N'abchd', N'www', N'ttttttttt', N'78965412365', N'+919421222277', N'aaa@corgtechnologies.com', N'wwwww', NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'11:27:25' AS Time), NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, N'harshad', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, N'Nagraj1', N'kka', N'knk', N'5555555599', N'9421222233', N'y@gmial.com', N'andkandal', NULL, NULL, NULL, CAST(N'2017-03-23' AS Date), CAST(N'11:15:17' AS Time), NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (6, N'harish', N'akjdbd', N'nnnnnnnlknlk', N'6688688889', N'8686868686', N'zcc@yahoo.com', N'ad', NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'12:52:17' AS Time), NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (7, N'harii', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (8, N'hhsh', N'hhsh', N'hhsh', N'7879799932', N'2233444877', N'uuuu@gmail.com', N'jhsd', NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'11:27:41' AS Time), NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (9, N'12345678901234567890123456789012345678901234567890', N'12345678901234567890123456789012345678901234567890', N'12345678901234567890123456789012345678901234567890', N'9433333322', N'9433333322', N'a@gmail.com', N'sss', NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'11:27:46' AS Time), NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (10, N'JAY', N'HH', N'JH', NULL, N'8767656666', NULL, N'BH', CAST(N'2017-05-30' AS Date), CAST(N'14:53:27' AS Time), NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (11, N'OOOOOOO', N'OOOOOOOO', N'OOOOOOO', N'02532215513', N'5454545454', N'kjkjkjkj@gmail.com', N'14FGHJ', CAST(N'2017-06-02' AS Date), CAST(N'16:39:08' AS Time), NULL, CAST(N'2017-06-02' AS Date), CAST(N'16:40:32' AS Time), NULL)
INSERT [dbo].[masterdelivaryboy] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (12, N'SDFGH', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-06-07' AS Date), CAST(N'15:01:47' AS Time), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1, N'Swapnil', N'pune', N'pune1', N'02532215513', N'8275273474', N'nikmam.vish@gmail.com', CAST(N'2016-11-26' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, N'Test Delivary Salesman', N'Add1', N'Add2', N'1254', N'52147996', N'test@test.gmail', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (3, N'wwqq', N'wwwwwww', N'reeeee', N'042626622222', N'9421222537', N'a@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, N'1234567890123456789012345678901234567890123456789w', N'123456789012345678901234567890123456789012345678a', N'1234567890123456789012345678901234567890123456789a', N'9898999898', N'9898989898', N'ad@gmail.com', NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'12:50:31' AS Time), NULL)
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, N'APURVA INDAPURKAR', N'nashik', N'pune', N'02532215513', N'8275273474', N'nikam.vish@gmail.com', CAST(N'2017-05-30' AS Date), CAST(N'14:48:31' AS Time), NULL, CAST(N'2017-05-30' AS Date), CAST(N'14:49:36' AS Time), NULL)
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (6, N'MAYANK', N'j', N'hh', NULL, N'9876556565', NULL, CAST(N'2017-05-30' AS Date), CAST(N'14:57:13' AS Time), NULL, NULL, NULL, NULL)
INSERT [dbo].[masterdelivarysalesman] ([ID], [Name], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (7, N'DJKJDSKJ7', N'jsjsj7', N'jsjsj7', N'4545454545', N'4454545454', N'jsjsj@gmail.com', CAST(N'2017-06-02' AS Date), CAST(N'18:03:45' AS Time), NULL, CAST(N'2017-06-02' AS Date), CAST(N'18:04:29' AS Time), NULL)
GO
SET IDENTITY_INSERT [dbo].[masterdoctor] ON 

INSERT [dbo].[masterdoctor] ([DocID], [DocName], [DocAddress1], [DocAddress2], [DocTelephone], [DocEmailID], [DocShortNameAddress], [DocDegree], [DocRegistrationNumber], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DoctorCode], [MobileNumberForSMS]) VALUES (1, N'DR KKKKKKKKKKKK', N'DFKLDFK', NULL, N'30493409', N'', N'', N'MD', N'', CAST(N'2021-09-15' AS Date), CAST(N'19:35:09' AS Time), N'1', CAST(N'2021-09-15' AS Date), CAST(N'19:46:13' AS Time), N'1', NULL, N'34993049')
INSERT [dbo].[masterdoctor] ([DocID], [DocName], [DocAddress1], [DocAddress2], [DocTelephone], [DocEmailID], [DocShortNameAddress], [DocDegree], [DocRegistrationNumber], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DoctorCode], [MobileNumberForSMS]) VALUES (2, N'DR KKKKKKKKKKKK', N'DDDD', NULL, N'9422310784', N'', N'', N'', N'', CAST(N'2022-05-15' AS Date), CAST(N'23:46:15' AS Time), N'1', NULL, NULL, NULL, NULL, N'')
SET IDENTITY_INSERT [dbo].[masterdoctor] OFF
GO
SET IDENTITY_INSERT [dbo].[masteremail] ON 

INSERT [dbo].[masteremail] ([EmailId], [EmailName], [Description], [CreatedUserId], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (1, N'sheelasharma@yahoo.com', N'sheela sharma', N'1', CAST(N'2021-09-13' AS Date), CAST(N'21:31:12' AS Time), NULL, NULL, NULL)
INSERT [dbo].[masteremail] ([EmailId], [EmailName], [Description], [CreatedUserId], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (2, N'ksm@gmail.com', N'kkkkkkkkkkkkkkkkkkk', N'1', CAST(N'2021-09-15' AS Date), CAST(N'18:42:28' AS Time), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[masteremail] OFF
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (1, N'00D4026D9FBC4A33AF15252259212C64', N'CLOTRIMAZOLE+BECLOTHASONE', NULL, NULL, NULL, NULL, NULL, NULL, 252)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (2, N'00E41DA190A444B0B24ECD6F7BB780CC', N'OFLOXACIN+TINIDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 205)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (3, N'0101FD7E4A194C1EA36148601EFEC19A', N'ALBENDAZOLE+IVERMECTIN', NULL, NULL, NULL, NULL, NULL, NULL, 185)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (4, N'02161539A59D4CC1B850B50A1FCB4321', N'NIMESULIDE+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 29)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (5, N'02C02279EB4444F2961650187EB8DE53', N'LACTOLOOSE SOLUTION', NULL, NULL, NULL, NULL, NULL, NULL, 632)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (6, N'02CDAA2AEC1B4494B1AAA65E48DB5E6A', N'FEXOFENADINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 277)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (7, N'02FDE69363C5494CB1283E77CEB86E27', N'OXETACAINE,ALUMINIUM HYDROXIDE,MAG HYDROXIDE GEL', NULL, NULL, NULL, NULL, NULL, NULL, 434)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (8, N'032BF1F7EDDC4A5EA3D6C09536671BA8', N'METHYLPREDNISOLONE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 737)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (9, N'033101EC35D44423B113C0F01E74EE5C', N'DRIED', NULL, NULL, NULL, NULL, NULL, NULL, 166)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (10, N'0377F8F1F93D4EF78DA82746F1767288', N'LAXATIV', NULL, NULL, NULL, NULL, NULL, NULL, 619)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (11, N'03AE4D077AB540BEBA0A25147D782617', N'ALLOPURINOL', NULL, NULL, NULL, NULL, NULL, NULL, 680)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (12, N'04A97C19C2F147D9B0E82601598E695A', N'TRAMADOL HYDROCHLORIDE INJ', NULL, NULL, NULL, NULL, NULL, NULL, 396)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (13, N'04C1095284B0407BAD4B27384AFCCF3E', N'PREG TEST', NULL, NULL, NULL, NULL, NULL, NULL, 774)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (14, N'055AF099A7454615A3C5D8EABDE13A41', N'REMOVE STONES', NULL, NULL, NULL, NULL, NULL, NULL, 695)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (15, N'06044B0E9158409D97CAF63947D47D4A', N'LACTIC ACID,MILK PROTEIN,ALOE VERA PERINEAL WASH', NULL, NULL, NULL, NULL, NULL, NULL, 559)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (16, N'060993F601ED430DBAB9C5A081B39239', N'MEFENAMIC ACID+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 26)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (17, N'0636D9817FC84EDA805A1B31333375CA', N'LIQUID PARAFFIN+MAGNESIA', NULL, NULL, NULL, NULL, NULL, NULL, 196)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (18, N'073AA130F1364799A561658BFC46A517', N'WART REMOVEL LOTTION', NULL, NULL, NULL, NULL, NULL, NULL, 768)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (19, N'07A385BC3D0145A58546FE7E2509DB3F', N'ADAPALENE,CLINDAMYCIN PHOSPHATE GEL', NULL, NULL, NULL, NULL, NULL, NULL, 747)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (20, N'07E51CDD86F94DC3B2C3B173F2BD2942', N'THROAT LOZENGES', NULL, NULL, NULL, NULL, NULL, NULL, 102)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (21, N'084591E60B8346D09C10AEEA2D95161D', N'MINOXIDIL TOPICAL SOLUTION 5%', NULL, NULL, NULL, NULL, NULL, NULL, 577)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (22, N'0877D6D011D64F4C8A40673A1D81405B', N'CALAMINE LON', NULL, NULL, NULL, NULL, NULL, NULL, 621)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (23, N'0879C2DB741545EBB99FD54BB4B56AE3', N'LYSINE HYDROCHLORIDE+B COMPLEX', NULL, NULL, NULL, NULL, NULL, NULL, 232)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (24, N'08CF09EEAB2B4C3FAC4375FFEFF7F400', N'CALCIUM+VITAMIN D3', NULL, NULL, NULL, NULL, NULL, NULL, 210)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (25, N'08E0FF1ABA4C4A9F93B1D75CC2ECC23A', N'WH', NULL, NULL, NULL, NULL, NULL, NULL, 406)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (26, N'097CFB875F464071B8D199F09B6CEC49', N'ETORICOXIB', NULL, NULL, NULL, NULL, NULL, NULL, 18)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (27, N'09B02E00A59E47E8B5AD6840C1EC0BD3', N'DILTIAZEM HCL 30MG', NULL, NULL, NULL, NULL, NULL, NULL, 530)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (28, N'0A1B1CAB18D847A6961B3705DC9CAED4', N'OFLOXACIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 335)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (29, N'0A63BB7FA7F54314ACBC73FBF9BEB87F', N'ANTIFUNGAL ANTIBACTERIAL ANTI-INFLAMMATORY', NULL, NULL, NULL, NULL, NULL, NULL, 408)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (30, N'0A824C2BBFAE46FF9DC42F7052EA4C4A', N'ENZYME CAP', NULL, NULL, NULL, NULL, NULL, NULL, 7)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (31, N'0AE8633DC3D244618085B57007AF5DD3', N'MONTELUKAST+LEVOCETIRIZINE', NULL, NULL, NULL, NULL, NULL, NULL, 147)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (32, N'0B06696F39B14748A4D35459CE9CFDAA', N'MOMETASONE+SALICYLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 357)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (33, N'0B848DC3E0304993AF286BE8812EA879', N'COUGH LOZENGES', NULL, NULL, NULL, NULL, NULL, NULL, 274)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (34, N'0BB28957F1E044E8B4AF8E65E22FCAA6', N'CHOLECALCIFEROL SOFT CAP', NULL, NULL, NULL, NULL, NULL, NULL, 320)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (35, N'0C269C8FCB204453A70A0F33DF2B3D68', N'FUNGAL DIASTASE,PAPAIN WITH ACTIVATED CHARCOAL', NULL, NULL, NULL, NULL, NULL, NULL, 394)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (36, N'0CD33B7F8AFE40E5BAFDBF4B64826216', N'TRAMADOL HCL SR', NULL, NULL, NULL, NULL, NULL, NULL, 736)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (37, N'0EF69C9DC0C9464AA4B645A2312FFC83', N'PARACETAMOL 500 MG', NULL, NULL, NULL, NULL, NULL, NULL, 456)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (38, N'0F09A80865244BD888DAC802EC9980ED', N'OLMESARTAN MEDOXOMIL,AMLODIPINE', NULL, NULL, NULL, NULL, NULL, NULL, 746)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (39, N'0F0EA1B0FFD34CB6B9EC0BF89A175263', N'METFORMIN 500', NULL, NULL, NULL, NULL, NULL, NULL, 527)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (40, N'0F368D221A1B405E82A9F3820CE94481', N'RABEPRAZOLE&LEVOSULIPRIDE SR CAPSULES', NULL, NULL, NULL, NULL, NULL, NULL, 682)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (41, N'0F9373E227304CD793C1727022B9C912', N'SERRATIOPEPTIDASE+PARACETAMOL+DICLOFENAC POTASSIUM', NULL, NULL, NULL, NULL, NULL, NULL, 98)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (42, N'104135DAA6B341BEAC587E05CBE7E6D0', N'CLINDAMYCIN PHOSPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 239)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (43, N'10590014E2BF445EA56C605E4A794BC7', N'PANTAPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 112)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (44, N'108F28063B4943AEAED22BEFA3A4A497', N'ACECLOFENAC WITH THIOCOCHICOSIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 684)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (45, N'10BA2AF0104545B4906CBEFAAA3ED95F', N'AYURVEDIC', NULL, NULL, NULL, NULL, NULL, NULL, 362)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (46, N'10DC27CDCDE9437D9BB52AA341B9ED65', N'POVIDINE IODINE GARGLE', NULL, NULL, NULL, NULL, NULL, NULL, 425)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (47, N'10EE721BF6934EC6887DA303868395ED', N'GINSENG+IRON+CALCIUM+VITAMINS+MINERALS', NULL, NULL, NULL, NULL, NULL, NULL, 199)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (48, N'11368D8BE72443CEA3CCB728451CDE5B', N'PILE CARE', NULL, NULL, NULL, NULL, NULL, NULL, 493)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (49, N'11DCC2B12C4A4B8EB43F08474A640699', N'AMOXYCILLIN TRIHYDRATE 250 CAP', NULL, NULL, NULL, NULL, NULL, NULL, 372)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (50, N'1260510149C444F7AAF471992439C8AB', N'PET SOAE', NULL, NULL, NULL, NULL, NULL, NULL, 578)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (51, N'12CC501C761A4C4D80C69E54AFA90913', N'IVERMECTIN AND ALBENAZOLE CHEWABLE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 447)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (52, N'1344748F8FC1433BBEF7C15ED797A97B', N'BISACODYL', NULL, NULL, NULL, NULL, NULL, NULL, 178)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (53, N'135C849CF3414F9BB9EC6C66515DD6A7', N'PUDINA CAP', NULL, NULL, NULL, NULL, NULL, NULL, 349)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (54, N'137F57529950407981C633988C210F13', N'LISINOPRIL', NULL, NULL, NULL, NULL, NULL, NULL, 207)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (55, N'13CD31F318CE48A199B2243B2D38D845', N'BECLOMETHASONE DI-PROPINATE,CLOTRIMAZOE', NULL, NULL, NULL, NULL, NULL, NULL, 457)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (56, N'150AB1814CC746A1AF5D765974E7CEB9', N'PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 114)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (57, N'1569DBAAA89B42CDAF0DE56FAEE65D26', N'SANITARY PAD', NULL, NULL, NULL, NULL, NULL, NULL, 410)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (58, N'158042E404724F49803E68D436D553C3', N'CYPROHEPTADINE HCL &TRICHOLINE CITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 433)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (59, N'164A9854828C4E20A7BEC9E4256E4939', N'FERROUS ASCORBATE+FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 104)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (60, N'166CA41EDC084DA9BEAE3AE114E770DD', N'L-ARGINE ZINC AND FOLIC ACID SACHET', NULL, NULL, NULL, NULL, NULL, NULL, 670)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (61, N'16C7417A7C5A40179ECFAFBE49218307', N'FINASTERIDE', NULL, NULL, NULL, NULL, NULL, NULL, 287)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (62, N'17068B3EC5034752852ABBB1AF74CCB2', N'AMPICILLIN &DICLOXACILLIN', NULL, NULL, NULL, NULL, NULL, NULL, 52)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (63, N'17188B43C6C4492DA0E9AA36B17E4928', N'CRACK ONT', NULL, NULL, NULL, NULL, NULL, NULL, 409)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (64, N'171EE42CE44C4790862644EB1EA1D260', N'CLINDAMYCIN PHOSPHATE & NICOTINAMIDE GEL', NULL, NULL, NULL, NULL, NULL, NULL, 304)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (65, N'17863F1EE6274F409AA5415472A0DF4A', N'METFORMINE HCL SUSTAINED RELEASE', NULL, NULL, NULL, NULL, NULL, NULL, 326)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (66, N'179B361BA3884CA8BC326023FAA8AF33', N'GLIMEPRIDE,VOGLIBOSE&METFORMIN HCL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 625)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (67, N'184B30D3DF324EFFA83C14AD7411CDFF', N'TORSEMIDE 10 MG TAB', NULL, NULL, NULL, NULL, NULL, NULL, 628)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (68, N'187644BA38EA464CAB157F6D74C56AC5', N'LOSARTAN POTASSIUM', NULL, NULL, NULL, NULL, NULL, NULL, 19)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (69, N'1A19568B4CE94408979938E0043117CC', N'ARTEMETHER AND LUMEFANTRINE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 743)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (70, N'1A25CB4DAE6947FA9E2E9375A89BF147', N'CEFIXIME+AZITHROMYCIN', NULL, NULL, NULL, NULL, NULL, NULL, 563)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (71, N'1A2D46A58C8F4BD7B84C8ADB8C9D7521', N'ACECLOFENAC & PARACITAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 501)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (72, N'1BBF548E545448819272E83E4DA9838F', N'TELMISARTAN,AMLODIPIN & HYDROCHLOROTHIAZIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 566)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (73, N'1BCD1BB4081F4E2FADB3E199B7CF27F0', N'CEFUROXIME AXETIL', NULL, NULL, NULL, NULL, NULL, NULL, 17)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (74, N'1C000F1837C947B3AF5FD0166DC45CB6', N'QUINIODOCHLOR 125 MG', NULL, NULL, NULL, NULL, NULL, NULL, 546)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (75, N'1C00EF0BADBF4D1385A899C53356467B', N'LIDOCAINE TOPICAL AEROSOL', NULL, NULL, NULL, NULL, NULL, NULL, 300)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (76, N'1C0AB7F273CA492DA50FFEFAE2CA62F4', N'NIMESULIDE & DICLOFENAC SODIUM', NULL, NULL, NULL, NULL, NULL, NULL, 398)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (77, N'1C42C238F220430A9EA2B35A21DBB0F4', N'TRANEXAMIC INJ', NULL, NULL, NULL, NULL, NULL, NULL, 750)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (78, N'1C9CE19BAD7D4162BFD0C637B7546CC9', N'MOMETASONE', NULL, NULL, NULL, NULL, NULL, NULL, 356)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (79, N'1E16759CA18B48A8B6A95DCA4A7ACED3', N'TEETHING PILES', NULL, NULL, NULL, NULL, NULL, NULL, 558)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (80, N'1E832E6AD929464CBFA4AF4CF1200222', N'AMOXICIIIN & DICLOXACILLIN CAP', NULL, NULL, NULL, NULL, NULL, NULL, 639)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (81, N'1EE134276F38454084FFD41AFCDCD1F0', N'PARACETAMOL 500', NULL, NULL, NULL, NULL, NULL, NULL, 72)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (82, N'1F82281F331C4FF5B4E007FFDB58C086', N'CLARITHROMYCIN 250 MG', NULL, NULL, NULL, NULL, NULL, NULL, 382)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (83, N'200BE9039FD44F34BA01F679EABC4F1F', N'BECLOMETHASONE DIPROPIONATE,CLOTRIMAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 789)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (84, N'200BFF1F1F324D379B2A3133FFF3EFB1', N'AMLODIPINE 2.5 MG', NULL, NULL, NULL, NULL, NULL, NULL, 307)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (85, N'207E5C2C30E54F32857AEC0FE4A78333', N'PARACETAMOL 325MG & DOMPERIDONE 10', NULL, NULL, NULL, NULL, NULL, NULL, 328)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (86, N'20D578A0EB90410BA115C6A6378B9981', N'DISODIUM HYDROGEN CITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 293)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (87, N'2232AE614AF94FB9A52DF0C1C32AB6BD', N'NADIFLOXACIN CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 545)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (88, N'225942736A9547BE9AEE289C20F8A549', N'PROTIN TONIC WITH IRON VITAMINS & MINERALS', NULL, NULL, NULL, NULL, NULL, NULL, 790)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (89, N'22E8EADBA3EC419BAD740875F8665BAD', N'BECLOMETHASONE DIPROPIONATE+NEOMYCIN SULPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 361)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (90, N'232E5EA85D3E4B309D39C10A806F2B41', N'PROPOFOL INJ.', NULL, NULL, NULL, NULL, NULL, NULL, 587)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (91, N'2359CB2780254746908215D5C00D3691', N'IBUGESIC', NULL, NULL, NULL, NULL, NULL, NULL, 370)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (92, N'23644EB03C8B420FA336BD4D8092F207', N'MULTIVITAMINS WITH MECOBALAMIN DROP', NULL, NULL, NULL, NULL, NULL, NULL, 690)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (93, N'2367A1E599FF4109A004700A1AF6E910', N'NEBIVOL HCL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 723)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (94, N'2368955CE0664A55BFB2E26748E25016', N'ANTI-SEPTIC LIQ', NULL, NULL, NULL, NULL, NULL, NULL, 580)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (95, N'24229D0AF206461DB67EC6E953CD4356', N'MICROTAPE', NULL, NULL, NULL, NULL, NULL, NULL, 520)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (96, N'2446BE8034054C23AB1D419C7BD5C131', N'NIMESULIDE MD', NULL, NULL, NULL, NULL, NULL, NULL, 109)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (97, N'2463F5A58C29473DBE0382E20D3B6F60', N'CHLORAMPHENICOL & BETAMETHASONE SOD PHOSPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 375)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (98, N'24658E512E5543F5996846899FEED6F1', N'NEEM SOAP', NULL, NULL, NULL, NULL, NULL, NULL, 574)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (99, N'2516E5B9364A4FC5A51718A158E28319', N'ROXITHROMYCIN', NULL, NULL, NULL, NULL, NULL, NULL, 175)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (100, N'259639D41C944459B173C01AB245C58B', N'HEMATINIC,IRON,FOLIC ACID,VITAMIN-B12', NULL, NULL, NULL, NULL, NULL, NULL, 572)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (101, N'25EBC5775A0F400FB2C2BDA37E45AECD', N'MOUTH ULCER GEL', NULL, NULL, NULL, NULL, NULL, NULL, 253)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (102, N'26191F97F01D41E685E3467FC8DC6661', N'NITROFURANTOIN SR 100MG TAB', NULL, NULL, NULL, NULL, NULL, NULL, 699)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (103, N'261C1A37B96444D2932E025215282FC4', N'MEBENDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 224)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (104, N'26C38B5802E248709B5E755752682AF2', N'ERYTHROMYCIN STEARATE', NULL, NULL, NULL, NULL, NULL, NULL, 325)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (105, N'27EAA4C3894944FA841D80C370806645', N'ONDANSETRON ORALLY DISINTEGRATING', NULL, NULL, NULL, NULL, NULL, NULL, 46)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (106, N'280F0338D0CA4C3E95D2D0631FE3B687', N'CIPROFLOXACIN AND TINIDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 436)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (107, N'281CEA376EA348989716073E00E3E1F5', N'NICORANDIL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 683)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (108, N'2824CEDEE9DF49F8AB79FAB3B0061B3E', N'CLOPIDOGREL & ASPIRIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 421)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (109, N'290B571C37FE4AB4AF64405B3514731A', N'DEXAMETHASONE SOD.PHOSPHATE & CHLORAMPHENICOL', NULL, NULL, NULL, NULL, NULL, NULL, 377)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (110, N'29215F64286342FCA63F5209F33E41D8', N'TAMSULOSIN HCL', NULL, NULL, NULL, NULL, NULL, NULL, 526)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (111, N'2944D360C4024A8FAE229F0F11ED0540', N'KETOCANAZOL', NULL, NULL, NULL, NULL, NULL, NULL, 474)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (112, N'29D966FF78E54E1689FFE8F8429671AA', N'MICONAZOLE &FLUOCINOLONE', NULL, NULL, NULL, NULL, NULL, NULL, 53)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (113, N'29F6766F864E4A4EAEE799D2487D5E26', N'ALBENDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 86)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (114, N'2A69B9DFD2644ED985D16D3BDA6771F3', N'OFLOXACIN AND ORNIDAZOLE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 336)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (115, N'2B7C653B0A8840178EFC4F4921569945', N'RAMIPRIL 5MG', NULL, NULL, NULL, NULL, NULL, NULL, 528)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (116, N'2B7F1B11D0A743F0B9D78613129D1DED', N'ADULT DAIPER', NULL, NULL, NULL, NULL, NULL, NULL, 429)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (117, N'2B8B08F507C64CA8BF0D0714D28365B3', N'RABEPRAZOLE SODIUN', NULL, NULL, NULL, NULL, NULL, NULL, 76)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (118, N'2C4FB86F3F5048A181F3E1935A734E3B', N'ACETAMINOPHEN AND TRAMADOL HYDROCHLORIDE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 444)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (119, N'2CA3A00671AE42E7B887AD4BDAF5E805', N'PRE & PROBIOTIC SACHET', NULL, NULL, NULL, NULL, NULL, NULL, 717)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (120, N'2CD1492A180540B2841D0529EEE5FAD2', N'MEFENAMIC ACID+DICYLOMINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 133)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (121, N'2D1827A4E21D4AC79A161BEAD44B0FEE', N'ANTIMIGRAINE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 6)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (122, N'2E0283509AFC4CFB93322A3C027793CF', N'ETORICOXIB+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 190)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (123, N'2E1C8CFA484D4BF1A2B7926A29DAD6E9', N'CETIRIZINE HCL & AMBROXOL HCL', NULL, NULL, NULL, NULL, NULL, NULL, 386)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (124, N'2F303CA8B5B5489F9A7CF95F016FCF11', N'GLUCOMITER', NULL, NULL, NULL, NULL, NULL, NULL, 519)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (125, N'2F4348D641A240A793C8EFBCE01366F3', N'CEFIXIME 100MG TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 449)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (126, N'2F440278723A4B759F5BB7A976EE489A', N'GLIMEPRIDE,VOGLIBOSE & METFORMIN HCL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 626)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (127, N'2FAA2EE144DA41D286F54903E3A66E60', N'PERMETHRIN LON', NULL, NULL, NULL, NULL, NULL, NULL, 766)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (128, N'2FB51B507AD74637B30A5AE8790093C0', N'HEALTH POWDER', NULL, NULL, NULL, NULL, NULL, NULL, 130)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (129, N'2FCA2CD7FB424E7C811317EF7F1AEE43', N'ENALAPRIL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 334)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (130, N'2FCFF281ABCA4A85A6F16963AC4C6018', N'METFORMINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 66)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (131, N'3044BB337A614B82BD2CDD137C9ECE43', N'DIMINISHES&CLEARE SCARS,MARKS', NULL, NULL, NULL, NULL, NULL, NULL, 258)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (132, N'307E98DD1A6B49FF9826B510A107EB8E', N'EBASTINE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 658)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (133, N'31A94C13621E401FB3AD2FFC8602E4E1', N'NIMESULIDE', NULL, NULL, NULL, NULL, NULL, NULL, 108)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (134, N'31B58BB9378343B2992959EB521C52B9', N'CINNARIZINE AND DOMPERIDONE', NULL, NULL, NULL, NULL, NULL, NULL, 679)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (135, N'3210E401268844B88E6A8E4DE7085CED', N'SUTURE', NULL, NULL, NULL, NULL, NULL, NULL, 585)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (136, N'3214B3715DC542DEB145E98D269107DE', N'CEFIXIME', NULL, NULL, NULL, NULL, NULL, NULL, 211)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (137, N'3285D20D202841169A2C9299B81C16CF', N'WELL BALANCE TONIC FOR INFANTS.', NULL, NULL, NULL, NULL, NULL, NULL, 616)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (138, N'333D908AEE1F4FAD80C3757385D7CA0D', N'TELMISARTAN 40', NULL, NULL, NULL, NULL, NULL, NULL, 539)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (139, N'3439675C39114C489BC24DA8DDD0B5B8', N'CLIN', NULL, NULL, NULL, NULL, NULL, NULL, 573)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (140, N'34ED2DA7ED6B4E44962B8B0C38D01650', N'BETAHISTINE HCL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 665)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (141, N'34FA5FD032944D4CBF34AD8FE64C7A0D', N'CALCITRIOL,CAL-CAR,METHY,V-K2-7,L-METH,ZINC&MAG', NULL, NULL, NULL, NULL, NULL, NULL, 635)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (142, N'352744AEF8604865B32649EB10409796', N'BECLOMETHASONE DIPROPIONATE,NEOMYCIN,CLOTRIMAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 257)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (143, N'35B8B29D3AAC4A8FACDE17A578BDCF25', N'ROSUVASTATIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 350)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (144, N'35B926BCE510463492B647098505C30D', N'SILDENAFIL CITRATE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 780)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (145, N'35C452264AB849DD9F728A822BD19173', N'SKIN SOAP', NULL, NULL, NULL, NULL, NULL, NULL, 576)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (146, N'3649861445EC45759DC992CCE8047012', N'DOMPERIDONE', NULL, NULL, NULL, NULL, NULL, NULL, 118)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (147, N'36635B1EC65C4A488D4103C134E6C24A', N'OMEPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 111)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (148, N'366D855C6FA249D2B7DB225C84C7D71C', N'SILDENAFIL CITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 16)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (149, N'36707D0615914E8B9F676BDBF1D190A8', N'NIMESULIDE GEL', NULL, NULL, NULL, NULL, NULL, NULL, 678)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (150, N'3790AB4D2C674B178E1657603A2C992A', N'AZITHROMYCIN-500', NULL, NULL, NULL, NULL, NULL, NULL, 39)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (151, N'37CFECB148F54338864D64B0C5DD4BCA', N'CHLORHEXDINE GAUZE DRESSING', NULL, NULL, NULL, NULL, NULL, NULL, 542)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (152, N'38046925C1514E28963DEC8E68EBD619', N'TELMISARTAN', NULL, NULL, NULL, NULL, NULL, NULL, 264)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (153, N'153', N'2-PROPANOL 1-PROPANOL rrrrrrr', NULL, NULL, NULL, CAST(N'2017-03-23' AS Date), CAST(N'11:27:54' AS Time), NULL, NULL)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (154, N'38CEE865EB014F08B5D3A1D3408809F9', N'CEFTRIOXANE INJ', NULL, NULL, NULL, NULL, NULL, NULL, 391)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (155, N'3969241FD4AB4DE49FE80852C2301983', N'S-AMLODIPIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 644)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (156, N'39823B1776484EC39CAEFB536F071EB5', N'CALCITROL+CALC.CORBONATE&ZINC', NULL, NULL, NULL, NULL, NULL, NULL, 5)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (157, N'39C23F2541D3496F86E83B27BF7F009F', N'LEVONORGESTREL 1.5MG', NULL, NULL, NULL, NULL, NULL, NULL, 560)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (158, N'3A516345FC144FF3B24BC092DB3A6679', N'AMLODIPINE+ATENOLOL', NULL, NULL, NULL, NULL, NULL, NULL, 127)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (159, N'3AC93AC010324311B8CBCFD380972E6E', N'ANTACID', NULL, NULL, NULL, NULL, NULL, NULL, 522)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (160, N'3BB8763F00B447C8BEC9C2727B48764E', N'DISPERSIBLE AMOXICICILIN TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 422)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (161, N'3BEEC9475CE24CA984E13CA57E2D85A2', N'CLOTRIMAZOLE WITH BECLOMETHASONE', NULL, NULL, NULL, NULL, NULL, NULL, 44)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (162, N'3C3F8691F361473ABEA5D4FC565B4895', N'CONDOMS', NULL, NULL, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (163, N'3C5CE8D2CDDF4BF6A2EAF30EDD6AB1C8', N'ZINC OXIDE CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 698)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (164, N'3CEB155CEEF6424B95670B8C00B20675', N'SERRATIOPEPTIDASE+NIMESULIDE', NULL, NULL, NULL, NULL, NULL, NULL, 222)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (165, N'3DDFFA0AB29A4781B6D8B5B7965EFE26', N'ATORVASTATIN', NULL, NULL, NULL, NULL, NULL, NULL, 22)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (166, N'3E8BE1D9871D4A19AC222C8D167C9E5C', N'CALC', NULL, NULL, NULL, NULL, NULL, NULL, 634)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (167, N'3ECACCC3CC104A6DBB517699601AE4D5', N'GABAPENTIN USP CAP', NULL, NULL, NULL, NULL, NULL, NULL, 488)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (168, N'3F675FB9F9404D9A829E861E086A501C', N'FAMOTIDINE', NULL, NULL, NULL, NULL, NULL, NULL, 364)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (169, N'3F7A92B78626408EAD341AF1CA6FF04D', N'PAINKILLER SPRAY', NULL, NULL, NULL, NULL, NULL, NULL, 32)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (170, N'3F8D8F26DBB24C31928928D5D94D7754', N'FLUCANA', NULL, NULL, NULL, NULL, NULL, NULL, 171)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (171, N'3FC908844D4B4CE2858BB0D4CCDD9825', N'ACARBOSE', NULL, NULL, NULL, NULL, NULL, NULL, 400)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (172, N'3FCD03399E084D6E85503304C3E78BA8', N'NOREHTHISTERONE TAB. 5 MG.', NULL, NULL, NULL, NULL, NULL, NULL, 412)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (173, N'3FDE6263C5C64224A6363AB76F1EF01B', N'IRON+FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 204)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (174, N'40051DC63EE54400AB529C3669C2F228', N'RANITIDINE HYDROCHLORIDE', NULL, NULL, NULL, NULL, NULL, NULL, 238)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (175, N'402980068978471E87875779560B2ABE', N'GRISEOFULVIN WITH BETACYCLODEXTRIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 636)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (176, N'40A31280F4214379BAA289F6071C49B0', N'NITZOXINADE AND OFLOXACIN', NULL, NULL, NULL, NULL, NULL, NULL, 583)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (177, N'411AF6017D644DB7808618BB49135D8E', N'MULTI', NULL, NULL, NULL, NULL, NULL, NULL, 373)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (178, N'4137B718034A4132B65069F890DD4151', N'ES', NULL, NULL, NULL, NULL, NULL, NULL, 510)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (179, N'41504BFD5D1D4C21A573C54568A8DC2A', N'KETOCONAZOLE SOAP', NULL, NULL, NULL, NULL, NULL, NULL, 404)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (180, N'420565FAE6404210B9DE7F732EF83EA0', N'PREGABALIN+MECOBALAMIN', NULL, NULL, NULL, NULL, NULL, NULL, 260)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (181, N'42E06450D19347508AD5790EE5E65F26', N'ERYTHROMYCIN PLUS BROMEHXIN', NULL, NULL, NULL, NULL, NULL, NULL, 627)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (182, N'42E615CE5C5A452587C5FF5F65DF8E45', N'CYPROHEPTADIN HCL', NULL, NULL, NULL, NULL, NULL, NULL, 93)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (183, N'42F6CA631FC6414CA4523D507C74D970', N'OXYMETAZOLINE HYDROCHLORIDE NASAL SOLU', NULL, NULL, NULL, NULL, NULL, NULL, 54)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (184, N'43188F11E24545FDB4ADCE44FAC9FDD4', N'TOBRAMYCIN+DEXAMETHASONE', NULL, NULL, NULL, NULL, NULL, NULL, 380)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (185, N'432AE528EF23472AA850CFD44CD9DCF4', N'VIT B12 INJ', NULL, NULL, NULL, NULL, NULL, NULL, 647)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (186, N'43AD00F907DC46B3B83E808EC5BE1B2E', N'PIROXICAM DISP.20MG', NULL, NULL, NULL, NULL, NULL, NULL, 3)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (187, N'44519201AF774AF7834712470B9CF371', N'DISULFIRAM TAB. 500 MG', NULL, NULL, NULL, NULL, NULL, NULL, 494)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (188, N'4454044C06E9452CBD3E18C9D93C5C3C', N'B-COMPLEX+FOLIC ACID+L-LYSINE', NULL, NULL, NULL, NULL, NULL, NULL, 131)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (189, N'44A440C700A248AA8B31403020D7A60F', N'DAILY HEALTH SUPPLEMENT', NULL, NULL, NULL, NULL, NULL, NULL, 630)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (190, N'44B091E484C241D49B17D1936D3D3E0A', N'TRAMADOL HCL', NULL, NULL, NULL, NULL, NULL, NULL, 115)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (191, N'452527CF32124E09BA37FD198684C499', N'MOSAPRIDE', NULL, NULL, NULL, NULL, NULL, NULL, 164)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (192, N'4534CAD0261748969FA097575A7EED31', N'TELMISARTAN+HYDROCHLOROTHIAZIDE', NULL, NULL, NULL, NULL, NULL, NULL, 265)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (193, N'45711ED23D714A979A273F6AD436FFC4', N'BENZYDAMINE MOUTHWASH', NULL, NULL, NULL, NULL, NULL, NULL, 701)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (194, N'458EC4ED75F74D46B9F24B94977A97BA', N'PROTEIN WITH IRON,VIT,MINERALS', NULL, NULL, NULL, NULL, NULL, NULL, 431)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (195, N'45AAD7FF8FB8463E9D21A93162152709', N'TELMISARTAN & HYDROCHLOROTHIAZIDE', NULL, NULL, NULL, NULL, NULL, NULL, 302)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (196, N'45D40EDC4D564FB3AA0455E4F8971B8E', N'URSODIOL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 792)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (197, N'45DE14D9894444BBAF9576886318040A', N'CEFIXIM DISPERSABLE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 724)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (198, N'46631243D1AE4CFCB3B9FDB72FE55422', N'ENALAPRIL MALEATE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 534)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (199, N'47509839B6044DA2B8E6C29655FC889B', N'SURGICAL', NULL, NULL, NULL, NULL, NULL, NULL, 376)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (200, N'47A86CAFFDA84097B7778BF82B7401AB', N'PARA,NIMU,CAFFINE,PHENYLEPHRINR,CETRI', NULL, NULL, NULL, NULL, NULL, NULL, 776)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (201, N'47C62A0012BC49039485299338E75668', N'MEROPENEM 1000 INJ', NULL, NULL, NULL, NULL, NULL, NULL, 490)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (202, N'47E2DAC450C6445D9190719E8F2461CF', N'GLICLAZIDE 80', NULL, NULL, NULL, NULL, NULL, NULL, 536)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (203, N'47EB6F9F196442A0B27882BF9C43EF08', N'RANITADINE HYDRO.150', NULL, NULL, NULL, NULL, NULL, NULL, 8)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (204, N'480674BA48DE474B9E7F9998B9CE6C54', N'SODIUM CHLORIDE,SALINE NASAL SOLUTION', NULL, NULL, NULL, NULL, NULL, NULL, 163)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (205, N'48C6611ABD3A43888ACC1B52D216ACEA', N'ANTIOXIDANT', NULL, NULL, NULL, NULL, NULL, NULL, 218)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (206, N'48C67132C7E34EBCA98E35624154037C', N'TAMOXIFEN CITRATE TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 498)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (207, N'49AACCB46CD1434C935307A518E16101', N'MULTIPURPOSE SOLUTION FOR CONTACT LENSE', NULL, NULL, NULL, NULL, NULL, NULL, 592)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (208, N'4A0DEB21C14947B8B945933F8D726720', N'PROTEIN CONCENTRATE WITH VIT+IRON+MINERALS', NULL, NULL, NULL, NULL, NULL, NULL, 74)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (209, N'4A19726B467B4D8E9D62A2EAB84E9008', N'SILDENAFIL 50MG', NULL, NULL, NULL, NULL, NULL, NULL, 781)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (210, N'4A223B26A2144DDB83E2598CB8EBEE68', N'LINEZOLID TAB', NULL, NULL, NULL, NULL, NULL, NULL, 507)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (211, N'4A3DC194790747EFB037759ED23C0928', N'GINSENG+VITAMINS+MINERALS', NULL, NULL, NULL, NULL, NULL, NULL, 119)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (212, N'4AB806C7FB434AA5863B226FC8C618E1', N'ACECLOFENAC 200MGSR', NULL, NULL, NULL, NULL, NULL, NULL, 145)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (213, N'4B5F4F78D0D54F49A4E7BA335D9FF182', N'METENAMIC ACID+DICYCLOMINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 81)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (214, N'4B5FA89BDCA14AEE83FCD19B9A16D908', N'TRAMADOL HCL &PARACETAMOL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 712)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (215, N'4B7F08F1CC694887BCE2C64265A9DE38', N'CYPROHEPTADINE HYDROCHLORIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 339)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (216, N'4BFD02A730454AA6BD598ADCD2EB0355', N'SODIUM BICARBONATE PEPPERMINT OIL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 467)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (217, N'4C22E9B27B1048719F82DBDC0F757C1C', N'PARACETAMOL 650', NULL, NULL, NULL, NULL, NULL, NULL, 221)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (218, N'4C680749EF2E4B9A92633B0E9F017226', N'MONTELEUKAST 10MG', NULL, NULL, NULL, NULL, NULL, NULL, 392)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (219, N'4C6C2E8845E344929ECCF854FFE4CAF2', N'GLIMEPRIDE 1MG & METFORMIN HCL 500MG SR', NULL, NULL, NULL, NULL, NULL, NULL, 638)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (220, N'4CEBCCD4A29F4F94BE771F43E3F961DE', N'RUPATADINE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 659)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (221, N'4D080ABC8C0A44F48706A6F69ECF9A13', N'CALCITRIOL CALCIUM & ZINC', NULL, NULL, NULL, NULL, NULL, NULL, 316)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (222, N'4D34CB8A40AA44779052A4E296C142DC', N'METHYLERGOMETRINE MALEATE', NULL, NULL, NULL, NULL, NULL, NULL, 27)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (223, N'4D3E5C4D6FFE46EA93CBC201F0618839', N'AMLODIPINE 10', NULL, NULL, NULL, NULL, NULL, NULL, 374)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (224, N'4D8C3A78FD574A9E83606F1EB0B81E9A', N'VITALITY ENHANCER EITH GOLD,MUSALI, ASHWAGANDHA', NULL, NULL, NULL, NULL, NULL, NULL, 714)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (225, N'4E9AC5BCD5F5443C9E7ABD0316068A76', N'SIL ROSE CONDOM', NULL, NULL, NULL, NULL, NULL, NULL, 517)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (226, N'4ECAA3C340174C7BB874F60E0FCCD823', N'DAIY FACE WASH', NULL, NULL, NULL, NULL, NULL, NULL, 575)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (227, N'4F07A6116FFE4B0A9845DB74D079876D', N'VALSARTAN & HYDROCHLOROTHIAZIDE', NULL, NULL, NULL, NULL, NULL, NULL, 726)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (228, N'4F99EB84351F4FF6B0157C49D4D3D307', N'DICLOFENAC GEL', NULL, NULL, NULL, NULL, NULL, NULL, 203)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (229, N'5002FCE3F1F640F78EAE725A46D22B6E', N'TRAMADOL HYDROCHLORIDE', NULL, NULL, NULL, NULL, NULL, NULL, 25)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (230, N'50A7A64F79BA42F2AEDF663F832C788E', N'FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 117)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (231, N'50B8C8581C34454FA95669ED85D1456B', N'CINNARIZINE', NULL, NULL, NULL, NULL, NULL, NULL, 116)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (232, N'5122CD9C8EC9491B8051A6FFF7E66697', N'AMIKACIN 250 INJ.', NULL, NULL, NULL, NULL, NULL, NULL, 543)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (233, N'51405E6757B540949467214628B5B0E1', N'BECLOMETHASONE DIPROPINATE+CHINOFORM', NULL, NULL, NULL, NULL, NULL, NULL, 360)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (234, N'516278B6F1454A379E3747292DFB8841', N'ACRIFLAVINE HCL,THYMOL,CETRIMIDE,EMULSIFYING WAX', NULL, NULL, NULL, NULL, NULL, NULL, 355)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (235, N'518ED7B17BD64F1EA3E5F03CA13625C0', N'TAMSULOSIN PROLONGED-RELEASE CAP', NULL, NULL, NULL, NULL, NULL, NULL, 620)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (236, N'520627EDFB7442A1B681B1305086E01C', N'MECOBALAMIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (237, N'52876070D10C424E927763057810522A', N'OMEPRAZOLE+DOMPERIDONE', NULL, NULL, NULL, NULL, NULL, NULL, 142)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (238, N'531360CE554B48B38C77D71FDB9FDB96', N'URS', NULL, NULL, NULL, NULL, NULL, NULL, 641)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (239, N'5326B94A9787484CB8BF15B7E138C1BB', N'TERBINAFINE HEL', NULL, NULL, NULL, NULL, NULL, NULL, 285)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (240, N'53372DF98D96404CB3035988B4E73370', N'PROGESTERONE  300 SR', NULL, NULL, NULL, NULL, NULL, NULL, 685)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (241, N'5417AE4A5F644E49A559AE59D36F8BEE', N'S-AMLODIPIN', NULL, NULL, NULL, NULL, NULL, NULL, 640)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (242, N'54BACD09001F4ECE9D036B5C10ED71D6', N'URSODIOL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 791)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (243, N'54DB00A31F8545778BBAC3DBD41B6557', N'ZINC ACETATE SYP', NULL, NULL, NULL, NULL, NULL, NULL, 614)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (244, N'552FEB26CC8C4866ADE8D2685F01D2C5', N'AMIKACIN INJ', NULL, NULL, NULL, NULL, NULL, NULL, 209)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (245, N'5535A2DC73FD423C95371D9E9E7F214E', N'LEMON GREEN TEA', NULL, NULL, NULL, NULL, NULL, NULL, 557)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (246, N'55C5F7737665493F8AB1092D42F25616', N'ISOTRETINOIN SOFTGEL CAP', NULL, NULL, NULL, NULL, NULL, NULL, 469)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (247, N'55EF3B8083C24D98B3209081E8DBFD25', N'GREEN TEA', NULL, NULL, NULL, NULL, NULL, NULL, 556)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (248, N'5665C984557C4BECB5ECA4F05B0DDF79', N'ATORVASTATIN AND FENOFIBRATE', NULL, NULL, NULL, NULL, NULL, NULL, 588)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (249, N'5690254CDD3F4EB1BD1B3184B78C7035', N'VALPROATE AND VALPROIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 674)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (250, N'569CBC0AA787400798E0BC5BCDD9CA0D', N'VOGLIBOSE', NULL, NULL, NULL, NULL, NULL, NULL, 267)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (251, N'57AFE1DC97ED484F9FE51E53F10776BD', N'VOGLIBOSE 0.2,GILIMEPRIDE 2MG,METFORMIN 500MG', NULL, NULL, NULL, NULL, NULL, NULL, 753)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (252, N'59F547D288E04F7B85AEA44F9B8A030E', N'LOPERAMIDE HYDROCHLORIDE', NULL, NULL, NULL, NULL, NULL, NULL, 225)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (253, N'5A34290BA7C045FBA74DF72DDEC21D57', N'CHLORAMPHENICOL', NULL, NULL, NULL, NULL, NULL, NULL, 324)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (254, N'5A59C544B9444779938ED81704AC233C', N'CHLORPHENIRAMINE MALEATE AND PHENYLEPHRINE HCL CAP', NULL, NULL, NULL, NULL, NULL, NULL, 480)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (255, N'5B305D779E3F4E4B8F7FFEB605792F5A', N'CHLORAMPHENICOL EYE OINT', NULL, NULL, NULL, NULL, NULL, NULL, 348)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (256, N'5B97A12775164727A285E2222336C7E6', N'ZINC GLUCONATE WITH PRE &PROBIOTIC ORAL SUSP.', NULL, NULL, NULL, NULL, NULL, NULL, 497)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (257, N'5B9E6148134B47578D787FFEFE6DAB4C', N'FEBUXOSTAT 80 MG TAB', NULL, NULL, NULL, NULL, NULL, NULL, 491)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (258, N'5BA8A777C33748E8BAD5153DD0089E65', N'METOPROLOL TARTRATE IP 25 TAB', NULL, NULL, NULL, NULL, NULL, NULL, 540)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (259, N'5C28CBB834814DACA1AACA558808429F', N'CALCIUM CITRATE MALATE,VITAMIN D3,FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 773)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (260, N'5C2CF7CEEBD64114B7FE5C10EB1BD1FD', N'PRAZOSIN HYDROCLORIDE EXTEND RELEASE TAB 2.5 MG', NULL, NULL, NULL, NULL, NULL, NULL, 508)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (261, N'5C36DB1055C849B780475BE6E6B1C298', N'DRIDE ALUMINIUM HCL+MAGNESIUM HEL+OXETACAINE SUSP', NULL, NULL, NULL, NULL, NULL, NULL, 71)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (262, N'5C4700723EF24EE890B757B070F0E476', N'THYMOL+ICHTHAMMOL+MENTHOL+LIGNOCAINE', NULL, NULL, NULL, NULL, NULL, NULL, 244)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (263, N'5C5EC3C1F6D84053AE258289FFC83EBB', N'URIN BAG', NULL, NULL, NULL, NULL, NULL, NULL, 590)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (264, N'5C9331856EF9423697178A34ED851199', N'SOLUTION OF SORBITOL AND TRICHOLINE CITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 599)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (265, N'5CD4C5013A384103B646CB880E8BE8D7', N'LOSARTAN & AMLODIPINE TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 607)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (266, N'5D7F99C13CAC4F99BE8476D6D108A8CF', N'ACECLOFENAC SR', NULL, NULL, NULL, NULL, NULL, NULL, 734)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (267, N'5DB73B03601D40A588E5D5A77092A30B', N'HIGHLY PURIFIED CHORIONIC GONADOTROPHIN INJ', NULL, NULL, NULL, NULL, NULL, NULL, 589)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (268, N'5F0449DABA7044C8A826A1801FB7EDBB', N'KETOCONAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 87)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (269, N'5F5A7F3C26BB41A8B1894BC41FFACA66', N'ACECLOFENAC+PARACETAMOL+CHLORZOXAZONE', NULL, NULL, NULL, NULL, NULL, NULL, 107)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (270, N'5FB77A59414C457797BB17A5B7C4CD28', N'DEXAMETHASONE SODIUM PHOSPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 176)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (271, N'5FEB3B6C7F514F8DAA191BD4DE8D3D41', N'KETOROLAC TROMETHAMINE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 596)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (272, N'5FFDDD5F138641EEA5462228F463438D', N'CALCIUM OROTATE', NULL, NULL, NULL, NULL, NULL, NULL, 318)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (273, N'60AD6690A03C4F6287C3C592117B6DD6', N'ANTI ICHING CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 477)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (274, N'60CFB3490480455795A330F50FD8E994', N'SERRATIOPEPTIDASE+DICLOFENAC SODIUM', NULL, NULL, NULL, NULL, NULL, NULL, 99)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (275, N'60DC40D52C59438FB9AA61E634DC8914', N'PREBIOTIC & PROBIOTIC CAPSULE.', NULL, NULL, NULL, NULL, NULL, NULL, 473)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (276, N'60F8A922AEC242FEAAF3E6A246DC7419', N'INHALANT CAP', NULL, NULL, NULL, NULL, NULL, NULL, 282)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (277, N'611502FBCE574339BCBB668CED49AADA', N'PANTOPROZOL DOMPREDONR SR', NULL, NULL, NULL, NULL, NULL, NULL, 402)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (278, N'6140496FA9A84AE6A7037EB8DC9E9D7B', N'DIACEREIN', NULL, NULL, NULL, NULL, NULL, NULL, 201)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (279, N'618C8BF9B379406A97F8F9AF459956E2', N'LEVOCETIRIZINE DIHYDROCHLORIDE,AMBROXOL HCL', NULL, NULL, NULL, NULL, NULL, NULL, 689)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (280, N'618E42E5A8CD4BADB16FE9A913D9929D', N'DULOXETINE HCL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 664)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (281, N'619860DE2BDD420DBA99B4C489378794', N'VITAMIN C CHEWABLE TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 438)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (282, N'62A7BD60ADB6462A83CC5580489EFA36', N'FLUCONAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 105)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (283, N'632C66ADA26246D194517DAB9E62B220', N'CALCITROL+CALCIUMCARBONATE+ZINC', NULL, NULL, NULL, NULL, NULL, NULL, 12)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (284, N'6395C0A1DFF546DA92F5E54B4C63656D', N'DEXAMETHASONE+CHLORAMPHENICOL', NULL, NULL, NULL, NULL, NULL, NULL, 250)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (285, N'644714A2F45B476A98034D155B5E261F', N'DIACERIN CAP.', NULL, NULL, NULL, NULL, NULL, NULL, 492)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (286, N'656AA1236F504C9493DEBC250E3397FD', N'FOLIC ACID TAB', NULL, NULL, NULL, NULL, NULL, NULL, 764)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (287, N'65EEECC0FDA644ADB691C29C4CFCF97F', N'LORNOXICAM', NULL, NULL, NULL, NULL, NULL, NULL, 296)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (288, N'669BBA42FEB3465E8E96EA2A7E867EAD', N'GABAPENTIN AND METHYLCOBALAMIN', NULL, NULL, NULL, NULL, NULL, NULL, 645)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (289, N'66A124BE31F84A8BA3469441D711DC2C', N'MECOBALAMIN-1500MCG', NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (290, N'672950A5F41A4F8988CD1F5AB385D6C7', N'TELMISARTAN TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 782)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (291, N'672EF41292354A52AA68FCF7C2FA4FD3', N'CLOTRIMAZOLE P/W', NULL, NULL, NULL, NULL, NULL, NULL, 694)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (292, N'6781726C43BB40759D2A995F12DC8892', N'KETOCONAZOLE SHAMPOO 2 %', NULL, NULL, NULL, NULL, NULL, NULL, 788)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (293, N'67D6052E51F642A69C7AB0195DE4980B', N'GLIMEPRIDE & METFORMIN HCL', NULL, NULL, NULL, NULL, NULL, NULL, 437)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (294, N'68D2D89DCC144188ABC02E1F4979A57A', N'CITICOLINE SODIUM TAB', NULL, NULL, NULL, NULL, NULL, NULL, 499)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (295, N'68DA73AA88F046C794FB2436E8D4605F', N'CALCITROL,METHYCOBALMINE DHA,FOLIC ACID ,BORAN CAL', NULL, NULL, NULL, NULL, NULL, NULL, 594)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (296, N'68F996B5093B4B80AD15D47C1B551FC5', N'MONTELUKAST SOD AND FEXOFENADINE HCL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 763)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (297, N'69806C89E1F5492591F7A99850AAC754', N'CEFIXIME+OFLOXACINE', NULL, NULL, NULL, NULL, NULL, NULL, 371)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (298, N'69D12BF6B0D746AAA07DDC3751E28130', N'GLICLAZIDE AND METFORMIN HYDROCHORIDE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 450)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (299, N'6A35334186014C98AA935CAA690CB957', N'PARACETAMOL+ACECLOFENAC', NULL, NULL, NULL, NULL, NULL, NULL, 128)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (300, N'6A3F3BAA582C41C9A44B8E180EFECF00', N'CALCIUM WITH VITAMIN D3', NULL, NULL, NULL, NULL, NULL, NULL, 40)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (301, N'6A4D569E35F54906A47D7C2BD2DFCE3A', N'PROTEIN POWDER', NULL, NULL, NULL, NULL, NULL, NULL, 75)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (302, N'6A78270D7DED45A5B9AD5E321DA1428F', N'50', NULL, NULL, NULL, NULL, NULL, NULL, 460)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (303, N'6ACA4B2387DB44EDB76008F8EE8878B4', N'TORSEMIDE TAB 20 MG', NULL, NULL, NULL, NULL, NULL, NULL, 605)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (304, N'6B5732A29F464F51A4A754CE751CC84F', N'OFOLXACIN', NULL, NULL, NULL, NULL, NULL, NULL, 124)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (305, N'6B9BAC0838594A9EB8C48442BFC3F629', N'WHITFIELD ONT', NULL, NULL, NULL, NULL, NULL, NULL, 405)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (306, N'6C76C5E1E611471AAC2D98843EE5610D', N'N-ACETYLCYSTEINE,VITAMINS& MINERALSTAB', NULL, NULL, NULL, NULL, NULL, NULL, 549)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (307, N'6C9C6BA7AF4349AEA2E0BEAD5FFAC7DA', N'NICORANADIL 5 MG', NULL, NULL, NULL, NULL, NULL, NULL, 532)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (308, N'6DBE8ED0CCE0447EBDDABCF3B173908D', N'NORFLOXAQCINE+ TINIDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 354)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (309, N'6DDF5C45CCD045D18B318FF941B427F6', N'PARACETAMOL 125MG SUS', NULL, NULL, NULL, NULL, NULL, NULL, 214)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (310, N'6E301EE0DAE540399903963966D3BB1B', N'ETAMSYLATE 500MG', NULL, NULL, NULL, NULL, NULL, NULL, 511)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (311, N'6E39F783EAC64DCA987BC5CD5C401F06', N'DOXYLAMINE SUCCINATE,PYRIDOXINE,FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 654)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (312, N'6E5B2518A6BF4CEBBA3DDB16BEDE372F', N'SERRATIOPEPTIDASE', NULL, NULL, NULL, NULL, NULL, NULL, 96)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (313, N'6F1EEDC3184845E5B034A09742D27917', N'OXITOCIN', NULL, NULL, NULL, NULL, NULL, NULL, 208)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (314, N'6F9CE30329FD481296EE26C30DD723CA', N'AMOXYCILLIN TRIHYDRATE 500 CAP', NULL, NULL, NULL, NULL, NULL, NULL, 41)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (315, N'7009560D0B7549CD83D77EBFD1030794', N'OLMESARTAN MEDOXOMIL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 725)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (316, N'70BB49102E734B3D8F8A43E33159DAB0', N'AMPICILLIN AND DICLOXACILLIN CAP', NULL, NULL, NULL, NULL, NULL, NULL, 340)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (317, N'718A1217018740F1BAAFDB242E334923', N'ONDANSETRON HYDROCHLORIDE', NULL, NULL, NULL, NULL, NULL, NULL, 261)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (318, N'718D3EBB23164EA990899B8C20EFF5D9', N'IRON TONIC WITH B12 FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 615)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (319, N'72606F4AC3344838A3C593711870A1F9', N'ERYTHROMYCIN STEARATE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 341)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (320, N'72C85913A11E41288E0EB20A61318D44', N'ACECLOFENAC+RABEPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 144)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (321, N'73549BFCE982473DB32481D0422EEFB6', N'VITAMIN C AND ZINC CHEWABLE TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 440)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (322, N'7364ECD1AB9E4C98B3088BBB62817E6E', N'PROLONGED-RELEASE DICLOFENAC', NULL, NULL, NULL, NULL, NULL, NULL, 78)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (323, N'737F8CB2E3CB497E8E4FC39C8A744B3F', N'SUCRALFATE,OXETACAINE SYP', NULL, NULL, NULL, NULL, NULL, NULL, 742)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (324, N'739DA358A6CC49A69FDB97C2113B146E', N'AMPICILLIN&DICLOXACLLIN DISP TAB', NULL, NULL, NULL, NULL, NULL, NULL, 51)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (325, N'7493A9DD47D54AD28AB85AC647C97F9A', N'PANTAPRAZOLE SR AND LEVOSULPIRIDE CAP', NULL, NULL, NULL, NULL, NULL, NULL, 756)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (326, N'74F9F49C056C4DB2B3487E37599FB4C4', N'PRAZOSIN HCL SR TAB', NULL, NULL, NULL, NULL, NULL, NULL, 730)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (327, N'75836210432F47E288E54C2836BAE639', N'PARACETAMOL+PHENYLEPHRINE+CETIRIZINE', NULL, NULL, NULL, NULL, NULL, NULL, 92)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (328, N'7651EC3F94254C64A1A691DA3D6C14E4', N'NULTIVITAMINS,MULTI MINERAL,ANTIOXIDANT', NULL, NULL, NULL, NULL, NULL, NULL, 693)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (329, N'7669A464CE524190AD8A8299CCA1E33C', N'CEFIXIME&POTASSIUM CLAVULANATE', NULL, NULL, NULL, NULL, NULL, NULL, 281)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (330, N'76754977206F4C9CB5D4484B1D393F0C', N'PREGNANCY TEST', NULL, NULL, NULL, NULL, NULL, NULL, 90)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (331, N'76BBE48FFFEF46F7B0B9C0BC3BF65A9F', N'MOXCIFLOXACIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 551)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (332, N'76CA5E9EDC0C491084714C59B1DF6724', N'ACLOVEER', NULL, NULL, NULL, NULL, NULL, NULL, 633)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (333, N'76EC769D8B984E1BB437C4E9B0AC1499', N'SUGER FREE COUGH SYP', NULL, NULL, NULL, NULL, NULL, NULL, 624)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (334, N'7721E69BE7BE44E5B354A8A709B7DBE3', N'ACECLOFENAC100+THIOCOLCHICOSIDE4', NULL, NULL, NULL, NULL, NULL, NULL, 143)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (335, N'77DF681C74B3475395B42BFAFC48B9C4', N'ALPRAZOLAM 0.5', NULL, NULL, NULL, NULL, NULL, NULL, 567)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (336, N'77FA26109EE74C0EB647D38EE6E10ED0', N'TELMISARTAN& HCL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 784)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (337, N'781650095D2D47F08DBDDE0CC77D31D4', N'CEFPODOXIME PROXETIL DT', NULL, NULL, NULL, NULL, NULL, NULL, 295)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (338, N'7839C0BF19094B589A8F278DB0F0A0EB', N'ROSUVASTATIN AND FENOFIBRATE', NULL, NULL, NULL, NULL, NULL, NULL, 728)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (339, N'784393EF3B1C40B5B6B6E908A0E82145', N'ATORVASATATIN TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 486)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (340, N'78B71EE565B047519A768BEC7EB87C0F', N'CIPROFLOXACIN', NULL, NULL, NULL, NULL, NULL, NULL, 255)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (341, N'799E4F95905A4E52A5ECB27A44ECB7D2', N'RABEPRAZOLE SODIUM ,LEVOSULPIRIDE CAP', NULL, NULL, NULL, NULL, NULL, NULL, 738)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (342, N'79B165EC7CB14C71879E870AB870BA7D', N'ATORVASTATIN TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 463)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (343, N'7A0BF19DCD884AC9A7FD7C35D8F8D6FF', N'CALCIUM,VITAMIN D3,MINERALS TAB', NULL, NULL, NULL, NULL, NULL, NULL, 767)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (344, N'7A2B396A1CD54BC0B2C9353B341F07C1', N'TROBRAMYCIN DROP', NULL, NULL, NULL, NULL, NULL, NULL, 343)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (345, N'7A46708FF7BA474DA6AF003F7933F618', N'OMEPRAZOLE&DOMPERIDONE CAP.', NULL, NULL, NULL, NULL, NULL, NULL, 423)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (346, N'7A7765A740DE4A688D68B4D48E293A2F', N'ATROVASTATIN & FENOFIBRATE', NULL, NULL, NULL, NULL, NULL, NULL, 697)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (347, N'7B2074FC742F4F548FC062777CEA804D', N'PIRACETAM TAB.800MG', NULL, NULL, NULL, NULL, NULL, NULL, 762)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (348, N'7B25FCF973944CBC9ACF51F5497506BA', N'ATORVASTATIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 333)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (349, N'7B62E2E43962477082780E08F150572C', N'LEVOSULPIRIDE SR TAB', NULL, NULL, NULL, NULL, NULL, NULL, 656)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (350, N'7C0126A4678A4084A3138F962931389A', N'VITAMIN K2 CAL CALCITRIOL BORON ZINC', NULL, NULL, NULL, NULL, NULL, NULL, 305)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (351, N'7C09E58643894EE2B4215E980C11230A', N'CLARITHROMYCIN TOPICAL GEL', NULL, NULL, NULL, NULL, NULL, NULL, 622)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (352, N'7C91228D545F482883A957507375E531', N'GENTAMICIN', NULL, NULL, NULL, NULL, NULL, NULL, 184)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (353, N'7CD0E1ABE5D34E4EA993742858B62A75', N'SURGICAL CLOTH MASK', NULL, NULL, NULL, NULL, NULL, NULL, 427)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (354, N'7DA2690A299642FCB9A6BBEE5E6D877D', N'GLUC', NULL, NULL, NULL, NULL, NULL, NULL, 505)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (355, N'7DE61BBA9A3149E7B0F43C3EBE9B2C2B', N'TELMISARTAN 80 MG TAB', NULL, NULL, NULL, NULL, NULL, NULL, 495)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (356, N'7E09CA7594CF43479CE68E4DE8EA3B8C', N'DICLOFENAC SODIUM+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 79)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (357, N'7E291763E38B45D68134AD9C27B038A1', N'GENTAMICIN BETAMETHASONE BENZALKONIUM CHLORIDE DRO', NULL, NULL, NULL, NULL, NULL, NULL, 345)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (358, N'7E91F67E64CB460CAC2BA33446A657C5', N'DOXOFYLINE TAB 400MG', NULL, NULL, NULL, NULL, NULL, NULL, 631)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (359, N'7EAD4C998A6C427D8A768D357FB53A9B', N'TRAMADOL HCL, DICLOFENAC &CHLORZOXAZONE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 711)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (360, N'7ED8CCEFA03847749FF6EBE6B7082ED1', N'MULTIVITAMIN+MINERALS', NULL, NULL, NULL, NULL, NULL, NULL, 89)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (361, N'7F14F95E3F2F46B196D7A530A7CD6B90', N'HYDROCORTISONE SODIUM SUCCINATE INJ', NULL, NULL, NULL, NULL, NULL, NULL, 458)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (362, N'7F3B23FCF9D04CA29CCBADFF34B89653', N'COUGH SYP', NULL, NULL, NULL, NULL, NULL, NULL, 623)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (363, N'7F8537C7B64642AE91516D8BA1DE92A5', N'CYPRO 4 MG', NULL, NULL, NULL, NULL, NULL, NULL, 771)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (364, N'7FD95D4FC61D4AE8BAB4D581591F02C1', N'ATORVASATATIN   TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 462)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (365, N'8105C8A39C80425C8D0C881A3F4B6FF3', N'PANTOPRAZOLE INJ', NULL, NULL, NULL, NULL, NULL, NULL, 154)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (366, N'811681455FFB43FBBB23EA2BB74EAF7D', N'DAPOXETINE 30', NULL, NULL, NULL, NULL, NULL, NULL, 755)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (367, N'811A85D7F169466BBB67AE497A37055D', N'TAMSILOSIN HCL AND DUTASTERIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 643)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (368, N'81D25AF06A67454BA9C8A672D637F5DB', N'RABEPRAZOLE AND DOMPERIDON SR CAP', NULL, NULL, NULL, NULL, NULL, NULL, 332)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (369, N'820DB1BACEC444A79DCF1AC5F765ACE8', N'VIT.B1,B6,B12&NIACINAMIDE WITH CALCIUM', NULL, NULL, NULL, NULL, NULL, NULL, 548)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (370, N'821A767383D344E29B634953C6E0D307', N'COAL TAR&SALICYLIC ACID SCALP SOLUTION', NULL, NULL, NULL, NULL, NULL, NULL, 610)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (371, N'8251C83CA09D4E9EBDC57495434C9ED9', N'AZITHROMYCIN AND AMBROXOL', NULL, NULL, NULL, NULL, NULL, NULL, 775)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (372, N'82668432894440B9AF79FFE7C14941D1', N'CHLORPHENIRAMINE+MALEATE EXP', NULL, NULL, NULL, NULL, NULL, NULL, 123)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (373, N'82CB0F497B1D4347AD2A1715041520ED', N'BANDID', NULL, NULL, NULL, NULL, NULL, NULL, 121)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (374, N'82F01E693DDE4663B6BE683AAAA9A4FC', N'ORAL REHYDRATION', NULL, NULL, NULL, NULL, NULL, NULL, 70)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (375, N'8306056B4A9444B1A189CB4251D1355F', N'CRYSTEL VIOLET B P 0.5', NULL, NULL, NULL, NULL, NULL, NULL, 413)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (376, N'832FD8F5AD544F95A0389639AAB45B29', N'CEFIXIM ANDLINEZOLID TAB', NULL, NULL, NULL, NULL, NULL, NULL, 604)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (377, N'837798EEF0F94DED9E09CAA17DE84C71', N'DEXAMETHASONE', NULL, NULL, NULL, NULL, NULL, NULL, 432)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (378, N'83A10D1B68814FB0903119F0E15DDE41', N'SIDENAFIL CITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 83)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (379, N'83B119F005704D6C8D15710E54CBEB5B', N'FERROUS ASCORBATE,FOLIC ACID,ZINC TAB', NULL, NULL, NULL, NULL, NULL, NULL, 655)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (380, N'83CB6CFF0986471C96CDD4200FD3B3B6', N'B-COMPLEX', NULL, NULL, NULL, NULL, NULL, NULL, 550)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (381, N'84164D12601F415092E0B0471FC0EDFA', N'TRANEXMIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 586)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (382, N'859804AA6C1549CE8738B91F09F0B915', N'SODIUM FUSIDATE', NULL, NULL, NULL, NULL, NULL, NULL, 249)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (383, N'85E9ED4D98914772A572CD14080E5AB8', N'CEFIXIME ORAL SYP', NULL, NULL, NULL, NULL, NULL, NULL, 745)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (384, N'86C80016F04C4CF89964E0CBBAF5FD79', N'CEFPODOXIME&POTASSIUM CLAV. TAB', NULL, NULL, NULL, NULL, NULL, NULL, 465)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (385, N'86D9C133BB454CEFAC08925D44B6A029', N'PROGESTERONE TABLETS 100MG', NULL, NULL, NULL, NULL, NULL, NULL, 445)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (386, N'87FB20180CAC4572BF7141820ABF080B', N'NICORANDIL 10MG', NULL, NULL, NULL, NULL, NULL, NULL, 533)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (387, N'89A18ED8155746CB8174937B37933CA0', N'PIOGLITAZONE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 280)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (388, N'89AF981311C04EBAA7A5A7AC4C820936', N'PARADICHOROBENZENE+BENZOCAINE+TURPENTINE', NULL, NULL, NULL, NULL, NULL, NULL, 42)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (389, N'89F47A9BD80342F78E170F302E1F3DCE', N'RAMIPRIL 5 MG', NULL, NULL, NULL, NULL, NULL, NULL, 424)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (390, N'8BBBD6DC35114083ADC840FFDC4C379E', N'ANTIBACTERIAL,ANTIFUNGAL,ANTIINFLAMMATORY', NULL, NULL, NULL, NULL, NULL, NULL, 259)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (391, N'8C96EE31E9E7458BAC24B33D64FEA692', N'ACICLOVIR', NULL, NULL, NULL, NULL, NULL, NULL, 248)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (392, N'8CAA77A6683D45CCA03545EC4E6245C2', N'ALPRAZOLAM', NULL, NULL, NULL, NULL, NULL, NULL, 148)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (393, N'8CE79AA4CEDE452AAC5BD454BC64A837', N'DISODIUM HYDROGEN CITRATE SYP', NULL, NULL, NULL, NULL, NULL, NULL, 760)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (394, N'8D3E4281F96A4F00A910105415383A8F', N'DILTIAZEM 90MG HCL SR', NULL, NULL, NULL, NULL, NULL, NULL, 531)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (395, N'8D4DC686365C499084BE02F8B6BEB8FF', N'RABEPRAZOLE+DOMPERIDONE 20+30', NULL, NULL, NULL, NULL, NULL, NULL, 77)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (396, N'8D4DCF8508F448E7BE4FB80A5EB16CEE', N'BETAHISTINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 289)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (397, N'8D7593B987E244F1932D71505E647F96', N'CETIRIZINE HYDROCHLORIDE', NULL, NULL, NULL, NULL, NULL, NULL, 20)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (398, N'8DAECFC6C37C4A068C516E02D3411D52', N'QUICK RELIF ORTHO OIL', NULL, NULL, NULL, NULL, NULL, NULL, 681)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (399, N'8DCF8983CBDA4777B50DB348E1B07F12', N'AZITHROMYCIN-250', NULL, NULL, NULL, NULL, NULL, NULL, 38)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (400, N'8E536EB6430A4D2D9C9BE14877215505', N'PHENYLEPHRINE HCL PARACETAMOL ,CETRIZINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 688)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (401, N'8EDECE1B67804FB088AE5FD6FD099A35', N'ANTICOLD SYP', NULL, NULL, NULL, NULL, NULL, NULL, 547)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (402, N'8FAA7D19A62B43E0B6BAAD7232899507', N'PAINKILLER OINT.', NULL, NULL, NULL, NULL, NULL, NULL, 523)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (403, N'8FB4BC90CBB741F5B7FBF440DEAF5E85', N'ASHWAGANDHA, SHILAJIT, AND KESHAR.', NULL, NULL, NULL, NULL, NULL, NULL, 414)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (404, N'8FE7A36E30D0430C960121D16A525CDD', N'METOPROLOL SUCCINATE EXTENDED RELEASE', NULL, NULL, NULL, NULL, NULL, NULL, 266)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (405, N'907638D9378F404AB1FD4EDC54FF481E', N'BETAMETTHASONE SODIUM PHOSPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 236)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (406, N'9115D4710BAD4D618035ED59F0352E25', N'ONDANSETRON ING', NULL, NULL, NULL, NULL, NULL, NULL, 696)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (407, N'9138B9FDD0E246C1A7AB78C6BF5AFDB4', N'LEVOCETIRIZINE', NULL, NULL, NULL, NULL, NULL, NULL, 62)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (408, N'9169A0A6717941DB805650B39F6588C3', N'AMIKACIN', NULL, NULL, NULL, NULL, NULL, NULL, 397)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (409, N'916C252134D2421786988ADC12B9F37E', N'DISPERSIBLE AMOXICILLIN AND POTA CLAV', NULL, NULL, NULL, NULL, NULL, NULL, 777)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (410, N'9220B7EC9B7044A78BD56C51870F3C36', N'ACECLOFENAC TAB', NULL, NULL, NULL, NULL, NULL, NULL, 729)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (411, N'923C141B3F7A4157828B2FC6522B6782', N'GLICLAZIDE & METFORMIN', NULL, NULL, NULL, NULL, NULL, NULL, 327)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (412, N'9273870CBF244EC5819C877D0071B10E', N'FEXOFENADINE HYDROCHLORIDE 120', NULL, NULL, NULL, NULL, NULL, NULL, 581)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (413, N'92C9614891B34E86B15FF3C8E0F39BEE', N'ETHAMSYLATE', NULL, NULL, NULL, NULL, NULL, NULL, 385)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (414, N'9347B721A5DA400D9E5ECB087BB0B2BD', N'CEFIXIME DISPERSIBLE TAB 100 MG', NULL, NULL, NULL, NULL, NULL, NULL, 732)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (415, N'93CD417DF0334976B19C7DD3D7889493', N'CALCIUM CALCITROL AND VITK2-7', NULL, NULL, NULL, NULL, NULL, NULL, 483)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (416, N'94532819AEDA414A90319ED06754998A', N'CLINDAMYCIN CAP', NULL, NULL, NULL, NULL, NULL, NULL, 686)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (417, N'94BA8D2505FA468BBAA8837709360709', N'TELMISARTAN & HCL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 783)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (418, N'9627378AAF714F7EBF5A3E7E96BF540E', N'LATEX EXAM GLOVES', NULL, NULL, NULL, NULL, NULL, NULL, 554)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (419, N'96676B5FC11948D6AB1ECDE4994B418A', N'DIPHENHYDRAMINE HYDROCHLORIDE', NULL, NULL, NULL, NULL, NULL, NULL, 220)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (420, N'96710B4755C54334AB00B506E5483F7F', N'LACTITOL MONOHYDRATE & ISPAGHULA HUSK GRA', NULL, NULL, NULL, NULL, NULL, NULL, 562)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (421, N'96860769D7234B87A013CE3C5459598E', N'OFLOXACIN&OMIDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 73)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (422, N'96924A7AB8C8460DB999338F9AF8587F', N'CLOTRIMAZOLE V GEL', NULL, NULL, NULL, NULL, NULL, NULL, 193)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (423, N'9707E436F6744F1DA8C5746812DB9D7D', N'DICLOFENAC SODIUM', NULL, NULL, NULL, NULL, NULL, NULL, 169)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (424, N'97158D4CCCBE45A396E1772B5F4D0A4F', N'CILNIDIPINE TAB 10MG', NULL, NULL, NULL, NULL, NULL, NULL, 504)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (425, N'98734903CD0B4F57BCF22A526FDC6E78', N'ALDULT DAIPER', NULL, NULL, NULL, NULL, NULL, NULL, 430)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (426, N'98B84952FA3644588D2CC1B1C5A5CC23', N'GLIMIPIRIDE &METFORMING HCLSR TAB', NULL, NULL, NULL, NULL, NULL, NULL, 704)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (427, N'98C2478312274B878B39FF7CF1420ABC', N'ROSUVASTIN', NULL, NULL, NULL, NULL, NULL, NULL, 268)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (428, N'994E0ABF5AEC4AED93962945FD2906D4', N'B-COMPLEX FORTE+VITAMIN C', NULL, NULL, NULL, NULL, NULL, NULL, 132)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (429, N'994FAC2F5CFB45C28B19AE18919FD14E', N'LYOPHILIZED SACCHAROMYCES BOULARDII LACTIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 657)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (430, N'996A5E9989B444BBB1E3D2D21D43BA8D', N'FAIRNESS CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 584)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (431, N'99940A2B663D446B81B5FC8114258C93', N'CHOLINE SALICYLATE+LIGNOCAINE HYDROCHLORIDE GEL', NULL, NULL, NULL, NULL, NULL, NULL, 183)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (432, N'99CFF1F313D1453DAE1D4D92520C56A0', N'AYURVEDIC COUGH SYP', NULL, NULL, NULL, NULL, NULL, NULL, 378)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (433, N'9A4D2386EC4D40BB8D1DD822E9B57F7C', N'CLOPIDOGREL', NULL, NULL, NULL, NULL, NULL, NULL, 367)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (434, N'9A6F08F27EB240DFA7DDD7EF08B672C6', N'BIOTIN AND FOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 544)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (435, N'9B0FE7DDC6CB4D60889CF947A9D20257', N'NIMESULIDE & SERRATIOPEPTIDASE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 769)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (436, N'9B529855164B4B899FE0F240574CDC25', N'MONTELUKASRT & FEXOFENADINE HYDROCHLORIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 565)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (437, N'9BD0DFA7166E4ADB90567673284E2ECB', N'NIMESULIDE+DICYCLOMINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 110)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (438, N'9BFA4AA8F93A43ED9ACF4AF588A91D38', N'STRAWBERRY', NULL, NULL, NULL, NULL, NULL, NULL, 331)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (439, N'9C3092A152E640E5ADDDB5F22FCADE81', N'DICLOFENAC+DIEPYLAMINE', NULL, NULL, NULL, NULL, NULL, NULL, 125)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (440, N'9C90FFF97F7049D18AB57486FA622193', N'NORETHINDONE ACETATE', NULL, NULL, NULL, NULL, NULL, NULL, 61)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (441, N'9CA1BA1D4FAC4DB1A57D9E217A46145C', N'ORLISTAT CAP', NULL, NULL, NULL, NULL, NULL, NULL, 383)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (442, N'9CBDEB871E174E51BBBAB15B3BEAC37E', N'GLIMEPIRIDE & METFORMIN HCL', NULL, NULL, NULL, NULL, NULL, NULL, 525)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (443, N'9DE549C99B764690B862F604251A23BD', N'B-COMPLEX WITH L-LYSINE SYP DAITERY SUPP.', NULL, NULL, NULL, NULL, NULL, NULL, 464)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (444, N'9DE8A4F2983C43B486A2F8E409549CF7', N'COUGH SYRUP', NULL, NULL, NULL, NULL, NULL, NULL, 139)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (445, N'9DE997BF9CBA4D84A30D3AC7F5FEF253', N'METHYLPREDINISOLONE', NULL, NULL, NULL, NULL, NULL, NULL, 653)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (446, N'9E1EBD2CB63248D7A709C34DF833567B', N'FOLIC ACID, IRON, ZINC', NULL, NULL, NULL, NULL, NULL, NULL, 481)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (447, N'9EE5AF836EC641A3AD384F0AFEFD1E54', N'COFFEE CONDOM', NULL, NULL, NULL, NULL, NULL, NULL, 516)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (448, N'9F6207F2217A406E92FC85D0AD9A77DF', N'DEFLAZACORT 6MG', NULL, NULL, NULL, NULL, NULL, NULL, 417)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (449, N'A0A6493288714BA087B6CA67A9F329BA', N'NORFLOFLOXACIN,TINIDAZOLE&SIMETHICONE', NULL, NULL, NULL, NULL, NULL, NULL, 312)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (450, N'A0DE4899FC4347E482D376858ED2C28B', N'CO- TRIMAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 275)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (451, N'A0F21194E4044F8E8E7662774E3F3CD0', N'CHOLE', NULL, NULL, NULL, NULL, NULL, NULL, 186)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (452, N'A1AD94EC1EE24147B30CBA843713A1E7', N'CIPROFOXALIN DROP', NULL, NULL, NULL, NULL, NULL, NULL, 342)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (453, N'A1E7FF9DEA994FF6B0185074ADE6977F', N'DICLOFENAC SODIUM GASTRO RESISTANT TAB', NULL, NULL, NULL, NULL, NULL, NULL, 337)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (454, N'A1EFDFFA8DF34A5885E3014F13BAE15C', N'COUGH RELIEF', NULL, NULL, NULL, NULL, NULL, NULL, 138)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (455, N'A20D369B2EC544D3AD7320AA0D431D96', N'VITAMIN E WHEAT GERM OIL AND OMEGA3 FATTY ACID CAP', NULL, NULL, NULL, NULL, NULL, NULL, 338)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (456, N'A2955EF42C6D4B599AF685B2E679F440', N'HAEMATINIC,ZIC,VITAMINS', NULL, NULL, NULL, NULL, NULL, NULL, 598)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (457, N'A2B2F735DEAD465E9C04B48DA4F6CC69', N'CHOLECALCIFEROL GRANULES', NULL, NULL, NULL, NULL, NULL, NULL, 187)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (458, N'A335AF87E5D9469CB6E26600C39D7669', N'GAMMA LINOLENIC ACID,MULTIVITAMIN,MULTIMINIRAL,', NULL, NULL, NULL, NULL, NULL, NULL, 571)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (459, N'A3AAEEB6E62140E48F963B9C15323511', N'MULTIVITAMIN SYRUP', NULL, NULL, NULL, NULL, NULL, NULL, 344)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (460, N'A41B4D60E03142A49A8A7441352F9D0B', N'ACECOLFENC & PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 502)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (461, N'A42318AD597B42ECB01FDD0E22E5C035', N'FERRIC AMONIUM CITYRAT', NULL, NULL, NULL, NULL, NULL, NULL, 191)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (462, N'A4339CDCBD6F40C2B76423997ADD82F8', N'PRICKLY HEAT POWDER', NULL, NULL, NULL, NULL, NULL, NULL, 168)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (463, N'A43609CB6E984B68B99E3EFB9064E273', N'OLMESARTAN MEDOXOMIL', NULL, NULL, NULL, NULL, NULL, NULL, 269)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (464, N'A4769BB5D5814A07B722F938A3A58BB2', N'FRADIOMYCIN', NULL, NULL, NULL, NULL, NULL, NULL, 246)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (465, N'A54D0ABE08324BB9B9140694B4CB8C01', N'ERYTHROCIN', NULL, NULL, NULL, NULL, NULL, NULL, 311)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (466, N'A571A783D4B54DAFBDFF1DDCFD1EC4FF', N'PAIN BALM', NULL, NULL, NULL, NULL, NULL, NULL, 475)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (467, N'A68AB33EC7574E4A87E985329D9F601D', N'RACECADOTRIL SACHET', NULL, NULL, NULL, NULL, NULL, NULL, 602)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (468, N'A6A9F549E77C4389934489465ED7A59E', N'PARACETAMOL 250MG SUS', NULL, NULL, NULL, NULL, NULL, NULL, 215)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (469, N'A6B42D054F3B415A872838AEB4B3D666', N'LEPERAMIDE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 80)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (470, N'A6F470BD5D3C41E8B9F2CFB3326D3337', N'DIATARY SUPPLIMENT', NULL, NULL, NULL, NULL, NULL, NULL, 663)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (471, N'A79AB2954E814CECB9F090FE8D48B91E', N'FOOT CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 56)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (472, N'A7BBAA2BE463479B885B3AD6E162FDEF', N'TRYPSIN-CHYMOTRYPSIN', NULL, NULL, NULL, NULL, NULL, NULL, 197)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (473, N'A85A6E5658B44D8FBCF8DAB539ECA910', N'FERRIC AMMONIUM CITRATE+FOLIC ACID+B12', NULL, NULL, NULL, NULL, NULL, NULL, 192)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (474, N'A86AB1A4A0A7495BB8A24EB9766F10EF', N'DIACEREIN AND GLUCOSAMINE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 487)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (475, N'A87534C9972C4881AE182ACE00C809C1', N'NATURAL FIBRE OF ISABGOL HUSK', NULL, NULL, NULL, NULL, NULL, NULL, 202)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (476, N'A8FE6E79463E4FE5928CA96570D4EEBE', N'AZITHROMYCIN 500', NULL, NULL, NULL, NULL, NULL, NULL, 388)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (477, N'A900058ECF15444C997E7EA8F3925544', N'SERTRALINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 731)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (478, N'A9EC85F09D8E4D16BFAE0B8B3770FC06', N'CLARITHROMYCIN 500', NULL, NULL, NULL, NULL, NULL, NULL, 390)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (479, N'A9FBD66ADA6044668CBFC3FBB798062E', N'ROX', NULL, NULL, NULL, NULL, NULL, NULL, 676)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (480, N'AAEC6B39F20849E4BAD1BEA1D92BAEFB', N'ANALGESIC,ANTESTHETIC,ANTISEPTIC PAIN-RELIVING GEL', NULL, NULL, NULL, NULL, NULL, NULL, 254)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (481, N'AAFBB195203549A492041A9DBC905081', N'BUTTER SCOTCH CONDOM', NULL, NULL, NULL, NULL, NULL, NULL, 515)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (482, N'AB200B9F9FB04295988353B5929572E0', N'PIROXICAM GEL', NULL, NULL, NULL, NULL, NULL, NULL, 126)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (483, N'AB2D649AA309485D93D47AC46A36CB75', N'NIMESULIDE MOUTH DISOLVING TAB', NULL, NULL, NULL, NULL, NULL, NULL, 710)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (484, N'AB4C036C87874D518E9F117479792F1D', N'CEFPODOXIME 200', NULL, NULL, NULL, NULL, NULL, NULL, 720)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (485, N'AB9C96E75DD245DB87FCCF1DBD91D961', N'CODEINE PHOSPHATE+CHLORPHENIRAMINE', NULL, NULL, NULL, NULL, NULL, NULL, 159)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (486, N'ABC377E280A84CEA8D956351B535D083', N'PERMETHRIN CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 748)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (487, N'AC3D81BB641A47C9A228A79241EEF516', N'ITOPRIDE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 365)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (488, N'AC76221F70684A7F939A03E705717B04', N'RAMIPRIL 2.5', NULL, NULL, NULL, NULL, NULL, NULL, 529)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (489, N'AD3E51376E104409AF2D033B2F29543C', N'CLONAZEPAM MD O.5 TAB', NULL, NULL, NULL, NULL, NULL, NULL, 606)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (490, N'AD7333A78D6A4D38A8B18851C0D3BCA1', N'CARBOXMETHYLCELLULOSE SODIUM DROP', NULL, NULL, NULL, NULL, NULL, NULL, 749)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (491, N'AE81054EACC84AFC803AA93AE8A3DAAF', N'CIPROFLOXACIN 500', NULL, NULL, NULL, NULL, NULL, NULL, 136)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (492, N'AEC0187EDCBA4C69AE6FF6DECCFFBB5D', N'FERRIC AMMONIUM CITRATE+FOLIC ACID+CYANOCOBALAMIN', NULL, NULL, NULL, NULL, NULL, NULL, 14)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (493, N'AF0518E3453745F2A0B0BCEC9B1B0DE4', N'NIMESULIDE+PARACETAMOL+CHLORZOXAZONE', NULL, NULL, NULL, NULL, NULL, NULL, 60)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (494, N'AF0E9BC48BFC42C989E36F9D37A7A54A', N'DICLOFENAC', NULL, NULL, NULL, NULL, NULL, NULL, 212)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (495, N'AF3F4224511D4657B2183AE7D610AF1D', N'DROTAVERINE AND MEFENAMIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 600)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (496, N'AF59B5B3D1C04EC3A50E411E69D29E19', N'ROSUVASTATIN CALCIUM&FENOFIBRATE', NULL, NULL, NULL, NULL, NULL, NULL, 283)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (497, N'AF6526AE9D0C45319E5A7B3D92F56C3B', N'AMLODIPINE 5', NULL, NULL, NULL, NULL, NULL, NULL, 471)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (498, N'AF8DF17C4D6C40528961AD77C8E4E16C', N'ACECLOFENAC+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 69)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (499, N'AF97B3E6151947038429A7E4A0922184', N'GLIBECLAMIDE AND METFORMIN HCL. TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 461)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (500, N'AFCF18FF0175450C801A7B13E5379F80', N'AMOXICILLIN ORAL SUSP.', NULL, NULL, NULL, NULL, NULL, NULL, 552)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (501, N'AFE3CFB325B049029862DB38EFF85EEE', N'NORFLOXACIN&TINIDAZOLE+BETACYCLODEXTRIN', NULL, NULL, NULL, NULL, NULL, NULL, 290)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (502, N'B012B0B0435B421697D2B8E5718AAF51', N'MONTELUKAST', NULL, NULL, NULL, NULL, NULL, NULL, 722)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (503, N'B061120A856C498EBE8334B1B7F544C0', N'CALCIUM WITH VIT D3', NULL, NULL, NULL, NULL, NULL, NULL, 435)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (504, N'B0677819A85B414A8EC020DF7C1BF5A2', N'FLUCIN', NULL, NULL, NULL, NULL, NULL, NULL, 172)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (505, N'B0AC78CC9CB54708A85F2B29CDB2103C', N'ANTI-D IMMUNOGLOBIN INJ', NULL, NULL, NULL, NULL, NULL, NULL, 591)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (506, N'B0F83C01FDA5401DB6A86F156A8BEA3B', N'METFORMIN HCL 500MG', NULL, NULL, NULL, NULL, NULL, NULL, 787)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (507, N'B1113028F6824B81B4022CBF76DA7453', N'MASSAGE OIL', NULL, NULL, NULL, NULL, NULL, NULL, 570)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (508, N'B11925F8B3554A948928ECDE0A2248A0', N'DIPHENHYDRAMINE HYDROCHL,ORID', NULL, NULL, NULL, NULL, NULL, NULL, 219)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (509, N'B11E7384844341A485B83AB65D98056A', N'VITAMIN B-COMPLEX B12', NULL, NULL, NULL, NULL, NULL, NULL, 135)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (510, N'B19577A57AEC428DA7EAB52CD1DFA8AE', N'ISOSORBIDE MONONITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 278)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (511, N'B1DC4CB51EDE44D18AE4E5604A122981', N'PET SOAP', NULL, NULL, NULL, NULL, NULL, NULL, 579)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (512, N'B25DC9F08EA14A63BE4C1849D810245B', N'NUTRITIONAL SUPPLIMENT', NULL, NULL, NULL, NULL, NULL, NULL, 472)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (513, N'B275568A6F1749D091A0A5B285A4D9E1', N'TRAMADOL HCL+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 262)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (514, N'B293180519A643979AAD1E50BBAEB906', N'RAMIPRIL', NULL, NULL, NULL, NULL, NULL, NULL, 288)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (515, N'B33CA9E407564273885C5AD3C349AEFD', N'B COMPLEX INJ', NULL, NULL, NULL, NULL, NULL, NULL, 234)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (516, N'B3774E5994BE4C2C9FF582DF6BD431A6', N'ALBENDAZOLE ORAL SUSP', NULL, NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (517, N'B4215D26A9C04E3AA125FA32695A52B2', N'GLIMEPRIDE,PIOGLITAZONE HYDROCHLORIDE,METFORMIN SR', NULL, NULL, NULL, NULL, NULL, NULL, 451)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (518, N'B42E51D394494CD780E338184756DCC5', N'AZITHROMYCIN', NULL, NULL, NULL, NULL, NULL, NULL, 149)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (519, N'B44CD0B6CC834BFD9D95670D2034A4B3', N'DICYCLOMINE HCL+MEFENAMIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 538)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (520, N'B457EE4329B043418AD0FCC2FD51249F', N'ENTRIC COATED ESOMEPRAZOLE & DOMPERIDONE SR', NULL, NULL, NULL, NULL, NULL, NULL, 561)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (521, N'B4B0DABAF8924077A64DC2BCD75323E9', N'CEFPODOXIME SYP', NULL, NULL, NULL, NULL, NULL, NULL, 677)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (522, N'B5300A81436F4DA19F6D6E44878651A5', N'LORNOXICAM+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 30)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (523, N'B5902E8882D14A108BB1393C3106EFC3', N'CALCITRIOL,CALCIUM CARBONATE,VITAMIN K2 7', NULL, NULL, NULL, NULL, NULL, NULL, 652)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (524, N'B5AB4B47AA044F4BAFC7F15642BF6B33', N'CITICOLINE AND PIRACETAM', NULL, NULL, NULL, NULL, NULL, NULL, 468)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (525, N'B5C83F04A5674F3884E35EDDC3D5B994', N'VALSARTAN 80 TAB', NULL, NULL, NULL, NULL, NULL, NULL, 727)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (526, N'B72179225F7C40F2975F19811A4DACD8', N'LIGNOCAINE HCL GEL', NULL, NULL, NULL, NULL, NULL, NULL, 415)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (527, N'B79F4B37EBA6471692A373AAD3EB6A6F', N'AZITHROMYCIN 500MG', NULL, NULL, NULL, NULL, NULL, NULL, 416)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (528, N'B7ACFA91698D48AB93796B4FE7310AF5', N'ACECLOFENAC100', NULL, NULL, NULL, NULL, NULL, NULL, 146)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (529, N'B7EE538162F74EF3A494B4DB2505D949', N'CEPHALEXIN', NULL, NULL, NULL, NULL, NULL, NULL, 91)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (530, N'B875B754449E48B5BE69D82242A60A5B', N'SODIUM VALPROATE AND VALPROIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 672)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (531, N'B90FF2FE33FA4258BCBCC3AF4CABB36C', N'PARACETAMOL,ACECLOFENAC,SERRATIOPEPTIDASE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 442)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (532, N'B942E16E86CE4CD292DA29E9D809AAAA', N'FERROUS ASCORBATE+FOLIC ACID+METHYLCOBALAMIN', NULL, NULL, NULL, NULL, NULL, NULL, 512)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (533, N'B956D1EB57C64F51B2333039DC447D2C', N'OFLOXACIN+ORNIDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 161)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (534, N'B9DEA64833884AAF86E52F907AFC12D1', N'CRACK CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 476)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (535, N'BA30568A666B4FEAB36369A496626ABF', N'ACECLOFENAC,PARACETAMOL,CHLORZOXAZONE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 441)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (536, N'BA73071838994202873461F4FEFD971E', N'CYPROHEPTADINE HCL SYRUP', NULL, NULL, NULL, NULL, NULL, NULL, 347)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (537, N'BA7A3D7AFA4D41A684658FAD3A6F6E8A', N'DIGESTIVE ENZYME LIQUID', NULL, NULL, NULL, NULL, NULL, NULL, 741)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (538, N'BB0C6D4D76DD4C82830367E66D8DF6C5', N'PREGABALIN', NULL, NULL, NULL, NULL, NULL, NULL, 263)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (539, N'BB167BDA215F4556841E005B48379D1D', N'WHEY PROTIN AND DHA AND GLA', NULL, NULL, NULL, NULL, NULL, NULL, 496)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (540, N'BB2DD94CFCD345A89EA83080834EE4EE', N'PANTOPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 21)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (541, N'BB71C55582E14223A31F02DC619C40A2', N'HYDROQUINON2%TRETINOIN.0025%&MOMMENTOSONE.01%', NULL, NULL, NULL, NULL, NULL, NULL, 518)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (542, N'BB72C949B81D486C818B65FE6664B1B6', N'WHITFIELDS ONT', NULL, NULL, NULL, NULL, NULL, NULL, 407)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (543, N'BC17100C4E9A4C06AC59696553FBD1F6', N'FERROUS FUMARATE+FOLIC ACID+B12+ZINC SULPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 15)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (544, N'BD1CFCEE69D4430E86A14D4D50D1E401', N'ROSUVASTIN 10MG', NULL, NULL, NULL, NULL, NULL, NULL, 351)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (545, N'BD3D0F29C9F848CBBBA4E31722ABA296', N'ESOMEPRAZOLE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 740)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (546, N'BD4806BF598449E1B815CA773F496CDC', N'MEROPENEM 500 INJ', NULL, NULL, NULL, NULL, NULL, NULL, 485)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (547, N'BDDC8124C38E4FF6BB551E575C58F81E', N'CEFIXIME 200MG TABLET', NULL, NULL, NULL, NULL, NULL, NULL, 448)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (548, N'BE5CEC10609C4417A95995472ED266AD', N'IBUPROFEN+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 454)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (549, N'BE9490B6BA3B49CDB23501E8114A3C50', N'BENZATHINE PENICILLIN INJ', NULL, NULL, NULL, NULL, NULL, NULL, 321)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (550, N'BEE4DDB3405344EF844710F2E7BFBF47', N'PRAZOCIN HYDR.EXT. RELEASE TAB. 5 MG', NULL, NULL, NULL, NULL, NULL, NULL, 509)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (551, N'BEE908728C7D4DAEB7FD17AA539A0190', N'BISACODYL 5 MG', NULL, NULL, NULL, NULL, NULL, NULL, 478)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (552, N'BF657E48DCCE45F68B42958596E099B0', N'L-ARGINE SACHET', NULL, NULL, NULL, NULL, NULL, NULL, 669)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (553, N'BF7C51635BAD475095F2BFEBB6EC5A3B', N'ATENOLAL 50 TAB', NULL, NULL, NULL, NULL, NULL, NULL, 49)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (554, N'BFB79C13AB124E3F9CE4778029F0AFCA', N'TERNETHRIN', NULL, NULL, NULL, NULL, NULL, NULL, 358)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (555, N'C0049740E0214137B3D6E106398C2281', N'DOXYCYCLINE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 322)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (556, N'C02ECF3EC2AB4AFE8532588DACB3C441', N'ERGOTAMINE,CAFFEIN,PARACETAMOL & PROCHLORPERAZINE', NULL, NULL, NULL, NULL, NULL, NULL, 569)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (557, N'C0D92B1435604C739528934796DDDCDD', N'AMLODIPINE 5MG', NULL, NULL, NULL, NULL, NULL, NULL, 37)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (558, N'C1B727B81CB840AE8BE719303CF10DEA', N'AMOXYCILLIN POTASSIUM CLAVULANATE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 612)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (559, N'C275790D27AB4BF3B09CCCC1358748AC', N'MUPRIOCIN OINT.', NULL, NULL, NULL, NULL, NULL, NULL, 668)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (560, N'C27765231A374EC9BBD713B8BF1076DF', N'CEFUROXIME AXETIL AND POTASSIUM CLAVULANATE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 603)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (561, N'C29223B5C20040BFAE87FB88319455E1', N'ORAL REHYDRATION SALT POW', NULL, NULL, NULL, NULL, NULL, NULL, 379)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (562, N'C2C65E58075049D2B84D0D869DD4C54F', N'NIM', NULL, NULL, NULL, NULL, NULL, NULL, 479)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (563, N'C3154835692E44B7B7360B080EBB8ACB', N'LOSARTAN POTASSIUM & AMLODIPINE', NULL, NULL, NULL, NULL, NULL, NULL, 535)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (564, N'C39DCB501BFD4A6DAE0EC52579822C51', N'PEN', NULL, NULL, NULL, NULL, NULL, NULL, 401)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (565, N'C3CF94AC417F465E92F1EA6157563E1C', N'FACE PACK', NULL, NULL, NULL, NULL, NULL, NULL, 137)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (566, N'C40DD1A949A04A449C9BC04CFAE57F1C', N'ATENOLOL 25 TAB', NULL, NULL, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (567, N'C4EB71EABE0E45F286969D22550F2441', N'DOMPERIDEONE&OMEPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 65)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (568, N'C5410D82513640FE9BF50EB3D0408C3C', N'LEVETRIACETAM 500', NULL, NULL, NULL, NULL, NULL, NULL, 514)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (569, N'C5837C73384E4FF19B787A8D477A455F', N'OFLOXACIN', NULL, NULL, NULL, NULL, NULL, NULL, 160)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (570, N'C5DAE196DB0F41CD8E4E045C708FA804', N'CELECOXIB', NULL, NULL, NULL, NULL, NULL, NULL, 649)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (571, N'C5FF1E2A668C4B63B5B814861AD4F9FE', N'LEVOSALBUTAMOL,AMBROXOL,GUAIPHENESIN', NULL, NULL, NULL, NULL, NULL, NULL, 744)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (572, N'C616D9B74D8149799399047FADBDA69C', N'PROGESTERONE TABLETS 200', NULL, NULL, NULL, NULL, NULL, NULL, 446)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (573, N'C64887167F564A78BDD55013184BA763', N'METOPROLOL SUCCINATE ER TAB', NULL, NULL, NULL, NULL, NULL, NULL, 537)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (574, N'C7463A38650C4A22BBC75C50BE781E6D', N'GLIMEPIRIDE 1 MG', NULL, NULL, NULL, NULL, NULL, NULL, 524)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (575, N'C77E92A6A9CE4947AD3AA0C76EF540A6', N'THYROXINE SODIUM TAB', NULL, NULL, NULL, NULL, NULL, NULL, 629)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (576, N'C78C7D7F042D4071BB3CE3162FFB966A', N'POVIDONE IODINE+ORNIDAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 33)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (577, N'C7EA7676A94C44BF8285B51E666C9A26', N'PIPERACILLIN & TAZOBACTAM', NULL, NULL, NULL, NULL, NULL, NULL, 240)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (578, N'C8ACE7B7DC9D4CD4B733406D45AE25BB', N'GLUCOSAMINE,METHYLSULFONYLMETHANE,DIACERIN TAB', NULL, NULL, NULL, NULL, NULL, NULL, 593)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (579, N'C8CD7CF60B8D4617A4261DB7F7004D8E', N'CEPODOXIME PROXETIL', NULL, NULL, NULL, NULL, NULL, NULL, 399)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (580, N'C92D6F9F3BCE4DF9B4FD5CF006F001AA', N'GLIMIPRIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 785)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (581, N'C9CE0E3DA1BD4815B9B07E7D68E7D7FC', N'DEFLAZACORT SYP', NULL, NULL, NULL, NULL, NULL, NULL, 778)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (582, N'CA7C0589C37A4A7182AACBC947E3F1C5', N'OMGA 3,6,9', NULL, NULL, NULL, NULL, NULL, NULL, 650)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (583, N'CA84390C193242038E3466722FC4BFBF', N'SILDENAFIL CITRATE ORAL JELLY', NULL, NULL, NULL, NULL, NULL, NULL, 403)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (584, N'CB12A3AC82664ED7BBE1672A438D1DB0', N'SORE THROAT', NULL, NULL, NULL, NULL, NULL, NULL, 122)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (585, N'CB4A34A99AD94E1C9CEE7E1836C97080', N'URSODEOXYCHOLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 757)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (586, N'CB6C61B2AEF74B3F941FFF53EB96D529', N'MONTELUKAST & LEVOCETRIZIN DISPERSABLE TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 661)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (587, N'CB750C3B0194417A884DF20F2CDD847F', N'DEXTROMETHORPHAN HCL,BROMHEXINE,PHENYLEPHRINE', NULL, NULL, NULL, NULL, NULL, NULL, 687)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (588, N'CB7F4012BE804AD098722692B8CDD1A9', N'SIMVASTATIN', NULL, NULL, NULL, NULL, NULL, NULL, 216)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (589, N'CB87D428AEE54C88B19476AFBD27DDB2', N'MEFENAAMIC ACID AND DICYLOMINE HCL TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 418)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (590, N'CC2E42D30BCA4D8FA49C2E1F82F0F451', N'NORFLOXACIN 400 MG', NULL, NULL, NULL, NULL, NULL, NULL, 426)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (591, N'CC3BAE35018B4DD592624080F34A7721', N'HYDROQUINONE,TRETINOIN &MOMETASONE FUROATE CR', NULL, NULL, NULL, NULL, NULL, NULL, 411)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (592, N'CCFB686689DE4CF38676BE8153DCE0A8', N'CLARITHROMYCIN', NULL, NULL, NULL, NULL, NULL, NULL, 200)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (593, N'CD3246765A53406AB29B96A6B5F85716', N'RANITIDINE HYDROCHLORIDE INJ', NULL, NULL, NULL, NULL, NULL, NULL, 206)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (594, N'CD431A22CDD94D198889691675B231C7', N'ANCAPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 152)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (595, N'CE26C41DB8E643AD9291678A3A0BA594', N'DIGESTIVE ENZYMES', NULL, NULL, NULL, NULL, NULL, NULL, 182)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (596, N'CE2C2D9EAA904150AF1E26B4B799F8F1', N'DICYCLOMINE HCL+PARACETAMOL', NULL, NULL, NULL, NULL, NULL, NULL, 82)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (597, N'CE81C8811EE740E2A3742C707C153BB0', N'ONDESATRON ORAL SUSP.', NULL, NULL, NULL, NULL, NULL, NULL, 662)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (598, N'CE8E5AD7D4A04B1493114249D6C0A034', N'CLOTRIMAZOLE+BECLOMETHASONE', NULL, NULL, NULL, NULL, NULL, NULL, 156)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (599, N'D125F3F1664A4E2E9CE63F9BFB5F91FD', N'ANT', NULL, NULL, NULL, NULL, NULL, NULL, 521)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (600, N'D1D1893578D946F78DFCB4FD86EC0C45', N'FENOFIBRATE TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 489)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (601, N'D2A0DCCFADF74589A4ACD239144385C4', N'IRON+FOLIC ACID+CYANOCOBALAMIN', NULL, NULL, NULL, NULL, NULL, NULL, 237)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (602, N'D3412AAB40D74959820BE031DF19462D', N'ACECLOFENAC+PARACETAMOL+SERRATIOPEPTIDASE', NULL, NULL, NULL, NULL, NULL, NULL, 129)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (603, N'D39677BBCF5C453A83E0F7727677F58C', N'ACECLOFENAC,PARA,THIOCOLCHICOSIDE', NULL, NULL, NULL, NULL, NULL, NULL, 733)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (604, N'D39D66A92F054933A0B50300B69A5BC1', N'GLIMEPRIDE 2 + METFORMIN 500', NULL, NULL, NULL, NULL, NULL, NULL, 721)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (605, N'D447DC8F1D9F4FE7B0950B8A3612DC41', N'SURGICAL SUTURE', NULL, NULL, NULL, NULL, NULL, NULL, 719)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (606, N'D53A6958AB16444AB1CE5ACC3614B261', N'AYURVEDIC TONIC', NULL, NULL, NULL, NULL, NULL, NULL, 217)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (607, N'D53D2360E2AD4AB9BD38DCE8FA03E55D', N'METHYLPREDNISOLONE', NULL, NULL, NULL, NULL, NULL, NULL, 223)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (608, N'D5F43B71B2FD44889A5E9D266C35AB30', N'PROMETHAZINE THECOLATE 25MG', NULL, NULL, NULL, NULL, NULL, NULL, 419)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (609, N'D61FC86E7D0647E586226E5E2FAC504A', N'AYURVEDIC PILES OINTMENT', NULL, NULL, NULL, NULL, NULL, NULL, 227)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (610, N'D6526D3C80AA407CBB80906E8CCC3C4B', N'LYS', NULL, NULL, NULL, NULL, NULL, NULL, 231)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (612, N'D7642D0F00B240F4A32D0B7E5F27FC8B', N'AMLODIPEN', NULL, NULL, NULL, NULL, NULL, NULL, 353)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (613, N'D7AAC0890AD745409E263F00423AD29E', N'AMOXICILLIN 250', NULL, NULL, NULL, NULL, NULL, NULL, 140)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (614, N'D847752B7E8B4E3FA4C97BDC930CE1FD', N'MULTIVITAMINS & MINERALS CAP', NULL, NULL, NULL, NULL, NULL, NULL, 716)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (615, N'D94E29DACDD34B099A62D93847EC860C', N'RABEPRAZOLE GASTRO-RESISTANT', NULL, NULL, NULL, NULL, NULL, NULL, 384)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (616, N'D9AF0818264D42FDA67225F517C7D6CD', N'GLUCOSAMINE,METHYLSULFONYLMETHANE& DIACEREIN', NULL, NULL, NULL, NULL, NULL, NULL, 506)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (617, N'D9C126ACDCDB42FB9665F5DB7A819B3C', N'ALPRAZOLAM 0.25', NULL, NULL, NULL, NULL, NULL, NULL, 568)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (618, N'D9E8557F470D47F187C3A1AFF53D7D29', N'GINSENG,CALCIUM,MULTIVITAMINS,&MULTIMINIRALS', NULL, NULL, NULL, NULL, NULL, NULL, 513)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (619, N'DA9B4EE6CE9F4DEDB767C62CF57086F3', N'CEFPODOXIM PROXETIL DISP. TAB', NULL, NULL, NULL, NULL, NULL, NULL, 708)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (620, N'DAF86F383DFE49ADB7F9AB02131E6442', N'CALAMINE DIPHENHYDRAMINE HCL & CAMPHOR LOTION', NULL, NULL, NULL, NULL, NULL, NULL, 314)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (621, N'DB86E1D1B0814D08BBFCA80BCDE810ED', N'MOXIFLOXACIN HYDROCHLORIDE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 564)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (622, N'DBBF7EA733094567B02595E534117A68', N'CEFUROXIME AXELITE', NULL, NULL, NULL, NULL, NULL, NULL, 470)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (623, N'DCB31F7586C540C79D26C004F396A9CA', N'DISPOSABLE BED SHEET', NULL, NULL, NULL, NULL, NULL, NULL, 428)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (624, N'DCFAE9B4B751412B831D4A26D6090CC8', N'CYPROHEPTADINE HCL, TRICHOLINE CITRATE DROP', NULL, NULL, NULL, NULL, NULL, NULL, 691)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (625, N'DD81A55CDFD047FE93132469E606DE50', N'CLOPIDOGREL+ASPIRIN', NULL, NULL, NULL, NULL, NULL, NULL, 368)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (626, N'DDA56DC96084418BAE262BDC117C42D1', N'IBUPROFEN', NULL, NULL, NULL, NULL, NULL, NULL, 369)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (627, N'DDCB41728B82468CB4DC153139753BD0', N'IRON+MULTIVITAMIN+PROTEIN', NULL, NULL, NULL, NULL, NULL, NULL, 230)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (628, N'DE67D3685DCB48BCAB06F71534505FFD', N'ACECLOFENAC,PARA,SERRA', NULL, NULL, NULL, NULL, NULL, NULL, 735)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (629, N'DE7CBE12D04B4DAE841344EDE444B01A', N'MECOBALAMIN 500 MCG', NULL, NULL, NULL, NULL, NULL, NULL, 28)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (630, N'DE84BCF6B2A548669238A3A7A544402B', N'VOGLIBOSE+METFORMIN HCL', NULL, NULL, NULL, NULL, NULL, NULL, 366)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (631, N'DED6ABB11BEC45E490D420F675689988', N'HYDROQUNONE+TRETINOIN+MOMETASONE FUEOATE', NULL, NULL, NULL, NULL, NULL, NULL, 85)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (632, N'DF6B4D166BE047438CA8AC2B5D0A8AEB', N'PARADICHLOROBENZENE+BENZOCAINE+CHLORBUTOL', NULL, NULL, NULL, NULL, NULL, NULL, 247)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (633, N'DF7CEE2797714713B1B2A737E461F526', N'LOSARTAN POTASSIUM+HCL', NULL, NULL, NULL, NULL, NULL, NULL, 279)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (634, N'DFF6FF3C37784F4E98C06E01FFBF6E5D', N'GLIMEPIRIDE,PIOGLITAZONE,METFORMIN HEL', NULL, NULL, NULL, NULL, NULL, NULL, 284)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (635, N'E015C127BFA9403AADB62B87C3197779', N'VALPRO', NULL, NULL, NULL, NULL, NULL, NULL, 673)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (636, N'E03575E8038D418692362567D599C0B2', N'CIPROFLOXACIN 250', NULL, NULL, NULL, NULL, NULL, NULL, 194)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (637, N'E0539152247346F082175E0795852A0F', N'CHOLECALCIFEROL SOFTGEL CAP', NULL, NULL, NULL, NULL, NULL, NULL, 770)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (638, N'E0867D15E67D488D8651073572D193C9', N'HYDROQUINE, TRETINOIN & MOMETASONE FUROBATE CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 709)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (639, N'E0C396B575F74DF081965C2C1344E2F7', N'GLIMEPRIDE', NULL, NULL, NULL, NULL, NULL, NULL, 272)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (640, N'E1153F0E1F6343D49F48593816FC8215', N'POVIDONE-IODINE', NULL, NULL, NULL, NULL, NULL, NULL, 95)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (641, N'E2015F7618A84C799775EE4602175AFA', N'CALCIUM DOBESILATE CAP', NULL, NULL, NULL, NULL, NULL, NULL, 779)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (642, N'E23B549B8EA34F1497FE204ABD10D72B', N'PANTOPRAZOLE+DOMPERIDONE', NULL, NULL, NULL, NULL, NULL, NULL, 113)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (643, N'E26166B0F09C4391B2B8D1880D36F77A', N'NORFLOXACINE & BETACYCLODEXTRIN', NULL, NULL, NULL, NULL, NULL, NULL, 298)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (644, N'E2B60B0F619A4A51A13FAF4C76B85622', N'PAIK', NULL, NULL, NULL, NULL, NULL, NULL, 179)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (645, N'E3520F3304124FE7B255938ED7D8AA31', N'CLOTRIMAZOLE VAGINAL GEL', NULL, NULL, NULL, NULL, NULL, NULL, 359)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (646, N'E35B91A2FAA4428990B2770ED1C8268D', N'DICLOFENAC SODI+PARACETAMOL+CHLOROZOXAZONE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (647, N'E365278AA9374565A481A660F04C3982', N'ACRIFLAVINE+THYMOL+CETRIMIDE', NULL, NULL, NULL, NULL, NULL, NULL, 271)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (648, N'E3EA60C7F44E46B09524AE132092B51B', N'LIDOCAINE HCL GEL', NULL, NULL, NULL, NULL, NULL, NULL, 329)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (649, N'E41E69C933384B5F9A6056A451BBC82E', N'DIP', NULL, NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (650, N'E442C8E5C8FD44AA80C4A50C6706036E', N'CORAL CALCIUM WITH VIT.D3', NULL, NULL, NULL, NULL, NULL, NULL, 555)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (651, N'E46B45ABB15F436CA9FF89BA54A48D73', N'LANSOPRAZOLE 30', NULL, NULL, NULL, NULL, NULL, NULL, 155)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (652, N'E4EF1EFD7F4145B594DDCC3B808CF770', N'LOPERAMIDE HCL', NULL, NULL, NULL, NULL, NULL, NULL, 395)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (653, N'E51412AE106E4A7C995571297DBFBD01', N'TRAMCINOLONE ACETONIDE', NULL, NULL, NULL, NULL, NULL, NULL, 229)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (654, N'E5FB0E62DEFE453B9A09C60F7F58F860', N'KETOCONAZOLE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 595)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (655, N'E6296736E2E042A8AF597B0D16889BD5', N'DICLOFENAC POTASSIUM+THIOCOLCHICOSIDE 50+4', NULL, NULL, NULL, NULL, NULL, NULL, 213)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (656, N'E68938A648A64866B4F7B4283F93C8BF', N'GAMMA BENZENE LOTION', NULL, NULL, NULL, NULL, NULL, NULL, 646)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (657, N'E6D46D7097F74BC3A6E6E88D899D6EC1', N'TELMISARTAN AND AMLODIPINE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 352)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (658, N'E818C98A3B564A168D854B9602B02C7E', N'GLYCOLIC ACID,ARBUTIN,KOJIC ACID DIPALMITATE CREAM', NULL, NULL, NULL, NULL, NULL, NULL, 751)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (659, N'E8AE455D79C14D85B6A88F3AC8B01479', N'SODIUM VALPROATE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 675)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (660, N'E9A8D65329EF430399667C0598F0C77E', N'GINSENG', NULL, NULL, NULL, NULL, NULL, NULL, 198)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (661, N'E9F81E41A87D45969857A6784F322871', N'LYCOPENE+MULTIVITAMIN+MINERALS SOFTFEL', NULL, NULL, NULL, NULL, NULL, NULL, 88)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (662, N'EA084150DC464EB8B0EB2DFC119282CD', N'MENOTROPHIN INJ', NULL, NULL, NULL, NULL, NULL, NULL, 618)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (663, N'EAB12F78CE434F94A08A64B3F2935110', N'CALCIUM CITRATE,CALCITRIOL,ZINC SULPHATE', NULL, NULL, NULL, NULL, NULL, NULL, 651)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (664, N'EAD9A0ECE66D49CC9E86F5B635DC8F6F', N'LANCAPRAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 153)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (665, N'EB872525CDFB40B098FA4BA92544C0B6', N'GAMMA BENZENE HEXACHLORIDE+CETRIMIDE', NULL, NULL, NULL, NULL, NULL, NULL, 226)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (666, N'EB8BF4EE7CAE4B43BCA031E894F56D3E', N'ONDANSETRON MD TAB', NULL, NULL, NULL, NULL, NULL, NULL, 692)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (667, N'EC487BDF67AB4060992B5A19547DD6F9', N'CALCIUM WITH DHA PROTIN POWDER', NULL, NULL, NULL, NULL, NULL, NULL, 705)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (668, N'ED5B7A4DA02540599DD97C4B81BA3476', N'LYCOPEN WITH MULTIVITAMINS & MIN.', NULL, NULL, NULL, NULL, NULL, NULL, 715)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (669, N'ED8391AFD46C4DE98F705F3617E241D7', N'COTTON CREPE BANDAGE', NULL, NULL, NULL, NULL, NULL, NULL, 317)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (670, N'ED8BCE753BB84B14A95313EA2FD15263', N'AL', NULL, NULL, NULL, NULL, NULL, NULL, 759)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (671, N'ED9BA4116E0345719C05F1517D47793B', N'ACELOFENAC PARACITAMOLAND SERRATIOPEOTIDASE', NULL, NULL, NULL, NULL, NULL, NULL, 389)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (672, N'EE6EF2BC19DD46309DBB4A7A4F2E6001', N'BECLOMETHASONE DIPROPIONATE,SALICYLIC ACID', NULL, NULL, NULL, NULL, NULL, NULL, 303)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (673, N'EEC1C923E43849F088FD04A51FEEA8F7', N'CHORIONIC GONADOTROPHIN INJ', NULL, NULL, NULL, NULL, NULL, NULL, 387)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (674, N'EF522338286D4782AF88A89B9F4EBA83', N'AMOXICIIIN 500', NULL, NULL, NULL, NULL, NULL, NULL, 141)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (675, N'EFCF9774316945B3A519398092043D30', N'RANITIDINE HCL+DOMPERIDONE', NULL, NULL, NULL, NULL, NULL, NULL, 165)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (676, N'F08C8365868D4037B3E6AF50F307E364', N'CLOTRIMAZOLE VAGINAL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 363)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (677, N'F16B2F9AFCC649EC9740F16BC3166377', N'MICONAZOLE NITRATE', NULL, NULL, NULL, NULL, NULL, NULL, 235)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (678, N'F16FE699966A4EB6BF39E05C245F8CA5', N'RACECADOTRIL CAP 100MG', NULL, NULL, NULL, NULL, NULL, NULL, 601)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (679, N'F274B348AABB4B08A238B6A9140E78E8', N'ENALPRIL MALEATE', NULL, NULL, NULL, NULL, NULL, NULL, 310)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (680, N'F27A30B43E2C46C9AC789D828F0094C3', N'10', NULL, NULL, NULL, NULL, NULL, NULL, 786)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (681, N'F28E590F27244421B50DF85169B0C79E', N'TESTOSTRONE PROPIOMNATE ETC', NULL, NULL, NULL, NULL, NULL, NULL, 613)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (682, N'F2A2BF2EE11549CA8DFA9298DA445282', N'SPARFLOXACIN', NULL, NULL, NULL, NULL, NULL, NULL, 608)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (683, N'F2B74010ADAA4D5DBAD6D222D6C2A70F', N'LANSOPROAZOLE 150MG', NULL, NULL, NULL, NULL, NULL, NULL, 752)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (684, N'F2F080F240E14618A3590566849F1801', N'ACECLOFENAC,PARA,& TRAMADOL HCL TAB', NULL, NULL, NULL, NULL, NULL, NULL, 706)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (685, N'F3C9DBF67365409BBC18C71D8BC6E21F', N'PHENYLEPHERINE HCL & CHLORPHENIRAMINE-MALETE CAP', NULL, NULL, NULL, NULL, NULL, NULL, 637)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (686, N'F44550A80CF24330AF2C64BD5BC7D253', N'AZITHROMYCIN DT TAB', NULL, NULL, NULL, NULL, NULL, NULL, 313)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (687, N'F5324F72A6484B92AB541C650E1C652E', N'CALAMINE,DIPHENHYDRAMINE HCL &CAMPHOR', NULL, NULL, NULL, NULL, NULL, NULL, 466)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (688, N'F56A111AD40940319F998C1065D0BF1A', N'CLOBETASOL PROPIONATE+NEOMYCIN+MICONAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 241)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (689, N'F581C84D8C534ACCAA52952C36C50DAC', N'AMBROXYL HCL TERBUTALINE SUL,GUAIPHENSIN SYP', NULL, NULL, NULL, NULL, NULL, NULL, 660)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (690, N'F5C7188C235E403EA3F7EAC7207D36FB', N'AMOXYCILLIN&POTASSIUM CLAVULANATE', NULL, NULL, NULL, NULL, NULL, NULL, 68)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (691, N'F6547821BE154F06A10BE9B9C35257E8', N'FEBUXOSTAT 40 TAB.', NULL, NULL, NULL, NULL, NULL, NULL, 482)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (692, N'F7A26CF7C4AF48B5B086A8F6689774AA', N'LYCOPEN WITH VITAMINS & MINARELS.', NULL, NULL, NULL, NULL, NULL, NULL, 713)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (693, N'F8785914B6CA4F09AA3C8F50C62BF290', N'CUCUMBER', NULL, NULL, NULL, NULL, NULL, NULL, 330)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (694, N'F8CA79F6A61C422BA3D8277E875B97CF', N'KunalK', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (695, N'F9CD1B39DDDF4027931BE4898B604BF7', N'EYE SOLUTION.', NULL, NULL, NULL, NULL, NULL, NULL, 420)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (696, N'F9F088AD4F9E44CCACDAC043E180611F', N'ACECLOFENAC AND PARACETAMOL TABLETS', NULL, NULL, NULL, NULL, NULL, NULL, 443)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (697, N'F9FA3BA6A46C45299167037E0090A140', N'CLOTRIMAZOLE', NULL, NULL, NULL, NULL, NULL, NULL, 43)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (698, N'FA232F082E714627A2FE6433240A0F54', N'PRAZOSIN HCL EXTEND RELEASE TAB', NULL, NULL, NULL, NULL, NULL, NULL, 648)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (699, N'FA37AF46F3E3419B9EBA8851015793F0', N'NOR', NULL, NULL, NULL, NULL, NULL, NULL, 617)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (700, N'FAC1C53171AA4FB28430BC2C24625BED', N'CLOBETASOL PROPIONATE & SALICYLIC ACID OINT', NULL, NULL, NULL, NULL, NULL, NULL, 315)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (701, N'FB361D3FDB9640D9AA07939CA8833C0B', N'LEVOFLOXACIN', NULL, NULL, NULL, NULL, NULL, NULL, 306)
GO
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (702, N'FBA48D93E2204DF9968BFC042B2E0EF4', N'METFORMIN HCL,GLIMEPIRIDE', NULL, NULL, NULL, NULL, NULL, NULL, 286)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (703, N'FBF4B4BE75A748809A3A8D78A8DBD029', N'AYURVEDIC PILES TAB', NULL, NULL, NULL, NULL, NULL, NULL, 177)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (704, N'FC1CAB822A7E457A99B65D2AFA568B24', N'GAS ACIDITY AND INDIGESTION LIQ.', NULL, NULL, NULL, NULL, NULL, NULL, 484)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (705, N'FCC8D13B75C849B7BE13478106CC3710', N'ADAPALENE&CLINDAMYCIN PHOSPHATE GEL', NULL, NULL, NULL, NULL, NULL, NULL, 48)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (706, N'FDDBDDCF2E8E4DF385DA852A4544AD5B', N'CLOPIDOGREL 75 MG', NULL, NULL, NULL, NULL, NULL, NULL, 381)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (707, N'FE566CCC75CA4B619E949CC39CDB5C07', N'URSODIOL 300', NULL, NULL, NULL, NULL, NULL, NULL, 642)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (708, N'FF2DEA0B36CA4413A119EB55FA1C5B2D', N'FLUCA', NULL, NULL, NULL, NULL, NULL, NULL, 170)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (709, N'FFD1D02679254DBEB9A11DD67273CB84', N'DAPOXETINE+SILDENAFIL', NULL, NULL, NULL, NULL, NULL, NULL, 84)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (712, N'712', N'ACETONE PURE 500MG', CAST(N'2017-05-30' AS Date), CAST(N'14:44:29' AS Time), NULL, CAST(N'2017-05-30' AS Date), CAST(N'14:44:53' AS Time), NULL, NULL)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (714, N'714', N'DKDKDKDKDK12', CAST(N'2017-06-02' AS Date), CAST(N'17:56:10' AS Time), NULL, CAST(N'2017-06-02' AS Date), CAST(N'17:58:08' AS Time), NULL, NULL)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (715, NULL, N'PPPPPPP', CAST(N'2022-07-06' AS Date), CAST(N'16:53:38' AS Time), N'1', NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergenericcategory] ([GenericCategoryID], [ID], [GenericCategoryName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [DrugCode]) VALUES (716, NULL, N'PDDDD', CAST(N'2022-07-06' AS Date), CAST(N'16:57:40' AS Time), N'1', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[mastergroup] ON 

INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (21, N'CAPITAL ACCOUNT', NULL, N'30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (22, N'CURRENT ASSETS ACCOUNT', N'G', N'2', N'2', N'Y', N'Y', NULL, 3, CAST(22.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5453765.04 AS Decimal(18, 2)), CAST(5386225.00 AS Decimal(18, 2)), CAST(676215.00 AS Decimal(18, 2)), CAST(608674.96 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (23, N'BANK ACCOUNTS', NULL, N'25', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(77393.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (24, N'CASH IN HAND', NULL, N'24', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(608674.96 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (25, N'PREPAID EXPENSES ASSET', N'G', N'22', N'2', N'Y', N'', NULL, NULL, CAST(25.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (26, N'LOANS & ADVANCES ASSET', N'G', N'22', N'22', N'Y', N'', NULL, NULL, CAST(26.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (27, N'STOCKS', N'G', N'22', N'2', N'Y', N'', NULL, NULL, CAST(27.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (28, N'SUNDRY DEBTOR', N'D', N'22', N'2', N'Y', N'X', NULL, NULL, CAST(28.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(662998.00 AS Decimal(18, 2)), CAST(64176.00 AS Decimal(18, 2)), CAST(598822.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (29, N'CURRENT LIABILITIES ACCOUNT', N'G', N'3', N'3', N'Y', N'Y', NULL, 4, CAST(29.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1498466.37 AS Decimal(18, 2)), CAST(1809482.68 AS Decimal(18, 2)), CAST(54001.00 AS Decimal(18, 2)), CAST(365017.31 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (30, N'DUTIES & TAXES', N'G', N'29', N'3', N'Y', N'', NULL, NULL, CAST(30.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(55852.37 AS Decimal(18, 2)), CAST(209898.68 AS Decimal(18, 2)), CAST(54000.00 AS Decimal(18, 2)), CAST(208046.31 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (31, N'SUNDRY CREDITOR', N'C', N'29', N'3', N'Y', N'X', NULL, NULL, CAST(31.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1442614.00 AS Decimal(18, 2)), CAST(1599584.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(156971.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (32, N'PROVISIONS', NULL, N'303', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (33, N'FIXED ASSETS  ACCOUNT', N'G', N'5', N'5', N'Y', N'Y', NULL, NULL, CAST(33.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (36, N'INVESTMENTS ACCOUNT', N'G', N'7', N'7', N'Y', N'Y', NULL, 5, CAST(36.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (37, N'LOANS LIABILIES ACCOUNT', N'G', N'8', N'8', N'Y', N'Y', NULL, 6, CAST(37.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(990000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(990000.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (38, N'BANK OCC ACCOUNT', N'G', N'37', N'8', N'Y', N' ', NULL, NULL, CAST(38.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (39, N'SECURED LOANS', N'G', N'37', N'8', N'Y', N'', NULL, NULL, CAST(39.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (40, N'UNSECURED LOANS', N'G', N'37', N'8', N'Y', N'', NULL, NULL, CAST(40.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (41, N'MISC. EXPENSES (ASSETS) ACCOUNT', N'G', N'9', N'9', N'Y', N'Y', NULL, 7, CAST(41.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (42, N'PROFIT AND LOSS ACCOUNT', N'G', N'10', N'10', N'Y', N'Y', NULL, 8, CAST(42.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (43, N'EXPENDITURE ACCOUNT', N'G', N'4', N'4', N'Y', N'Y', NULL, 9, CAST(43.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(455151.00 AS Decimal(18, 2)), CAST(32688.15 AS Decimal(18, 2)), CAST(422462.85 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (44, N'EXPENSES DIRECT', N'G', N'43', N'4', N'Y', N' ', NULL, NULL, CAST(44.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(455151.00 AS Decimal(18, 2)), CAST(32688.15 AS Decimal(18, 2)), CAST(422462.85 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (45, N'EXPENSES INDIRECT', N'G', N'43', N'4', N'Y', N' ', NULL, NULL, CAST(45.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (46, N'STATUTORY AUDIT FEES', N'G', N'45', N'43', N'Y', N' ', NULL, NULL, CAST(46.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (47, N'DEPRECIATION ON FIXED ASSETS', N'G', N'45', N'43', N'Y', N' ', NULL, NULL, CAST(47.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (48, N'PRINTING AND STATIONARY', N'G', N'45', N'43', N'Y', N' ', NULL, NULL, CAST(48.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (49, N'TRAVELING AND CONVEYANCE', N'G', N'45', N'43', N'Y', N' ', NULL, NULL, CAST(49.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (52, N'POSTAGE AND COURIER', N'G', N'45', N'43', N'Y', N' ', NULL, NULL, CAST(52.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (55, N'INCOME REVENUE ACCOUNT', N'G', N'6', N'6', N'Y', N'Y', NULL, 10, CAST(55.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (56, N'PURCHASE ACCOUNT', N'G', N'11', N'11', N'Y', N'Y', NULL, 11, CAST(56.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(3363460.57 AS Decimal(18, 2)), CAST(138493.78 AS Decimal(18, 2)), CAST(3363191.86 AS Decimal(18, 2)), CAST(138225.07 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (57, N'CASH PURCHASE', NULL, N'38', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(1518809.54 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (58, N'CREDIT PURCHASE', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(58.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2314.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2314.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (59, N'CASH CREDIT PURCHASE', NULL, N'49', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(1498701.14 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (60, N'SALES ACCOUNT', N'G', N'13', N'13', N'Y', N'Y', NULL, 12, CAST(60.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(2413958.36 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(2413958.36 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (61, N'CASH SALE', NULL, N'38', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(2268439.03 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (62, N'CREDIT SALE', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(62.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (63, N'CASH CREDIT SALE', NULL, N'60', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(99326.97 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (64, N'SALES RETURN', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(64.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (65, N'DISCOUNT SALE', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(65.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4.75 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4.75 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (66, N'DEBIT NOTE SALE', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(66.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (67, N'ROUND OFF SALE', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(67.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.25 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.25 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (68, N'SALES ADD ON', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(68.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (69, N'SALE FOR ZERO VAT', N'G', N'60', N'13', N'Y', N' ', NULL, NULL, CAST(69.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(46192.36 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(46192.36 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (70, N'OTHER ADVANCES', N'G', N'26', N'22', N'Y', N'', NULL, NULL, CAST(70.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (71, N'STAFF ADVANCES', N'G', N'26', N'22', N'Y', N'', NULL, NULL, CAST(71.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (72, N'PURCHASE OCTROI HAMALI', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(72.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(92.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(92.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (73, N'PURCHASE LESS', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, CAST(73.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (74, N'OTHER SUNDRY ASSETS', N'G', N'33', N'5', N'Y', N'', NULL, NULL, CAST(74.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (76, N'OTHER CURRENT LIABILITES', N'G', N'29', N'3', N'Y', N'', NULL, NULL, CAST(76.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (79, N'INVESTMENTS', N'G', N'36', N'7', N'Y', N'', NULL, NULL, CAST(79.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (82, N'FIXED CAPITAL A/C', N'G', N'21', N'1', N'Y', N'', NULL, NULL, CAST(82.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (83, N'CURRENT CAPITAL ACCOUNT', N'G', N'15', N'15', N'Y', N'Y', NULL, NULL, CAST(83.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (84, N'REBATE AND REMISSION ACCOUNT', N'G', N'12', N'12', N'Y', N'Y', NULL, 14, CAST(84.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (85, N'PURCHASE RETURN', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(85.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (100, N'DIFFERENCE IN BALANCE', N'G', N'20', N'20', N'Y', N'Y', NULL, 15, CAST(100.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (101, N'OTHER CREDITORS', N'R', N'29', N'3', N'Y', N'X', NULL, NULL, CAST(101.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (102, N'OTHER DEBTORS', N'T', N'22', N'2', N'Y', N'X', NULL, NULL, CAST(102.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (109, N'BANK CHARGES AND COMMISSION', NULL, N'30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (111, N'SUSPENSE SUB  ACCOUNT', N'G', N'65', N'60', N'Y', N' ', NULL, NULL, CAST(111.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (113, N'ADVANCE AND OTHER CURRENT ASSTES', NULL, N'22', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'12:35:27' AS Time), NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (114, N'DEPOSITS', N'G', N'22', N'2', N'Y', N'', NULL, NULL, CAST(114.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (117, N'CURRENT LIABLITIES', N'G', N'29', N'3', N'Y', N'', NULL, NULL, CAST(117.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (122, N'PROFIT AND LOSS A/C', N'G', N'42', N'10', N'Y', N' ', NULL, NULL, CAST(122.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (128, N'STOCK OF SUNDRY ITEMS', N'G', N'22', N'2', N'Y', N'', NULL, NULL, CAST(128.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (129, N'FURNITURE AND FITTINGS', N'G', N'33', N'5', N'Y', N'', NULL, NULL, CAST(129.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (130, N'PURCHASE CASH DISCOUNT', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(130.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(41156.35 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(41156.35 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (131, N'PURCHASE SPECIAL DISCOUNT', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(131.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (132, N' PURCHASE SCHEME DISCOUNT', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, CAST(132.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(55992.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(55992.50 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (133, N'PURCHASE ITEM DISCOUNT', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, CAST(133.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(36492.09 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(36492.09 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (134, N'PURCHASE DEBIT NOTE', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(134.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4572.48 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4572.48 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (135, N'PURCHASE CREDIT NOTE', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, CAST(135.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4.10 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4.10 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (136, N'VAT INPUT 5%', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, CAST(136.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(75202.91 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(75202.91 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (137, N'VAT INPUT 12 POINT 5 PERCENT', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(137.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(205083.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(205083.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (138, N'PURCHASE ROUND OFF', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(138.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(268.71 AS Decimal(18, 2)), CAST(280.36 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11.65 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (139, N'PURCHASE FOR ZERO PURCHASE', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(139.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(62983.40 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(62983.40 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (140, N'PURCHASE ADD ON', N'G', N'56', N'11', N'Y', N' ', NULL, NULL, CAST(140.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (155, N'PURCHASE FOR 13.5 PER VAT', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (156, N'PURCHASE FOR 6 PER VAT', N'G', N'56', N'11', N'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (300, N'SUSPENSE ACCOUNT', N'G', N'42', N'10', N'Y', N'', NULL, 13, CAST(300.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (301, N'VISHAL NIKAM', NULL, N'300', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (302, N'VISHAL NIKAM', NULL, N'27', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (303, N'kunal kadlag', NULL, N'302', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (304, N'snehal', NULL, N'302', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (305, N'CASH SALE', NULL, N'60', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (306, N'dharma', NULL, N'21', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-05-30' AS Date), CAST(N'14:33:04' AS Time), NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (308, N'Ddddd', NULL, N'21', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-03-22' AS Date), CAST(N'18:55:02' AS Time), NULL, NULL, NULL, NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (309, N'scorg technologies1', NULL, N'30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-05-30' AS Date), CAST(N'15:03:51' AS Time), NULL, CAST(N'2017-05-30' AS Date), CAST(N'15:04:25' AS Time), NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (310, N'kj some', NULL, N'29', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-06-02' AS Date), CAST(N'13:40:07' AS Time), NULL, CAST(N'2017-06-02' AS Date), CAST(N'13:40:22' AS Time), NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (311, N'scorg group ', NULL, N'30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-06-02' AS Date), CAST(N'14:40:59' AS Time), NULL, CAST(N'2017-06-02' AS Date), CAST(N'14:41:30' AS Time), NULL)
INSERT [dbo].[mastergroup] ([GroupID], [GroupName], [GroupCode], [UnderGroupId], [UnderGroupIDParentID], [IFFIX], [IFMainGroup], [IfSubGroup], [SerialNumber], [LevelNumber], [BalanceSheetCode], [ShowInBalanceSheet], [BalanceSheetSrNumber], [OpeningDebit], [OpeningCredit], [TransactionDebit], [TransactionCredit], [ClosingDebit], [ClosingCredit], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2000, N'CLOSING STOCK', N'G', N'', N'', N'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[mastergroup] OFF
GO
SET IDENTITY_INSERT [dbo].[mastermessage] ON 

INSERT [dbo].[mastermessage] ([MessageId], [Message], [Fromdate], [Todate], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (1, N'HAPPY DIWALI', NULL, NULL, 1, CAST(N'2021-09-14' AS Date), CAST(N'19:39:39' AS Time), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[mastermessage] OFF
GO
INSERT [dbo].[masterpack] ([PackID], [PackName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (9, N'100 ML', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterpack] ([PackID], [PackName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (10, N'10 TAB', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterpack] ([PackID], [PackName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (11, N'1 ML', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterpack] ([PackID], [PackName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (12, N'2 ML', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[masterpacktype] ([ID], [PackTypeName]) VALUES (0, N'SYRING')
INSERT [dbo].[masterpacktype] ([ID], [PackTypeName]) VALUES (3, N'BOTTLE')
INSERT [dbo].[masterpacktype] ([ID], [PackTypeName]) VALUES (4, N'STRIP')
INSERT [dbo].[masterpacktype] ([ID], [PackTypeName]) VALUES (5, N'CAPSULE')
GO
INSERT [dbo].[masterproduct] ([ProductID], [ProdName], [ProdLoosePack], [ProdPack], [ProdPackType], [ProdBoxQuantity], [ProdCompShortName], [ProdVATPercent], [ProdCST], [ProdCSTPercent], [ProdGrade], [ProdScheduleDrugCode], [ProdDPCOCode], [ProdIfSchedule], [ProdIfShortListed], [ProdIfSaleDisc], [ProdIfPurchaseRateInclusive], [ProdIfMRPInclusive], [ProdIfBarCodeRequired], [ProdIfOctroi], [ProdRequireColdStorage], [ProdMinLevel], [ProdMaxLevel], [ProdMargin], [ProdLastPurchaseBillNumber], [ProdLastPurchaseDate], [ProdLastPurchasePartyId], [ProdLastPurchaseVoucherType], [ProdLastPurchaseVoucherNumber], [ProdLastPurchaseRate], [ProdLastPurchaseTradeRate], [ProdLastPurchaseSaleRate], [ProdLastPurchasePTR], [ProdLastPurchaseCNF], [ProdLastPurchaseEcoMart], [ProdLastPurchaseMRP], [ProdLastPurchaseVATPer], [ProdLastPurchaseCSTPer], [ProdLastPurchaseCST], [ProdLastPurchaseSCMPer], [ProdLastPurchaseSCM], [ProdLastPurchaseItemDiscPer], [ProdLastPurchaseLocalTaxPer], [ProdLastPurchaseLocalTaxAmt], [ProdLastPurchaseExpiry], [ProdLastPurchaseExpiryDate], [ProdLastPurchaseBatchNumber], [ProdLastPurchaseStockID], [ProdOpeningStock], [ProdClosingStock], [ProdUserDefineCode], [ProdSchemeOpeningQty], [ProdSchemePurchaseQty], [ProdSchemeSaleQty], [ProdSchemeCRQty], [ProdSchemeDBQty], [ProdOctroiPer], [ProdLastSaleBillType], [ProdLastSaleBillNumber], [ProdLastSaleDate], [ProdLastSalePartyId], [ProdLastSaleStockID], [ProdLastSaleScanID], [TAG], [MSCDACode], [SSOpeningStock], [SSPurchaseStock], [SSSaleStock], [SSCRStock], [SSDRStock], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [productCode], [companyCode], [GlobalID], [opstock], [purstock], [salestock], [crstock], [dbstock], [PacktypeId], [ProdCompID], [ProdShelfID], [ProdDrugID], [ProdCategoryID], [ProdLBTID], [ProdPartyId_1], [ProdPartyId_2], [ProdTaxID], [prodmrp], [HSNNumber], [ScannedBarcode]) VALUES (1, N'PROD', 1, N'1 ML', N'SYRING', 0, N'CIP', CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'A', N'', NULL, N'Y', N'Y', N'N', N'N', N'N', N'N', N'N', N'N', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2022-07-06' AS Date), CAST(N'10:35:03' AS Time), N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 1, NULL, 0, 0, NULL, NULL, CAST(344 AS Numeric(18, 0)), NULL)
INSERT [dbo].[masterproduct] ([ProductID], [ProdName], [ProdLoosePack], [ProdPack], [ProdPackType], [ProdBoxQuantity], [ProdCompShortName], [ProdVATPercent], [ProdCST], [ProdCSTPercent], [ProdGrade], [ProdScheduleDrugCode], [ProdDPCOCode], [ProdIfSchedule], [ProdIfShortListed], [ProdIfSaleDisc], [ProdIfPurchaseRateInclusive], [ProdIfMRPInclusive], [ProdIfBarCodeRequired], [ProdIfOctroi], [ProdRequireColdStorage], [ProdMinLevel], [ProdMaxLevel], [ProdMargin], [ProdLastPurchaseBillNumber], [ProdLastPurchaseDate], [ProdLastPurchasePartyId], [ProdLastPurchaseVoucherType], [ProdLastPurchaseVoucherNumber], [ProdLastPurchaseRate], [ProdLastPurchaseTradeRate], [ProdLastPurchaseSaleRate], [ProdLastPurchasePTR], [ProdLastPurchaseCNF], [ProdLastPurchaseEcoMart], [ProdLastPurchaseMRP], [ProdLastPurchaseVATPer], [ProdLastPurchaseCSTPer], [ProdLastPurchaseCST], [ProdLastPurchaseSCMPer], [ProdLastPurchaseSCM], [ProdLastPurchaseItemDiscPer], [ProdLastPurchaseLocalTaxPer], [ProdLastPurchaseLocalTaxAmt], [ProdLastPurchaseExpiry], [ProdLastPurchaseExpiryDate], [ProdLastPurchaseBatchNumber], [ProdLastPurchaseStockID], [ProdOpeningStock], [ProdClosingStock], [ProdUserDefineCode], [ProdSchemeOpeningQty], [ProdSchemePurchaseQty], [ProdSchemeSaleQty], [ProdSchemeCRQty], [ProdSchemeDBQty], [ProdOctroiPer], [ProdLastSaleBillType], [ProdLastSaleBillNumber], [ProdLastSaleDate], [ProdLastSalePartyId], [ProdLastSaleStockID], [ProdLastSaleScanID], [TAG], [MSCDACode], [SSOpeningStock], [SSPurchaseStock], [SSSaleStock], [SSCRStock], [SSDRStock], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [productCode], [companyCode], [GlobalID], [opstock], [purstock], [salestock], [crstock], [dbstock], [PacktypeId], [ProdCompID], [ProdShelfID], [ProdDrugID], [ProdCategoryID], [ProdLBTID], [ProdPartyId_1], [ProdPartyId_2], [ProdTaxID], [prodmrp], [HSNNumber], [ScannedBarcode]) VALUES (2, N'PROD - 1', 1, N'1 ML', N'SYRING', 0, N'CIP', CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'A', N'', NULL, N'Y', N'Y', N'N', N'N', N'N', N'N', N'N', N'N', 0, 0, CAST(0.00 AS Decimal(18, 2)), N'46', N'20220715', N'23', N'PCR', N'21', CAST(100.00 AS Decimal(18, 2)), CAST(150.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), NULL, CAST(230.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'09/25', N'20250901', N'T-876', N'0', 0, 1138, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'SCR', 49, N'20220708', 7, 15, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2022-07-06' AS Date), CAST(N'12:35:43' AS Time), N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 1, NULL, 0, 0, NULL, CAST(230 AS Decimal(18, 0)), CAST(3444 AS Numeric(18, 0)), NULL)
INSERT [dbo].[masterproduct] ([ProductID], [ProdName], [ProdLoosePack], [ProdPack], [ProdPackType], [ProdBoxQuantity], [ProdCompShortName], [ProdVATPercent], [ProdCST], [ProdCSTPercent], [ProdGrade], [ProdScheduleDrugCode], [ProdDPCOCode], [ProdIfSchedule], [ProdIfShortListed], [ProdIfSaleDisc], [ProdIfPurchaseRateInclusive], [ProdIfMRPInclusive], [ProdIfBarCodeRequired], [ProdIfOctroi], [ProdRequireColdStorage], [ProdMinLevel], [ProdMaxLevel], [ProdMargin], [ProdLastPurchaseBillNumber], [ProdLastPurchaseDate], [ProdLastPurchasePartyId], [ProdLastPurchaseVoucherType], [ProdLastPurchaseVoucherNumber], [ProdLastPurchaseRate], [ProdLastPurchaseTradeRate], [ProdLastPurchaseSaleRate], [ProdLastPurchasePTR], [ProdLastPurchaseCNF], [ProdLastPurchaseEcoMart], [ProdLastPurchaseMRP], [ProdLastPurchaseVATPer], [ProdLastPurchaseCSTPer], [ProdLastPurchaseCST], [ProdLastPurchaseSCMPer], [ProdLastPurchaseSCM], [ProdLastPurchaseItemDiscPer], [ProdLastPurchaseLocalTaxPer], [ProdLastPurchaseLocalTaxAmt], [ProdLastPurchaseExpiry], [ProdLastPurchaseExpiryDate], [ProdLastPurchaseBatchNumber], [ProdLastPurchaseStockID], [ProdOpeningStock], [ProdClosingStock], [ProdUserDefineCode], [ProdSchemeOpeningQty], [ProdSchemePurchaseQty], [ProdSchemeSaleQty], [ProdSchemeCRQty], [ProdSchemeDBQty], [ProdOctroiPer], [ProdLastSaleBillType], [ProdLastSaleBillNumber], [ProdLastSaleDate], [ProdLastSalePartyId], [ProdLastSaleStockID], [ProdLastSaleScanID], [TAG], [MSCDACode], [SSOpeningStock], [SSPurchaseStock], [SSSaleStock], [SSCRStock], [SSDRStock], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [productCode], [companyCode], [GlobalID], [opstock], [purstock], [salestock], [crstock], [dbstock], [PacktypeId], [ProdCompID], [ProdShelfID], [ProdDrugID], [ProdCategoryID], [ProdLBTID], [ProdPartyId_1], [ProdPartyId_2], [ProdTaxID], [prodmrp], [HSNNumber], [ScannedBarcode]) VALUES (3, N'PROD - 2', 1, N'2 ML', N'CAPSULE', 0, N'CIP', CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'A', N'', NULL, N'Y', N'Y', N'N', N'N', N'N', N'N', N'N', N'N', 0, 0, CAST(0.00 AS Decimal(18, 2)), N'46', N'20220715', N'23', N'PCR', N'21', CAST(25.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(35.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(35.00 AS Decimal(18, 2)), NULL, CAST(45.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'00/00', N'', N'T-654', N'0', 0, 207, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'SCR', 45, N'20220708', 7, 16, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2022-07-06' AS Date), CAST(N'12:53:19' AS Time), N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 1, NULL, 0, 0, NULL, CAST(45 AS Decimal(18, 0)), CAST(49094 AS Numeric(18, 0)), NULL)
INSERT [dbo].[masterproduct] ([ProductID], [ProdName], [ProdLoosePack], [ProdPack], [ProdPackType], [ProdBoxQuantity], [ProdCompShortName], [ProdVATPercent], [ProdCST], [ProdCSTPercent], [ProdGrade], [ProdScheduleDrugCode], [ProdDPCOCode], [ProdIfSchedule], [ProdIfShortListed], [ProdIfSaleDisc], [ProdIfPurchaseRateInclusive], [ProdIfMRPInclusive], [ProdIfBarCodeRequired], [ProdIfOctroi], [ProdRequireColdStorage], [ProdMinLevel], [ProdMaxLevel], [ProdMargin], [ProdLastPurchaseBillNumber], [ProdLastPurchaseDate], [ProdLastPurchasePartyId], [ProdLastPurchaseVoucherType], [ProdLastPurchaseVoucherNumber], [ProdLastPurchaseRate], [ProdLastPurchaseTradeRate], [ProdLastPurchaseSaleRate], [ProdLastPurchasePTR], [ProdLastPurchaseCNF], [ProdLastPurchaseEcoMart], [ProdLastPurchaseMRP], [ProdLastPurchaseVATPer], [ProdLastPurchaseCSTPer], [ProdLastPurchaseCST], [ProdLastPurchaseSCMPer], [ProdLastPurchaseSCM], [ProdLastPurchaseItemDiscPer], [ProdLastPurchaseLocalTaxPer], [ProdLastPurchaseLocalTaxAmt], [ProdLastPurchaseExpiry], [ProdLastPurchaseExpiryDate], [ProdLastPurchaseBatchNumber], [ProdLastPurchaseStockID], [ProdOpeningStock], [ProdClosingStock], [ProdUserDefineCode], [ProdSchemeOpeningQty], [ProdSchemePurchaseQty], [ProdSchemeSaleQty], [ProdSchemeCRQty], [ProdSchemeDBQty], [ProdOctroiPer], [ProdLastSaleBillType], [ProdLastSaleBillNumber], [ProdLastSaleDate], [ProdLastSalePartyId], [ProdLastSaleStockID], [ProdLastSaleScanID], [TAG], [MSCDACode], [SSOpeningStock], [SSPurchaseStock], [SSSaleStock], [SSCRStock], [SSDRStock], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [productCode], [companyCode], [GlobalID], [opstock], [purstock], [salestock], [crstock], [dbstock], [PacktypeId], [ProdCompID], [ProdShelfID], [ProdDrugID], [ProdCategoryID], [ProdLBTID], [ProdPartyId_1], [ProdPartyId_2], [ProdTaxID], [prodmrp], [HSNNumber], [ScannedBarcode]) VALUES (4, N'PPROD', 1, N'2 ML', N'CAPSULE', 0, N'CIP', CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'A', N'', NULL, N'Y', N'Y', N'N', N'N', N'N', N'N', N'N', N'N', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2022-07-06' AS Date), CAST(N'12:56:00' AS Time), N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 1, NULL, 0, 0, NULL, NULL, CAST(49094 AS Numeric(18, 0)), NULL)
GO
INSERT [dbo].[masterproductcategory] ([ProductCategoryID], [ID], [ProductCategoryName], [SaleDiscount], [LBTPercent], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [catcode]) VALUES (1, NULL, N'ALOPATHY', NULL, NULL, CAST(N'2021-09-14' AS Date), CAST(N'19:54:51' AS Time), N'1', NULL, NULL, NULL, NULL)
INSERT [dbo].[masterproductcategory] ([ProductCategoryID], [ID], [ProductCategoryName], [SaleDiscount], [LBTPercent], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [catcode]) VALUES (2, NULL, N'VETERNARY', NULL, NULL, CAST(N'2021-09-15' AS Date), CAST(N'12:52:04' AS Time), N'1', NULL, NULL, NULL, NULL)
INSERT [dbo].[masterproductcategory] ([ProductCategoryID], [ID], [ProductCategoryName], [SaleDiscount], [LBTPercent], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [catcode]) VALUES (3, NULL, N'SURGICAL', NULL, NULL, CAST(N'2022-07-06' AS Date), CAST(N'16:56:32' AS Time), N'1', NULL, NULL, NULL, NULL)
INSERT [dbo].[masterproductcategory] ([ProductCategoryID], [ID], [ProductCategoryName], [SaleDiscount], [LBTPercent], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [catcode]) VALUES (4, NULL, N'UNANI', NULL, NULL, CAST(N'2022-07-06' AS Date), CAST(N'16:58:00' AS Time), N'1', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[masterpurchaseordercnf] ON 

INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (8, 2223, N'POR', 56, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'19:00:37', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (9, 2223, N'POR', 57, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'19:08:10', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (10, 2223, N'POR', 58, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'19:48:43', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (11, 2223, N'POR', 59, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'19:53:07', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (12, 2223, N'POR', 60, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'19:58:07', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (13, 2223, N'POR', 61, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'20:08:48', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (14, 2223, N'POR', 62, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'20:19:06', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (15, 2223, N'POR', 63, N'20220708', 1, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'20:28:04', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseordercnf] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (16, 2223, N'POR', 64, N'20220709', 22, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220709', N'10:07:10', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[masterpurchaseordercnf] OFF
GO
SET IDENTITY_INSERT [dbo].[masterpurchaseorderstockist] ON 

INSERT [dbo].[masterpurchaseorderstockist] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (37, 2223, N'POR', 54, N'20220708', 22, NULL, CAST(5750.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'18:21:13', NULL, NULL, NULL)
INSERT [dbo].[masterpurchaseorderstockist] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountID], [CompanyID], [Amount], [Narration], [if_uploaded], [CreatedUserID], [CreatedDate], [CreatedTime], [ModifiedUserID], [ModifiedDate], [ModifiedTime]) VALUES (38, 2223, N'POR', 55, N'20220708', 22, NULL, CAST(7130.00 AS Decimal(18, 2)), N'', NULL, 1, N'20220708', N'18:33:13', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[masterpurchaseorderstockist] OFF
GO
SET IDENTITY_INSERT [dbo].[mastersalesman] ON 

INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1, N'DATTU MORE', N'', N'', N'', N'', N'', NULL, CAST(N'2021-09-15' AS Date), CAST(N'13:49:53' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, N'MANOJ SATAV', N'', N'', N'', N'', N'', NULL, CAST(N'2021-09-15' AS Date), CAST(N'13:50:16' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (3, N'SUHAS CHAVAN', N'', N'', N'', N'', N'', NULL, CAST(N'2021-09-15' AS Date), CAST(N'13:52:56' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, N'RAJU KALSEKAR', N'DKFJKDJF', N'DKLFJ', N'3043049', N'304903444', N'', NULL, CAST(N'2021-09-15' AS Date), CAST(N'18:43:35' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, N'SUHAS CHAVAN', N'', N'', N'', N'', N'', NULL, CAST(N'2022-05-14' AS Date), CAST(N'12:47:21' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (6, N'BABU JADHAV', N'PUNE', N'', N'', N'', N'', NULL, CAST(N'2022-05-24' AS Date), CAST(N'20:15:13' AS Time), N'1', NULL, NULL, NULL)
INSERT [dbo].[mastersalesman] ([SalesmanID], [SalesmanName], [Address1], [Address2], [TelephoneNumber], [MobileNumberForSMS], [Email], [Remarks], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (7, N'PC', N'ADD', N'ADD', N'', N'', N'', NULL, CAST(N'2022-05-29' AS Date), CAST(N'20:16:04' AS Time), N'1', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[mastersalesman] OFF
GO
INSERT [dbo].[mastersaletype] ([SaleTypeId], [SaleTypeName], [Status]) VALUES (1, N'Regular Sale', N'1')
INSERT [dbo].[mastersaletype] ([SaleTypeId], [SaleTypeName], [Status]) VALUES (2, N'Counter Sale', N'1')
INSERT [dbo].[mastersaletype] ([SaleTypeId], [SaleTypeName], [Status]) VALUES (3, N'Special Sale', N'1')
INSERT [dbo].[mastersaletype] ([SaleTypeId], [SaleTypeName], [Status]) VALUES (4, N'MRP Sale', N'1')
INSERT [dbo].[mastersaletype] ([SaleTypeId], [SaleTypeName], [Status]) VALUES (5, N'PTS Sale', N'1')
GO
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, 25, 10, 10, 10, 11, 11, 11, NULL, CAST(N'2016-02-09' AS Date), CAST(N'2017-10-12' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (3, 42, 11, 1, 11, 0, 0, 0, NULL, CAST(N'2015-06-20' AS Date), CAST(N'2016-12-10' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, 42, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-05-24' AS Date), CAST(N'16:17:41' AS Time), NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, 1, 9, 2, 9, 2, 10, 1, NULL, CAST(N'2015-08-11' AS Date), CAST(N'2016-12-22' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (6, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2016-05-10' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (7, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2016-05-10' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (8, NULL, 1, 1, 1, 1, 1, 1, NULL, NULL, CAST(N'2016-11-02' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (9, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-09-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (10, 1512, 5, 1, 5, 1, 5, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-03-22' AS Date), CAST(N'19:05:49' AS Time), NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (11, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (12, NULL, 29, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-26' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (13, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-20' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (14, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2015-11-04' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (15, NULL, 9, 1, 18, 2, 27, 3, NULL, CAST(N'2015-08-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (16, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (17, NULL, 4, 1, 8, 2, 0, 0, NULL, CAST(N'2015-06-05' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (18, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-01' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (19, NULL, 10, 1, 5, 1, 4, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-19' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (20, NULL, 29, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-26' AS Date), CAST(N'2014-08-31' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (21, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2015-10-16' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (22, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (23, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-11-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (24, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-08' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (25, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2017-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (26, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (27, NULL, 30, 1, 0, 0, 0, 0, NULL, CAST(N'2015-09-10' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (28, NULL, 5, 1, 5, 1, 5, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-19' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (29, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (30, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2014-02-21' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (31, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-26' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (32, NULL, 14, 1, 0, 0, 0, 0, NULL, CAST(N'2014-03-05' AS Date), CAST(N'2014-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (33, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-28' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (34, NULL, 1, 1, 1, 1, 10, 10, NULL, CAST(N'2015-07-28' AS Date), CAST(N'2015-07-28' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (35, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-28' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (36, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (37, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-09' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (38, NULL, 9, 1, 18, 2, 27, 3, NULL, CAST(N'2015-07-01' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (39, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-08-05' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (40, NULL, 14, 1, 0, 0, 0, 0, NULL, CAST(N'2014-03-05' AS Date), CAST(N'2014-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (41, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (42, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-20' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (43, NULL, 29, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-26' AS Date), CAST(N'2016-03-31' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (44, NULL, 10, 1, 5, 1, 4, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-19' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (45, NULL, 14, 1, 0, 0, 0, 0, NULL, CAST(N'2014-03-05' AS Date), CAST(N'2014-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (46, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-08' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (47, NULL, 4, 1, 8, 2, 12, 3, NULL, CAST(N'2015-04-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (48, NULL, 15, 1, 30, 2, 0, 0, NULL, CAST(N'2015-11-14' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (49, NULL, 4, 1, 8, 2, 12, 3, NULL, CAST(N'2015-11-07' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (50, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-08-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (51, NULL, 9, 1, 18, 2, 27, 3, NULL, CAST(N'2015-07-01' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (52, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-04-10' AS Date), CAST(N'2016-04-10' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (53, NULL, 5, 1, 0, 0, 0, 0, NULL, CAST(N'2015-10-29' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (54, NULL, 5, 1, 0, 0, 0, 0, NULL, CAST(N'2015-10-31' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (55, NULL, 5, 2, 10, 4, 0, 0, NULL, CAST(N'2015-10-01' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (56, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-04-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (57, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-28' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (58, NULL, 9, 1, 18, 2, 27, 3, NULL, CAST(N'2015-08-20' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (59, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (60, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (61, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (62, NULL, 9, 1, 18, 2, 0, 0, NULL, CAST(N'2015-09-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (63, NULL, 4, 2, 0, 0, 0, 0, NULL, CAST(N'2015-07-28' AS Date), CAST(N'2014-05-24' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (64, NULL, 1, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (65, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (66, NULL, 15, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-30' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (67, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (68, NULL, 9, 1, 18, 2, 0, 0, NULL, CAST(N'2015-06-20' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (69, NULL, 1, 1, 2, 2, 3, 3, NULL, CAST(N'2015-05-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (70, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-12' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (71, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (72, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (73, NULL, 5, 1, 10, 2, 15, 3, NULL, CAST(N'2015-10-27' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (74, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-30' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (75, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-12-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (76, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (77, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-10-13' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (78, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (79, NULL, 19, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (80, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-26' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (81, NULL, 5, 1, 5, 1, 5, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-19' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (82, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-20' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (83, NULL, 1, 1, 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (84, NULL, 8, 2, 16, 4, 32, 8, NULL, CAST(N'2015-04-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (85, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-04-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (86, NULL, 8, 2, 16, 4, 32, 8, NULL, CAST(N'2015-04-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (87, NULL, 2, 2, 2, 2, 2, 2, NULL, CAST(N'2016-09-01' AS Date), CAST(N'2016-09-01' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (88, NULL, 9, 1, 18, 2, 27, 3, NULL, CAST(N'2015-07-07' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (89, NULL, NULL, NULL, NULL, NULL, 20, 10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (90, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-09' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (91, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2015-12-02' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (92, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-12-02' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (93, NULL, 15, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-05' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (94, NULL, 3, 1, 6, 2, 9, 3, NULL, CAST(N'1915-04-01' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (95, NULL, 5, 1, 0, 0, 0, 0, NULL, CAST(N'2015-11-23' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (96, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-20' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (97, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (98, NULL, 9, 1, 18, 2, 27, 3, NULL, CAST(N'2015-04-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (99, NULL, 1, 1, 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (100, NULL, 8, 2, 16, 4, 32, 8, NULL, CAST(N'2015-04-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (101, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (102, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-17' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (103, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-30' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (104, NULL, 1, 1, 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (105, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-22' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (106, NULL, 4, 1, 8, 2, 0, 0, NULL, CAST(N'2015-06-05' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (107, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2014-02-21' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (108, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (109, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-12' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (110, NULL, 1, 1, 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (111, NULL, 2, 1, 4, 2, 6, 3, NULL, CAST(N'2015-10-27' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (112, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2015-04-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (113, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-04-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (114, NULL, 5, 1, 5, 1, 5, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-17' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (115, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (116, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-04-10' AS Date), CAST(N'2016-04-10' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (117, NULL, 9, 1, 18, 2, 0, 0, NULL, CAST(N'2015-05-11' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (118, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-21' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (119, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-03' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (120, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (121, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-08-04' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (122, NULL, 1, 1, 2, 2, 3, 3, NULL, CAST(N'2015-05-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (123, NULL, 4, 1, 8, 2, 0, 0, NULL, CAST(N'2015-04-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (124, NULL, 5, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-05' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (125, NULL, 11, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-24' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (126, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-04-10' AS Date), CAST(N'2016-04-10' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (127, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-05' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (128, NULL, 9, 1, 0, 0, 0, 0, NULL, CAST(N'2015-08-11' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (129, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-07-29' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (130, NULL, 1, 1, 2, 1, 3, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-19' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (131, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-26' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (132, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-02-17' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (133, NULL, 4, 1, 8, 2, 0, 0, NULL, CAST(N'2015-05-13' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (134, NULL, 10, 1, 0, 0, 0, 0, NULL, CAST(N'2014-02-21' AS Date), CAST(N'2014-05-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (135, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (136, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-30' AS Date), CAST(N'2014-07-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (137, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2015-03-31' AS Date), N'Y', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (138, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-28' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (139, NULL, 14, 1, 0, 0, 0, 0, NULL, CAST(N'2014-03-05' AS Date), CAST(N'2014-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (140, NULL, 5, 1, 5, 1, 5, 1, NULL, CAST(N'2016-11-17' AS Date), CAST(N'2016-11-19' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (141, 1, 5, 1, 5, 0, 0, 0, NULL, CAST(N'2014-01-30' AS Date), CAST(N'2014-07-31' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (142, NULL, 4, 1, 0, 0, 0, 0, NULL, CAST(N'2015-06-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (143, NULL, 2, 1, 4, 2, 6, 3, NULL, CAST(N'2015-10-01' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (144, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-04-10' AS Date), CAST(N'2016-04-10' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (145, NULL, 3, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-16' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (146, NULL, 25, 1, 0, 0, 0, 0, NULL, CAST(N'2015-05-06' AS Date), CAST(N'2016-03-31' AS Date), N'Y', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (147, NULL, 5, 1, 0, 0, 0, 0, NULL, CAST(N'2014-01-30' AS Date), CAST(N'2014-07-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (148, NULL, 5, 1, 0, 0, 0, 0, NULL, CAST(N'2015-03-05' AS Date), CAST(N'2015-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (149, NULL, 1, 1, 2, 2, 3, 3, NULL, CAST(N'2015-05-25' AS Date), CAST(N'2016-03-31' AS Date), N'N', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (150, 42, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2017-05-24' AS Date), CAST(N'16:11:04' AS Time), NULL)
INSERT [dbo].[masterscheme] ([ID], [ProductID], [ProductQuantity1], [SchemeQuantity1], [ProductQuantity2], [SchemeQuantity2], [ProductQuantity3], [SchemeQuantity3], [SchemeDiscountPercent], [StartingDate], [ClosingDate], [IfSchemeClosed], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (151, 15, 2, 2, 2, 2, 2, 1, NULL, CAST(N'2017-06-02' AS Date), CAST(N'2017-06-02' AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[mastershelf] ON 

INSERT [dbo].[mastershelf] ([ShelfID], [ShelfCode], [ShelfDescription], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (4, N'DR-01-02', N'drawer column 01 row 01', CAST(N'2021-09-15' AS Date), CAST(N'20:06:05' AS Time), N'1', CAST(N'2022-05-15' AS Date), CAST(N'23:43:54' AS Time), N'1')
INSERT [dbo].[mastershelf] ([ShelfID], [ShelfCode], [ShelfDescription], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, N'DR-01-01', N'', CAST(N'2022-05-15' AS Date), CAST(N'23:35:26' AS Time), N'1', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[mastershelf] OFF
GO
INSERT [dbo].[mastertransactiontype] ([TransactionTypeId], [TransactionTypeName]) VALUES (1, N'Cash')
INSERT [dbo].[mastertransactiontype] ([TransactionTypeId], [TransactionTypeName]) VALUES (2, N'Credit')
INSERT [dbo].[mastertransactiontype] ([TransactionTypeId], [TransactionTypeName]) VALUES (3, N'Credit Statment')
GO
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (1, N'KUNAL KADLAG123', N'pune', N'8282828282', N'02532215513', N'nikam.vish@gmail.com', NULL, NULL, NULL, NULL)
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (2, N'VISHAL NIIKAM', N'nashik', N'8275273474', N'02532215513', N'kunal@gmail.com', NULL, NULL, NULL, NULL)
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (3, N'1234567890123456789', N'12345678901234567890', N'9898989898', N'9898989898', N'asssddd', NULL, NULL, CAST(N'2017-03-24' AS Date), CAST(N'12:50:15' AS Time))
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (4, N'12345678901234567890', N'12345678901234567890', N'9898989898', N'9898989898', N'ad@gmial.com', NULL, NULL, NULL, NULL)
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (5, N'1234567890123456789012345678901234567890123456789W', N'1234567890123456789012345678901234567890123456789a', N'9898989898', N'9898989898', N'as@gmail.com', NULL, NULL, CAST(N'2017-03-23' AS Date), CAST(N'11:11:52' AS Time))
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (6, N'SEE111', N'assaa', N'9898989898', N'9898989898', N'ad@gmial.com1', CAST(N'2017-03-22' AS Date), CAST(N'19:07:59' AS Time), CAST(N'2017-03-22' AS Date), CAST(N'19:08:14' AS Time))
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (7, N'SCORG TRAVELS ', N'PUNE', N'8275273474', N'02532215513', N'scorg@gmail.co.in', CAST(N'2017-05-30' AS Date), CAST(N'14:51:50' AS Time), CAST(N'2017-05-30' AS Date), CAST(N'14:52:13' AS Time))
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (8, N'TTTT', NULL, NULL, NULL, NULL, CAST(N'2017-05-30' AS Date), CAST(N'14:57:32' AS Time), NULL, NULL)
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (9, N'HKJBKJKJ', NULL, NULL, NULL, NULL, CAST(N'2017-06-01' AS Date), CAST(N'13:18:37' AS Time), NULL, NULL)
INSERT [dbo].[mastertransport] ([ID], [Name], [TransportAddress], [MobileNumberSMS], [Telephone], [EmailId], [CreatedDate], [CreatedTime], [ModifiedDate], [ModifiedTime]) VALUES (10, N'NAME', N'AKAKAKAK1', N'5455454545', N'4545454545', N'sdfsdf', CAST(N'2017-06-02' AS Date), CAST(N'18:10:30' AS Time), NULL, NULL)
GO
INSERT [dbo].[PaymentMode] ([PayModeID], [PayModeOption]) VALUES (1, N'GooglePay')
INSERT [dbo].[PaymentMode] ([PayModeID], [PayModeOption]) VALUES (2, N'PhonePay')
INSERT [dbo].[PaymentMode] ([PayModeID], [PayModeOption]) VALUES (3, N'RTGS')
INSERT [dbo].[PaymentMode] ([PayModeID], [PayModeOption]) VALUES (4, N'NEFT')
GO
INSERT [dbo].[tblaccountingyear] ([VoucherSeries], [FromDate], [ToDate], [YearEndOver], [CurrentYear]) VALUES (N'1617', N'20160401', N'20170331', N'N', N'N')
INSERT [dbo].[tblaccountingyear] ([VoucherSeries], [FromDate], [ToDate], [YearEndOver], [CurrentYear]) VALUES (N'1718', N'20170401', N'20180331', N'N', N'N')
INSERT [dbo].[tblaccountingyear] ([VoucherSeries], [FromDate], [ToDate], [YearEndOver], [CurrentYear]) VALUES (N'2122', N'20210401', N'20220331', N'N', N'N')
INSERT [dbo].[tblaccountingyear] ([VoucherSeries], [FromDate], [ToDate], [YearEndOver], [CurrentYear]) VALUES (N'2223', N'20220401', N'20230331', N'N', N'Y')
GO
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (1, N'Creditor')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (2, N'Debtor')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (3, N'Bank')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (4, N'General')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (5, N'Other Creditor')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (6, N'Other Debtor')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (7, N'Purchase')
INSERT [dbo].[tblaccounttype] ([AccountTypeID], [AccountType]) VALUES (8, N'Sale')
GO
INSERT [dbo].[tblcollectionnote] ([ID], [VoucherType], [VoucherSeries], [VoucherNumber], [VoucherDate], [SalesmanId], [Amount], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (2, N'CLN', 1718, 2, CAST(N'2017-06-01' AS Date), 1, CAST(200.00 AS Decimal(18, 2)), CAST(N'2017-06-01' AS Date), CAST(N'13:14:32' AS Time), 0, NULL, NULL, 0)
INSERT [dbo].[tblcollectionnote] ([ID], [VoucherType], [VoucherSeries], [VoucherNumber], [VoucherDate], [SalesmanId], [Amount], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (5, N'CLN', 1718, 4, CAST(N'2017-06-01' AS Date), 1, CAST(1899.00 AS Decimal(18, 2)), CAST(N'2017-06-01' AS Date), CAST(N'15:59:30' AS Time), 0, NULL, NULL, 0)
GO
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'26001', N'G', N'CASH ACCOUNT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'24', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30001', N'S', N'CASH SALE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'61', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30002', N'S', N'CREDIT SALE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'62', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30003', N'S', N'CASHCREDIT SALE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'63', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30004', N'S', N'VOUCHER SALE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'61', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30005', N'S', N'SALES RETURN', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'64', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30006', N'S', N'CASH DISCOUNT (SALE)', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'65', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30007', N'S', N'DEBIT NOTE (SALE)', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'66', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30008', N'S', N'ADD ON (SALE)', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'68', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30009', N'S', N'VAT OUTPUT 5 PERCENT ', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30010', N'S', N'VAT OUTPUT 12.5 PERCENT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30011', N'S', N'ROUNDOFF (SALE)', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'67', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30012', N'S', N'SALE FOR ZERO VAT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'69', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30013', N'S', N'PENDING CASH BILLS', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'61', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'30014', N'S', N'ITEM DISCOUNT (SALE)', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'65', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40001', N'P', N'CASH PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'57', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40002', N'P', N'CREDIT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'58', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40003', N'P', N'CASH CREDIT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'59', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40004', N'P', N'PURCHASE RETURN', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'85', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40005', N'P', N'CASH DISCOUNT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'130', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40006', N'P', N'SPECIAL DISCOUNT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'131', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40007', N'P', N'SCHEME DISCOUNT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'132', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40008', N'P', N'ITEM DISCOUNT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'133', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40009', N'P', N'DEBIT NOTE PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'134', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40010', N'P', N'CREDIT NOTE PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'135', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40011', N'P', N'ADD ON  PURCHASE ', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'72', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40012', N'P', N'VAT INPUT 5 PERCENT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'136', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40013', N'P', N'VAT INPUT 12.5 PERCENT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'137', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40014', N'P', N'ROUND OFF PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'138', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40015', N'P', N'PURCHASE FOR ZERO VAT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'139', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40016', N'P', N'FREIGHT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'140', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40017', N'P', N'PURCHASE FOR 5 PERCENT VAT', NULL, NULL, N'140', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'40018', N'P', N'PURCHASE FOR 12.5 PERCENT VAT', NULL, NULL, N'140', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'50001', N'G', N'STOCK IN  OUT', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'50002', N'P', N'STATEMENT PURCHASE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblfixaccounts] ([AccountID], [AccCode], [AccName], [AccOpeningDebit], [AccOpeningCredit], [AccGroupID], [AccDoctorID], [AccAreaID], [AccTransactionDebit], [AccTransactionCredit], [AccClosingDebit], [AccClosingCredit], [AccClearedAmount], [AccLastVoucherNumber], [AccVoucherDate], [AccShortName], [AccNameAddress], [AccBirthDay], [AccBirthMonth], [AccBirthYear], [AccHistory], [AccVATTinNumber], [AccDLN], [AccPAN], [AccEmailID], [AccTokenNumber], [IPartyID], [AccNumber], [AccRemark], [AccBankAccountNumber], [AccDbVisitDay1], [AccDbVisitDay2], [AccDbVisitDay3], [IFFIX], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [AccountCode]) VALUES (N'50003', N'S', N'STATEMENT SALE', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N' ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tblpharmasysdistpluslic] ([EcoMartID], [Data]) VALUES (N'FDE701F79E9143549BA5448C3CDF0182', N'tcinUDjKo31/Lw7JebWJl6rgshR83dpiE0CB37Yk3VpGf326EBMzbKAG+2Iv17Bt63Yxp30Rtgsky44WQ3nvCRQFF+On9s5+9H4yuzuTMUuPQ43+mn+PO/vWU+e90LlnAGSiF4hfMJwFMeFTAT6QuUEUy4YqMPih7IMPouK6WNZRATi1GNomfcdbU1gapYht0RijoLyq+Z0Bzk0mes2SKSWXWEbr9f/YxU7qVkRp5aTrJKCDSQJniDby024rdo0GMYL+bfdMplNIz0outCiy0kvgN27Nn1jGG/pzqUW1Cvi0ArMDFjFQ/tZR3bNBxFXhCv9xmwwiDJqHbVQhbMbIGe/00781ZvFp6oliy9qU3kdHyE0sw37cc5Vl9PBss+MlmrFIwQdz/ovHPQB7JkDiAOsRnbI4SioekdfAhlcNvVWh9oWioimzD45Ia8VZNswu5ZPdtl9D9/lAfAPuFprar+0m6raN0Ll/gyQu8Uxh1+F38GCrg6apfmX7TwmzMX9CS72yFfNTt2DzMwRe0khKcLuIapJ2TXT5Wr71g/DuSKQkgwzGRACavZiK4viG4wag/AArGR5rbz2WJQ8mAkMsxO8Ieg/3FEGtJxrho0b8tF9vHCEkyCm3Zig/gz3kPM6OcPrmTzRxuN+0cdPsTIns2HYKs6yz3F6UtP3aEkPBmmzCO5YAb80H4NuzqRCd8nuoL//Mcn4/ZoAOm12ebhUosN/7GXOALvNGtDlJFYvva1EnFQkYWFIh8f2ubON9E1xuiHn1rT8NIlyM5Anqa9F4JfCJlxn8A8bcWzUt9bZFYoVaX/0UC99cQNJFZloOXNnDl5N3aGd+iDvUTxBWiMcx3PWY87zKsoD9rPBwcFtKJzC2/AvGaOStC/4euj7vRv2LhXOiPtoj9zS9fA/nY8SiUqgCzOegmJo3EsJugJ40ctwVSNLjcwAb08bDJEUTJNbVWYF8KzjmTWSwjb7YU0N3LAgulIyi8rS66T7cEOZ65SbLMhsEoNjGFQf42jSiRFWM1YdEO87KsdyqPbTwE3EibjrlVlHMq8F60/iHRntud+8PrkCJWeT9eXPQ+i5mqosUjpjS1MwayH9uzRchyZIFCd0t+7d9ADrrvUQZFrrVhoQpx+Nk9lAAylBrZkUDo1+MZcxDGc2NehZ3Ht4rBAuDrLY0bKq4qYE27ECHg3AmUTd4tRntx79i39SkfQB6lO0k7geEDSEziRGb/WgrP5fmis/yyzpeObyT5e5j2JCdq63hKAN63/9zwSbgBMGZxDhYBBWFUoz6L9wIDI3R5Css6bbyWc+DEnzWGUXoniQUH9Eox7bqHF+UuFnmkQxdNwb+fhWXJ9HTFPA/8Skewr6v6f7Mo6E8+KmW26xu+0VZ2KA/GHZBpn0U+FTFs3i4jwSi/jkjK7qmSzTUgQRlL+TVFW41xqaaTL8xV47DiXX+M50Gtm+XDYq57djb79+yWp1sLv9pLjkYASnAYuYJeRlGZUCeZWZdSyCO/FLli3w0hfLZk1lbO5u2vomLWCw66+4proAsD0J8r4eTVNUnekgDZhtDebm/b20Atxq1FiVY7UZd/vzZnWtWGLjrEfl5TFkRskZpF/EbKdqBw8nTZQdPwKpm3+9tfn1mBiYo4RAfzAGU3/6blJSxmzt8jDRQgL/3rjGVvKutJaVsUqQY5KoC6s7j8z/aLRmRNWrcCag7QrlWXDYR2iYQBOU13FOoHLTGalaEPF1nvYszkkIXOYt55C/+ckQeQ7jXy+Rvjg/vh/OzFT1in4NqzcNCiSzRzzSrQGLCcVLiXaartLv4Mv8plSJIUCHsqH55le5tjh6RiMp2KN08x143VhHW6xL/R2zFL80TfgujogsOA2cunMxtw3ienVB3I4a+guLJiCI53F4VaziSrCAYH7YWAHzdyH7tCQZSiW9RUk/toVl4FG3aiQ==')
GO
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (11, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (12, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (13, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (14, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (15, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (16, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (17, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (18, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (19, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (20, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (21, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (23, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (24, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (25, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (26, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (27, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (28, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (29, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (30, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (31, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (32, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (33, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (34, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (35, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (36, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (37, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (38, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (39, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (40, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (41, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (42, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (43, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (44, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (45, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (46, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (47, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (48, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (49, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (50, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (51, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (52, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblsettings] ([Id], [setPurchaseCopyPurchaseOrder], [setPurchaseRounding], [setPurchaseReadPurchaseOrder], [setPurchaseChangeSaleRate], [setPurchaseAllowExpiredItems], [setPurchaseHold], [setPurchaseIfPTR], [setSaleRoundOff], [setSaleCreditStatement], [setSaleAllowBackDate], [setSaleTenderAmount], [setSaleShowProfitInSaleBill], [setSaleMaxDiscount], [setSaleGetOrderNumberDate], [setSaleGetDoctor], [setSaleGetDelivaryBoy]) VALUES (2223, N'N', N'N', NULL, N'Y', N'N', N'N', N'N', N'Y', N'Y', N'N', N'N', NULL, CAST(0.00 AS Decimal(18, 2)), N'N', N'N', N'N')
GO
INSERT [dbo].[tblshopdetails] ([ShopID], [ShopOwnerName], [ShopName], [AIOCDACode], [SCORGCode], [Address1], [Address2], [Telephone], [MobileNumber], [EmailID], [SGST], [CGST], [DrugLicNo], [Jurisdication], [CreatedDate]) VALUES (1, N'Akshay  p Lad 122', N'ARYAN PHARMA ,PUNE ', N'jgvbe mnw em', N'herv  e m b', N'pune 123', N'pune 123', N'9898989898', N'98989981155', N'abhijeet.gavali@scorgtechnologies.com', N'SGST', N'hrGST', N'her-7548t7t82', N'jtr-77tt872g42', N'2017-04-07')
GO
SET IDENTITY_INSERT [dbo].[tblstock] ON 

INSERT [dbo].[tblstock] ([StockID], [ProductID], [BatchNumber], [MRP], [Expiry], [ExpiryDate], [TradeRate], [PurchaseRate], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [IFBreakageStock], [OpeningStock], [ClosingStock], [PurchaseStock], [TransferInStock], [CreditNoteStock], [SaleStock], [TransferOutStock], [DebitNoteStock], [PurchaseSchemeStock], [PurchaseReplacementStock], [SaleSchemeStock], [IfRateCorrection], [ProductVATPercent], [PurchaseVATPercent], [LastPurchaseAccountId], [LastPurchaseBillNumber], [LastPurchaseDate], [LastPurchaseVoucherType], [LastPurchaseVoucherNumber], [PriceToRetailer], [ProfitPercent], [Margin], [ScanCode], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [IsHold], [IsLastPusrchseBatch], [LastSaleDate], [PurchaseId], [EcoMartID], [CNFID], [StockistID]) VALUES (34, 2, N'B123', CAST(230.00 AS Decimal(18, 2)), N'09/25', N'20250901', CAST(100.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(230.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, 50, 25, 0, 0, 0, 0, 0, 25, 0, 0, N' ', CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 23, N'44', N'20220715', N'PCR', 19, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 36, NULL, NULL, NULL)
INSERT [dbo].[tblstock] ([StockID], [ProductID], [BatchNumber], [MRP], [Expiry], [ExpiryDate], [TradeRate], [PurchaseRate], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [IFBreakageStock], [OpeningStock], [ClosingStock], [PurchaseStock], [TransferInStock], [CreditNoteStock], [SaleStock], [TransferOutStock], [DebitNoteStock], [PurchaseSchemeStock], [PurchaseReplacementStock], [SaleSchemeStock], [IfRateCorrection], [ProductVATPercent], [PurchaseVATPercent], [LastPurchaseAccountId], [LastPurchaseBillNumber], [LastPurchaseDate], [LastPurchaseVoucherType], [LastPurchaseVoucherNumber], [PriceToRetailer], [ProfitPercent], [Margin], [ScanCode], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [IsHold], [IsLastPusrchseBatch], [LastSaleDate], [PurchaseId], [EcoMartID], [CNFID], [StockistID]) VALUES (35, 3, N'B-234', CAST(45.00 AS Decimal(18, 2)), N'00/00', N'', CAST(25.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, 0, 80, 40, 0, 0, 0, 0, 0, 40, 0, 0, N' ', CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 23, N'44', N'20220715', N'PCR', 19, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 36, NULL, NULL, NULL)
INSERT [dbo].[tblstock] ([StockID], [ProductID], [BatchNumber], [MRP], [Expiry], [ExpiryDate], [TradeRate], [PurchaseRate], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [IFBreakageStock], [OpeningStock], [ClosingStock], [PurchaseStock], [TransferInStock], [CreditNoteStock], [SaleStock], [TransferOutStock], [DebitNoteStock], [PurchaseSchemeStock], [PurchaseReplacementStock], [SaleSchemeStock], [IfRateCorrection], [ProductVATPercent], [PurchaseVATPercent], [LastPurchaseAccountId], [LastPurchaseBillNumber], [LastPurchaseDate], [LastPurchaseVoucherType], [LastPurchaseVoucherNumber], [PriceToRetailer], [ProfitPercent], [Margin], [ScanCode], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [IsHold], [IsLastPusrchseBatch], [LastSaleDate], [PurchaseId], [EcoMartID], [CNFID], [StockistID]) VALUES (36, 2, N'T-876', CAST(230.00 AS Decimal(18, 2)), N'09/25', N'20250901', CAST(150.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), NULL, CAST(150.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, 0, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, N' ', CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 23, N'46', N'20220715', N'PCR', 21, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 37, 2, 0, 0)
INSERT [dbo].[tblstock] ([StockID], [ProductID], [BatchNumber], [MRP], [Expiry], [ExpiryDate], [TradeRate], [PurchaseRate], [SaleRate], [EcoMartRate], [CNFRate], [StockistRate], [IFBreakageStock], [OpeningStock], [ClosingStock], [PurchaseStock], [TransferInStock], [CreditNoteStock], [SaleStock], [TransferOutStock], [DebitNoteStock], [PurchaseSchemeStock], [PurchaseReplacementStock], [SaleSchemeStock], [IfRateCorrection], [ProductVATPercent], [PurchaseVATPercent], [LastPurchaseAccountId], [LastPurchaseBillNumber], [LastPurchaseDate], [LastPurchaseVoucherType], [LastPurchaseVoucherNumber], [PriceToRetailer], [ProfitPercent], [Margin], [ScanCode], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [IsHold], [IsLastPusrchseBatch], [LastSaleDate], [PurchaseId], [EcoMartID], [CNFID], [StockistID]) VALUES (37, 3, N'T-654', CAST(45.00 AS Decimal(18, 2)), N'00/00', N'', CAST(30.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), CAST(35.00 AS Decimal(18, 2)), NULL, CAST(30.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, 0, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, N' ', CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 23, N'46', N'20220715', N'PCR', 21, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 37, 2, 0, 0)
SET IDENTITY_INSERT [dbo].[tblstock] OFF
GO
SET IDENTITY_INSERT [dbo].[tbltrnac] ON 

INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1728, 3, 334, CAST(0.00 AS Decimal(18, 2)), CAST(18.77 AS Decimal(18, 2)), 7, N'2022-06-20', 0, NULL, NULL, N'SCR', N'T', 4, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:28:36', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1729, 3, 335, CAST(0.00 AS Decimal(18, 2)), CAST(18.77 AS Decimal(18, 2)), 7, N'2022-06-20', 0, NULL, NULL, N'SCR', N'T', 4, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:28:36', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1730, 3, 110, CAST(0.44 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'2022-06-20', 0, NULL, NULL, N'SCR', N'T', 4, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:28:36', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1731, 3, 305, CAST(0.00 AS Decimal(18, 2)), CAST(156.45 AS Decimal(18, 2)), 7, N'2022-06-20', 0, NULL, NULL, N'SCR', N'T', 4, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:28:36', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1732, 3, 306, CAST(0.00 AS Decimal(18, 2)), CAST(156.45 AS Decimal(18, 2)), 7, N'2022-06-20', 0, NULL, NULL, N'SCR', N'T', 4, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:28:36', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1733, 3, 7, CAST(350.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'2022-06-20', 0, NULL, NULL, N'SCR', N'T', 4, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:28:36', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1734, 3, 8, CAST(10.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'2022-06-22', NULL, NULL, NULL, N'BKR', NULL, 1, NULL, N'', NULL, N'393934', N'2022-06-22', NULL, 11, NULL, 186, NULL, N'2022-06-22', N'14:24:11', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1735, 3, 7, CAST(0.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 8, N'2022-06-22', NULL, NULL, NULL, N'BKR', NULL, 1, NULL, N'', NULL, N'393934', N'2022-06-22', NULL, 11, NULL, 186, NULL, N'2022-06-22', N'14:24:11', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1736, 4, 8, CAST(20.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'2022-06-22', NULL, NULL, NULL, N'BKR', NULL, 2, NULL, N'DFDF', NULL, N'354466', N'2022-06-22', NULL, 11, NULL, 186, NULL, N'2022-06-22', N'18:57:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1737, 4, 7, CAST(0.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 8, N'2022-06-22', NULL, NULL, NULL, N'BKR', NULL, 2, NULL, N'DFDF', NULL, N'354466', N'2022-06-22', NULL, 11, NULL, 186, NULL, N'2022-06-22', N'18:57:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1738, 4, 301, CAST(0.00 AS Decimal(18, 2)), CAST(420.00 AS Decimal(18, 2)), 7, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1739, 4, 331, CAST(0.00 AS Decimal(18, 2)), CAST(8.19 AS Decimal(18, 2)), 7, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1740, 4, 332, CAST(0.00 AS Decimal(18, 2)), CAST(8.19 AS Decimal(18, 2)), 7, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1741, 4, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.02 AS Decimal(18, 2)), 7, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1742, 4, 302, CAST(0.00 AS Decimal(18, 2)), CAST(163.80 AS Decimal(18, 2)), 7, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1743, 4, 303, CAST(0.00 AS Decimal(18, 2)), CAST(163.80 AS Decimal(18, 2)), 7, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1744, 4, 7, CAST(764.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'2022-06-25', 0, NULL, NULL, N'SCR', N'T', 6, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'18:04:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1745, 3, 2, CAST(1000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'2022-06-25', NULL, NULL, NULL, N'CSP', NULL, 5, NULL, N'DFDF', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'20:03:41', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1746, 3, 1, CAST(0.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 2, N'2022-06-25', NULL, NULL, NULL, N'CSP', NULL, 5, NULL, N'DFDF', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'20:03:41', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1747, 4, 2, CAST(1000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'2022-06-25', NULL, NULL, NULL, N'CSP', NULL, 6, NULL, N'DFDF', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'20:09:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1748, 4, 1, CAST(0.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 2, N'2022-06-25', NULL, NULL, NULL, N'CSP', NULL, 6, NULL, N'DFDF', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'20:09:12', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1749, 5, 2, CAST(500.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, N'2022-06-25', NULL, NULL, NULL, N'CSP', NULL, 7, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'20:09:41', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1750, 5, 1, CAST(0.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), 2, N'2022-06-25', NULL, NULL, NULL, N'CSP', NULL, 7, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-25', N'20:09:41', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1751, 5, 301, CAST(0.00 AS Decimal(18, 2)), CAST(362.25 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 8, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'09:10:23', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1752, 5, 110, CAST(0.25 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 8, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'09:10:23', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1753, 5, 7, CAST(362.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 8, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'09:10:23', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1754, 6, 334, CAST(0.00 AS Decimal(18, 2)), CAST(18.90 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 10, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'13:46:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1755, 6, 335, CAST(0.00 AS Decimal(18, 2)), CAST(18.90 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 10, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'13:46:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1756, 6, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.20 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 10, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'13:46:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1757, 6, 305, CAST(0.00 AS Decimal(18, 2)), CAST(157.50 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 10, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'13:46:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1758, 6, 306, CAST(0.00 AS Decimal(18, 2)), CAST(157.50 AS Decimal(18, 2)), 7, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 10, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'13:46:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1759, 6, 7, CAST(353.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'2022-06-28', 0, NULL, NULL, N'SCR', N'T', 10, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-28', N'13:46:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1760, 7, 301, CAST(0.00 AS Decimal(18, 2)), CAST(766.50 AS Decimal(18, 2)), 7, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1761, 7, 334, CAST(0.00 AS Decimal(18, 2)), CAST(25.20 AS Decimal(18, 2)), 7, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1762, 7, 335, CAST(0.00 AS Decimal(18, 2)), CAST(25.20 AS Decimal(18, 2)), 7, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1763, 7, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.10 AS Decimal(18, 2)), 7, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1764, 7, 305, CAST(0.00 AS Decimal(18, 2)), CAST(210.00 AS Decimal(18, 2)), 7, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1765, 7, 306, CAST(0.00 AS Decimal(18, 2)), CAST(210.00 AS Decimal(18, 2)), 7, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1766, 7, 7, CAST(1237.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'2022-06-29', 0, NULL, NULL, N'SCR', N'T', 12, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2022-06-29', N'07:15:42', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1767, 8, 301, CAST(0.00 AS Decimal(18, 2)), CAST(120.75 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 14, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'10:52:32', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1768, 8, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.25 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 14, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'10:52:32', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1769, 8, 7, CAST(121.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220629', 0, NULL, NULL, N'SCR', N'T', 14, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'10:52:32', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1770, 9, 301, CAST(0.00 AS Decimal(18, 2)), CAST(241.50 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1771, 9, 334, CAST(0.00 AS Decimal(18, 2)), CAST(31.50 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1772, 9, 335, CAST(0.00 AS Decimal(18, 2)), CAST(31.50 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1773, 9, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1774, 9, 305, CAST(0.00 AS Decimal(18, 2)), CAST(262.50 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1775, 9, 306, CAST(0.00 AS Decimal(18, 2)), CAST(262.50 AS Decimal(18, 2)), 7, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1776, 9, 7, CAST(830.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220629', 0, NULL, NULL, N'SCR', N'T', 16, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220629', N'19:35:59', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1777, 10, 301, CAST(0.00 AS Decimal(18, 2)), CAST(1170.75 AS Decimal(18, 2)), 7, N'20220630', 0, NULL, NULL, N'SCR', N'T', 18, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220630', N'11:56:03', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1778, 10, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.25 AS Decimal(18, 2)), 7, N'20220630', 0, NULL, NULL, N'SCR', N'T', 18, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220630', N'11:56:03', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1779, 10, 7, CAST(1171.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220630', 0, NULL, NULL, N'SCR', N'T', 18, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220630', N'11:56:03', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1780, 11, 301, CAST(0.00 AS Decimal(18, 2)), CAST(241.50 AS Decimal(18, 2)), 7, N'20220630', 0, NULL, NULL, N'SCR', N'T', 19, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220630', N'12:02:11', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1781, 11, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220630', 0, NULL, NULL, N'SCR', N'T', 19, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220630', N'12:02:11', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1782, 11, 7, CAST(242.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220630', 0, NULL, NULL, N'SCR', N'T', 19, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220630', N'12:02:11', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1783, 12, 301, CAST(0.00 AS Decimal(18, 2)), CAST(1291.50 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1784, 12, 334, CAST(0.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1785, 12, 335, CAST(0.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1786, 12, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1787, 12, 305, CAST(0.00 AS Decimal(18, 2)), CAST(525.00 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1788, 12, 306, CAST(0.00 AS Decimal(18, 2)), CAST(525.00 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1789, 12, 7, CAST(2468.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220702', 0, NULL, NULL, N'SCR', N'T', 21, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:09:47', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1790, 13, 301, CAST(0.00 AS Decimal(18, 2)), CAST(3228.75 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1791, 13, 334, CAST(0.00 AS Decimal(18, 2)), CAST(157.50 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1792, 13, 335, CAST(0.00 AS Decimal(18, 2)), CAST(157.50 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1793, 13, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.25 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1794, 13, 305, CAST(0.00 AS Decimal(18, 2)), CAST(1312.50 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1795, 13, 306, CAST(0.00 AS Decimal(18, 2)), CAST(1312.50 AS Decimal(18, 2)), 7, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1796, 13, 7, CAST(6169.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220702', 0, NULL, NULL, N'SCR', N'T', 23, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220702', N'21:32:51', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1797, 14, 301, CAST(0.00 AS Decimal(18, 2)), CAST(241.50 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 25, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'13:58:20', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1798, 14, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 25, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'13:58:20', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1799, 14, 7, CAST(242.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220703', 0, NULL, NULL, N'SCR', N'T', 25, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'13:58:20', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1800, 15, 301, CAST(0.00 AS Decimal(18, 2)), CAST(241.50 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 27, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'14:23:28', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1801, 15, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 27, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'14:23:28', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1802, 15, 7, CAST(242.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220703', 0, NULL, NULL, N'SCR', N'T', 27, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'14:23:28', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1803, 16, 301, CAST(0.00 AS Decimal(18, 2)), CAST(2415.00 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 29, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'20:44:40', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1804, 16, 334, CAST(0.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 29, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'20:44:40', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1805, 16, 335, CAST(0.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 29, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'20:44:40', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1806, 16, 305, CAST(0.00 AS Decimal(18, 2)), CAST(525.00 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 29, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'20:44:40', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1807, 16, 306, CAST(0.00 AS Decimal(18, 2)), CAST(525.00 AS Decimal(18, 2)), 7, N'20220703', 0, NULL, NULL, N'SCR', N'T', 29, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'20:44:40', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1808, 16, 7, CAST(3591.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220703', 0, NULL, NULL, N'SCR', N'T', 29, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220703', N'20:44:40', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1809, 17, 301, CAST(0.00 AS Decimal(18, 2)), CAST(241.50 AS Decimal(18, 2)), 7, N'20220704', 0, NULL, NULL, N'SCR', N'T', 31, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220704', N'16:44:52', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1810, 17, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220704', 0, NULL, NULL, N'SCR', N'T', 31, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220704', N'16:44:52', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1811, 17, 7, CAST(242.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220704', 0, NULL, NULL, N'SCR', N'T', 31, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220704', N'16:44:52', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1812, 18, 334, CAST(0.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 33, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'11:47:44', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1813, 18, 335, CAST(0.00 AS Decimal(18, 2)), CAST(63.00 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 33, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'11:47:44', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1814, 18, 305, CAST(0.00 AS Decimal(18, 2)), CAST(525.00 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 33, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'11:47:44', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1815, 18, 306, CAST(0.00 AS Decimal(18, 2)), CAST(525.00 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 33, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'11:47:44', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1816, 18, 7, CAST(1176.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220705', 0, NULL, NULL, N'SCR', N'T', 33, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'11:47:44', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1817, 19, 301, CAST(0.00 AS Decimal(18, 2)), CAST(1207.50 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 35, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'13:21:15', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1818, 19, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.50 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 35, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'13:21:15', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1819, 19, 7, CAST(1208.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220705', 0, NULL, NULL, N'SCR', N'T', 35, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'13:21:15', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1820, 20, 301, CAST(0.00 AS Decimal(18, 2)), CAST(966.00 AS Decimal(18, 2)), 7, N'20220705', 0, NULL, NULL, N'SCR', N'T', 37, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'13:22:00', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1821, 20, 7, CAST(966.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220705', 0, NULL, NULL, N'SCR', N'T', 37, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220705', N'13:22:00', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1822, 21, 334, CAST(0.00 AS Decimal(18, 2)), CAST(49.14 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 39, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'18:39:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1823, 21, 335, CAST(0.00 AS Decimal(18, 2)), CAST(49.14 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 39, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'18:39:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1824, 21, 110, CAST(0.28 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 39, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'18:39:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1825, 21, 305, CAST(0.00 AS Decimal(18, 2)), CAST(409.50 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 39, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'18:39:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1826, 21, 306, CAST(0.00 AS Decimal(18, 2)), CAST(409.50 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 39, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'18:39:31', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1827, 21, 7, CAST(917.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220706', 0, NULL, NULL, N'SCR', N'T', 39, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'18:39:31', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1828, 22, 334, CAST(0.00 AS Decimal(18, 2)), CAST(113.02 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 41, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'19:25:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1829, 22, 335, CAST(0.00 AS Decimal(18, 2)), CAST(113.02 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 41, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'19:25:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1830, 22, 110, CAST(0.00 AS Decimal(18, 2)), CAST(0.26 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 41, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'19:25:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1831, 22, 305, CAST(0.00 AS Decimal(18, 2)), CAST(941.85 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 41, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'19:25:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1832, 22, 306, CAST(0.00 AS Decimal(18, 2)), CAST(941.85 AS Decimal(18, 2)), 7, N'20220706', 0, NULL, NULL, N'SCR', N'T', 41, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'19:25:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1833, 22, 7, CAST(2110.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220706', 0, NULL, NULL, N'SCR', N'T', 41, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220706', N'19:25:01', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1834, 25, 334, CAST(0.00 AS Decimal(18, 2)), CAST(232.78 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 45, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1835, 25, 335, CAST(0.00 AS Decimal(18, 2)), CAST(232.78 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 45, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1836, 25, 110, CAST(0.32 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 45, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1837, 25, 305, CAST(0.00 AS Decimal(18, 2)), CAST(1939.88 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 45, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1838, 25, 306, CAST(0.00 AS Decimal(18, 2)), CAST(1939.88 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 45, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1839, 25, 7, CAST(4345.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220708', 0, NULL, NULL, N'SCR', N'T', 45, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1840, 26, 334, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 47, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1841, 26, 335, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 47, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1842, 26, 110, CAST(0.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 47, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1843, 26, 305, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 47, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1844, 26, 306, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 47, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1845, 26, 7, CAST(162.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220708', 0, NULL, NULL, N'SCR', N'T', 47, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1846, 27, 334, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 49, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1847, 27, 335, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 49, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1848, 27, 110, CAST(0.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 49, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1849, 27, 305, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 49, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1850, 27, 306, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), 7, N'20220708', 0, NULL, NULL, N'SCR', N'T', 49, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL)
INSERT [dbo].[tbltrnac] ([tblTrnacID], [VoucherID], [AccountID], [Debit], [Credit], [AccAccountID], [TransactionDate], [ReferenceVoucherID], [ReferenceVoucherType], [VoucherSeries], [VoucherType], [VoucherSubType], [VoucherNumber], [VoucherDate], [Narration], [ShortName], [ChequeNumber], [ChequeDate], [ClearedDate], [BankID], [BankName], [BranchID], [BranchName], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID]) VALUES (1851, 27, 7, CAST(162.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 102, N'20220708', 0, NULL, NULL, N'SCR', N'T', 49, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbltrnac] OFF
GO
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (1, N'Admin', N'test', 1, 1, NULL, N'New user', NULL, NULL, NULL, NULL, NULL, NULL, N'9897777777', N'a@gmail.com')
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (11, N'sss', N'123', 0, NULL, NULL, N'dddd', N'2017-04-04', N'19:48:00', NULL, NULL, NULL, NULL, N'66699999999', N's@gmail.com')
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (12, N'Abhijeet1', N'123', 1, NULL, NULL, N'dkssnfn;f', N'2017-04-15', N'14:07:09', NULL, N'2017-04-27', N'15:41:03', NULL, N'98999999999', N'a@gmail.com')
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (13, N'GrAND', N'123', 0, NULL, NULL, N'desss', N'2017-04-27', N'18:48:57', NULL, NULL, NULL, NULL, N'34553333333', N'a@gmial.com')
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (14, N'Admin1', N'abc', 1, NULL, NULL, N'jquery', N'2017-05-10', N'15:44:14', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (15, N'Ajit', N'1234', 1, NULL, NULL, N'hhhbee', N'2017-05-10', N'15:55:03', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (16, N'Ajit1', N'1234', 1, NULL, NULL, N'hhhbee', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (17, N'AJITG', N'123', 1, NULL, NULL, N'wweee', N'2017-05-10', N'16:37:10', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (21, N'nagraj', N'nag123', 1, NULL, NULL, N'233', N'2017-05-23', N'11:43:31', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (23, N'Vishal', N'vish123', 1, NULL, NULL, N'test', N'2017-05-30', N'11:49:38', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (25, N'hanumant', N'hanu123', 1, NULL, NULL, N'hanumant', N'2017-05-30', N'11:54:44', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (26, N'kunal', N'kunal123', 1, NULL, NULL, N'test', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbluser] ([UserID], [UserName], [Password], [IfInUse], [Level], [makeitdefault], [UserDetails], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [MobileNumber], [EmailID]) VALUES (27, N'vishal1', N'1234567', 1, NULL, NULL, N'jdhfkjfkjkj', N'2017-06-02', N'18:39:34', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tblvat] ([ID], [VATPercentage], [VATAmount], [SaleAmount], [PurchaseAmount], [VATActive], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (1, CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblvat] ([ID], [VATPercentage], [VATAmount], [SaleAmount], [PurchaseAmount], [VATActive], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (2, CAST(5.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblvat] ([ID], [VATPercentage], [VATAmount], [SaleAmount], [PurchaseAmount], [VATActive], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (3, CAST(12.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblvat] ([ID], [VATPercentage], [VATAmount], [SaleAmount], [PurchaseAmount], [VATActive], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (4, CAST(18.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblvat] ([ID], [VATPercentage], [VATAmount], [SaleAmount], [PurchaseAmount], [VATActive], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (5, CAST(28.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblvat] ([ID], [VATPercentage], [VATAmount], [SaleAmount], [PurchaseAmount], [VATActive], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (6, CAST(48.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tblvouchernumbers] ([ID], [PurchaseCredit], [PurchaseCashCredit], [PurchaseCash], [PurchaseOrder], [SaleChitNumber], [SaleCash], [SaleCredit], [SaleCashCredit], [SaleChallan], [DebitNote], [CreditNote], [CashReceipt], [BankReceipt], [CashPaid], [BankPaid], [BankExpenses], [CashExpenses], [StockIn], [StockOut], [OpeningStock], [TokenNumber], [CorrectionInRate], [ChequeReturn], [JournalVoucher], [StatementPurchase], [StatementSale], [ContraEntry], [SlipNumber], [Quation], [CreditNoteBreakageExpiry], [CollectionNote], [DeliveryNote]) VALUES (1718, 8, 1, 1, 41, 1, 1, 6, 1, 1, 48, 11, 32, 2, 287, 1, 2, 33, 9, 44, 7, 10, 9, 1, 1, 1, 1, 7, 1, 6, 1, 5, 1)
INSERT [dbo].[tblvouchernumbers] ([ID], [PurchaseCredit], [PurchaseCashCredit], [PurchaseCash], [PurchaseOrder], [SaleChitNumber], [SaleCash], [SaleCredit], [SaleCashCredit], [SaleChallan], [DebitNote], [CreditNote], [CashReceipt], [BankReceipt], [CashPaid], [BankPaid], [BankExpenses], [CashExpenses], [StockIn], [StockOut], [OpeningStock], [TokenNumber], [CorrectionInRate], [ChequeReturn], [JournalVoucher], [StatementPurchase], [StatementSale], [ContraEntry], [SlipNumber], [Quation], [CreditNoteBreakageExpiry], [CollectionNote], [DeliveryNote]) VALUES (2122, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tblvouchernumbers] ([ID], [PurchaseCredit], [PurchaseCashCredit], [PurchaseCash], [PurchaseOrder], [SaleChitNumber], [SaleCash], [SaleCredit], [SaleCashCredit], [SaleChallan], [DebitNote], [CreditNote], [CashReceipt], [BankReceipt], [CashPaid], [BankPaid], [BankExpenses], [CashExpenses], [StockIn], [StockOut], [OpeningStock], [TokenNumber], [CorrectionInRate], [ChequeReturn], [JournalVoucher], [StatementPurchase], [StatementSale], [ContraEntry], [SlipNumber], [Quation], [CreditNoteBreakageExpiry], [CollectionNote], [DeliveryNote]) VALUES (2223, 0, 21, NULL, 64, NULL, NULL, NULL, 49, NULL, 2, 0, 5, 2, 7, NULL, NULL, NULL, NULL, NULL, NULL, 10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[vouchercreditdebitnote] ON 

INSERT [dbo].[vouchercreditdebitnote] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountId], [AmountNet], [AmountClear], [DiscountPer], [DiscountAmount], [RoundingAmount], [Amount], [Narration], [ClearedInID], [ClearedInVoucherSeries], [ClearedInVoucherType], [ClearedInVoucherNumber], [ClearedInVoucherDate], [ClearedInPurchaseBillNumber], [OperatorID], [Uploaded], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [IsHold], [TransferToAcc], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [GSTI5], [GSTI12], [GSTI18], [GSTI28]) VALUES (42, 2223, N'DNS', 1, N'2022-06-20', 2, CAST(240.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), N'dfdf', NULL, NULL, N'', 0, NULL, NULL, NULL, NULL, N'2022-06-20', N'09:51:58', 1, N'2022-06-20', N'09:59:53', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[vouchercreditdebitnote] ([ID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [AccountId], [AmountNet], [AmountClear], [DiscountPer], [DiscountAmount], [RoundingAmount], [Amount], [Narration], [ClearedInID], [ClearedInVoucherSeries], [ClearedInVoucherType], [ClearedInVoucherNumber], [ClearedInVoucherDate], [ClearedInPurchaseBillNumber], [OperatorID], [Uploaded], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [IsHold], [TransferToAcc], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [GSTI5], [GSTI12], [GSTI18], [GSTI28]) VALUES (43, 2223, N'DNA', 2, N'2022-06-20', 2, CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), N'dfdf', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'2022-06-20', N'21:21:47', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[vouchercreditdebitnote] OFF
GO
SET IDENTITY_INSERT [dbo].[voucherpurchase] ON 

INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (26, 2223, N'PCR', 5, N'20220619', N'DFF', 2, CAST(103262.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(103262.00 AS Decimal(18, 2)), CAST(101000.00 AS Decimal(18, 2)), CAST(1560.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'DFDFDFDFDF', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(23000.00 AS Decimal(18, 2)), CAST(38220.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(38220.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(1911.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(1911.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220620', N'10:37:53', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (27, 2223, N'PCR', 6, N'20220620', N'DFGGG', 2, CAST(8560.00 AS Decimal(18, 2)), CAST(2500.00 AS Decimal(18, 2)), CAST(6060.00 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)), CAST(1200.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(8800.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220620 ', N'09:54:03', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (28, 2223, N'PCR', 7, N'20220620', N'DFDFDF', 2, CAST(194880.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(194880.00 AS Decimal(18, 2)), CAST(174000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'D', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(87000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(87000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(10440.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(10440.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220620', N'21:23:58', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (30, 2223, N'PCR', 9, N'20220628', N'234', 2, CAST(223000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(223000.00 AS Decimal(18, 2)), CAST(223000.00 AS Decimal(18, 2)), CAST(12000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(111000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(6000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(6000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220628', N'09:02:37', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (31, 2223, N'PCR', 10, N'20220629', N'DFDDDDD', 2, CAST(11200.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11200.00 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220629', N'11:33:31', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (32, 2223, N'PCR', 11, N'20220703', N'123', 9, CAST(111000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(111000.00 AS Decimal(18, 2)), CAST(123000.00 AS Decimal(18, 2)), CAST(12000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(111000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220703', N'13:38:05', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (33, 2223, N'PCR', 13, N'20220708', N'DFDF', 16, CAST(257600.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(257600.00 AS Decimal(18, 2)), CAST(230000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(115000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(115000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(13800.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(13800.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220708', N'00:41:48', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (34, 2223, N'PCR', 15, N'20220714', N'44', 23, CAST(7784.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(7784.00 AS Decimal(18, 2)), CAST(6950.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(3475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(3475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(417.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(417.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220714', N'22:48:28', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (35, 2223, N'PCR', 17, N'20220714', N'44', 23, CAST(7784.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(7784.00 AS Decimal(18, 2)), CAST(6950.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(3475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(3475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(417.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(417.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220714', N'22:50:15', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (36, 2223, N'PCR', 19, N'20220715', N'44', 23, CAST(7784.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(7784.00 AS Decimal(18, 2)), CAST(6950.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(3475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(3475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(417.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(417.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220715', N'11:26:44', 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[voucherpurchase] ([purchaseID], [VoucherSeries], [VoucherType], [VoucherNumber], [VoucherDate], [PurchaseBillNumber], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [AmountItemDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [CashDiscountPercentage], [SchemeDiscountPercentage], [SurchargePercentage], [AmountAddOn], [AmountFreight], [AmountExcise], [AmountTOD], [AmountCreditNote], [AmountDebitNote], [DueDate], [Narration], [EntryDate], [RoundUpAmount], [IsHold], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [OperatorID], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [MSCDACodeForAccount]) VALUES (37, 2223, N'PCR', 21, N'20220715', N'46', 23, CAST(5544.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5544.00 AS Decimal(18, 2)), CAST(4950.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'', NULL, CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(2475.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(297.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(297.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'20220715', N'12:43:19', 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[voucherpurchase] OFF
GO
SET IDENTITY_INSERT [dbo].[vouchersale] ON 

INSERT [dbo].[vouchersale] ([ID], [VoucherType], [VoucherSeries], [VoucherNumber], [CounterSaleNumber], [VoucherDate], [VoucherSubType], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [CashDiscountPercent], [AmountCashDiscount5], [AmountCashDiscount12point5], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [AmountPMTDiscount], [AmountItemDiscount], [AddOnFreight], [AmountCreditNote], [AmountDebitNote], [OctroiPercentage], [AmountOctroi], [Narration], [StatementNumber], [StatementID], [DoctorID], [PatientID], [OperatorID], [ScanPrescriptionID], [ScanPrescriptionFileName], [PatientName], [PatientAddress1], [PatientAddress2], [PatientShortName], [Telephone], [DoctorShortName], [OrderNumber], [OrderDate], [IPDOPDCode], [VAT5Per], [AmountVAT5Per], [VAT12Point5Per], [AmountVAT12Point5Per], [AmountForZeroVAT], [RoundingAmount], [DiscountAmountCB], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [CashierCheck], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [VoucherSaleID], [TransactionalCode], [SurchargePercentage], [LRNumber], [LRDate], [NoOfCases], [AmountSplDicount], [Narration2], [DelivaryBoyId], [TransporterID], [Addper], [SalesManID], [CompanyID], [TotalFinalVat], [NumberOfTimesPrinted], [DeliveryDate], [DeliverySalsmanID], [ISsentByEmail], [AmountCreditnoteTAXFirst], [AmountCreditnoteTAXSecond], [CollectionNoteNumber], [DeliveryNotenumber], [DeliveryNoteDate], [DeliveryorCounter], [AmountCreditNoteStock], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [EcoMartID], [CNFID], [StockistID], [IFDownLoaded]) VALUES (25, N'SCR', 2223, 45, NULL, N'20220708', N'T', 7, CAST(4345.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4345.00 AS Decimal(18, 2)), CAST(3879.75 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'', 0, NULL, 0, NULL, 0, NULL, NULL, N'DEBTOR 1', N'PUNE', N'', NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, CAST(-0.32 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'20220708', N'18:16:45', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1939.88 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(1939.88 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(232.78 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(232.78 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[vouchersale] ([ID], [VoucherType], [VoucherSeries], [VoucherNumber], [CounterSaleNumber], [VoucherDate], [VoucherSubType], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [CashDiscountPercent], [AmountCashDiscount5], [AmountCashDiscount12point5], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [AmountPMTDiscount], [AmountItemDiscount], [AddOnFreight], [AmountCreditNote], [AmountDebitNote], [OctroiPercentage], [AmountOctroi], [Narration], [StatementNumber], [StatementID], [DoctorID], [PatientID], [OperatorID], [ScanPrescriptionID], [ScanPrescriptionFileName], [PatientName], [PatientAddress1], [PatientAddress2], [PatientShortName], [Telephone], [DoctorShortName], [OrderNumber], [OrderDate], [IPDOPDCode], [VAT5Per], [AmountVAT5Per], [VAT12Point5Per], [AmountVAT12Point5Per], [AmountForZeroVAT], [RoundingAmount], [DiscountAmountCB], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [CashierCheck], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [VoucherSaleID], [TransactionalCode], [SurchargePercentage], [LRNumber], [LRDate], [NoOfCases], [AmountSplDicount], [Narration2], [DelivaryBoyId], [TransporterID], [Addper], [SalesManID], [CompanyID], [TotalFinalVat], [NumberOfTimesPrinted], [DeliveryDate], [DeliverySalsmanID], [ISsentByEmail], [AmountCreditnoteTAXFirst], [AmountCreditnoteTAXSecond], [CollectionNoteNumber], [DeliveryNotenumber], [DeliveryNoteDate], [DeliveryorCounter], [AmountCreditNoteStock], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [EcoMartID], [CNFID], [StockistID], [IFDownLoaded]) VALUES (26, N'SCR', 2223, 47, NULL, N'20220708', N'T', 7, CAST(162.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(162.00 AS Decimal(18, 2)), CAST(144.90 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'', 0, NULL, 0, NULL, 0, NULL, NULL, N'DEBTOR 1', N'PUNE', N'', NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, CAST(-0.30 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'20220708', N'18:32:56', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[vouchersale] ([ID], [VoucherType], [VoucherSeries], [VoucherNumber], [CounterSaleNumber], [VoucherDate], [VoucherSubType], [AccountID], [AmountNet], [AmountClear], [AmountBalance], [AmountGross], [CashDiscountPercent], [AmountCashDiscount5], [AmountCashDiscount12point5], [AmountSpecialDiscount], [AmountSchemeDiscount], [AmountCashDiscount], [AmountPMTDiscount], [AmountItemDiscount], [AddOnFreight], [AmountCreditNote], [AmountDebitNote], [OctroiPercentage], [AmountOctroi], [Narration], [StatementNumber], [StatementID], [DoctorID], [PatientID], [OperatorID], [ScanPrescriptionID], [ScanPrescriptionFileName], [PatientName], [PatientAddress1], [PatientAddress2], [PatientShortName], [Telephone], [DoctorShortName], [OrderNumber], [OrderDate], [IPDOPDCode], [VAT5Per], [AmountVAT5Per], [VAT12Point5Per], [AmountVAT12Point5Per], [AmountForZeroVAT], [RoundingAmount], [DiscountAmountCB], [ProfitInRupees], [ProfitPercentBySaleRate], [ProfitPercentByPurchaseRate], [CashierCheck], [CreatedDate], [CreatedTime], [CreatedUserID], [ModifiedDate], [ModifiedTime], [ModifiedUserID], [ModifiedOperatorID], [VoucherSaleID], [TransactionalCode], [SurchargePercentage], [LRNumber], [LRDate], [NoOfCases], [AmountSplDicount], [Narration2], [DelivaryBoyId], [TransporterID], [Addper], [SalesManID], [CompanyID], [TotalFinalVat], [NumberOfTimesPrinted], [DeliveryDate], [DeliverySalsmanID], [ISsentByEmail], [AmountCreditnoteTAXFirst], [AmountCreditnoteTAXSecond], [CollectionNoteNumber], [DeliveryNotenumber], [DeliveryNoteDate], [DeliveryorCounter], [AmountCreditNoteStock], [AmountGST0], [AmountGSTS5], [AmountGSTS12], [AmountGSTS18], [AmountGSTS28], [AmountGSTS48], [AmountGSTC5], [AmountGSTC12], [AmountGSTC18], [AmountGSTC28], [AmountGSTC48], [GSTS5], [GSTS12], [GSTS18], [GSTS28], [GSTS48], [GSTC5], [GSTC12], [GSTC18], [GSTC28], [GSTC48], [AmountGSTI5], [AmountGSTI12], [AmountGSTI18], [AmountGSTI28], [AmountGSTI48], [GSTI5], [GSTI12], [GSTI18], [GSTI28], [GSTI48], [EcoMartID], [CNFID], [StockistID], [IFDownLoaded]) VALUES (27, N'SCR', 2223, 49, NULL, N'20220708', N'T', 7, CAST(162.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(162.00 AS Decimal(18, 2)), CAST(144.90 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, N'', 0, NULL, 0, NULL, 0, NULL, NULL, N'DEBTOR 1', N'PUNE', N'', NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, CAST(-0.30 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'20220708', N'18:41:19', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(72.45 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(8.70 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[vouchersale] OFF
GO
