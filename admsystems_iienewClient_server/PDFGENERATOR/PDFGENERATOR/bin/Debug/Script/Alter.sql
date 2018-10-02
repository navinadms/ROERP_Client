USE [ROTESTDB]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetErrorInfo]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[usp_GetErrorInfo]
AS
SELECT
    ERROR_NUMBER() AS ErrorNumber
    ,ERROR_SEVERITY() AS ErrorSeverity
    ,ERROR_STATE() AS ErrorState
    ,ERROR_PROCEDURE() AS ErrorProcedure
    ,ERROR_LINE() AS ErrorLine
    ,ERROR_MESSAGE() AS ErrorMessage;
GO
/****** Object:  StoredProcedure [dbo].[sp_getCourierData]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[sp_getCourierData]
@criteria as nvarchar(1000)
As

begin

declare @Qry varchar(1500);

set @Qry='select c.* from Courier_Master c (nolock)
inner join AddressSubCategory_Master sm (nolock)
on sm.Pk_AddressSubID=c.FK_SubCategoryID' + @criteria 


exec (@Qry);
end

-- sp_getCourierData ''
GO
/****** Object:  StoredProcedure [dbo].[sp_GetColumns]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[sp_GetColumns]
@Param varchar(100)
as begin
select TABLE_NAME ,COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS
 where TABLE_NAME = @Param
order by ORDINAL_POSITION
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_AddressMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_AddressMaster]
--exec SP_Search_AddressMaster 'Where  Name like ''navin'' '
	@criteria varchar(1000)
AS


declare @Qry varchar(1500);

set @Qry=
			'select Pk_AddressID,Name,EnqNo
			from Address_Master inner join AddressCategory_Master 
			on FK_CategoryID=PK_AddressCategoryID
			inner join AddressSubCategory_Master
			on FK_SubCategoryID=PK_AddressSubID ' + @criteria ;

exec (@Qry);
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForOrder]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SP_Get_AddressForOrder 
Alter PROCEDURE [dbo].[SP_Get_AddressForOrder]    
@criteria nvarchar(max)
AS    
begin   

declare @Qry nvarchar(max);       
set @Qry=''    
  

      
set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo
FROM Address_Master a (nolock)
left join Tbl_OrderOneMaster o (nolock)
on a.Pk_AddressId=o.Fk_AddressId
where a.EnqStatus=1 and a.TypeOfEnq=''OC'' and Convert(varchar(20),o.DispatchDate,112) like ''19000101'''  + @criteria;      
      
exec (@Qry);     
    
  end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForPartyInvoiceDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForPartyInvoiceDetail]    
@criteria nvarchar(max)
AS    
begin   

declare @Qry nvarchar(max);       
set @Qry=''    
  

      
set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo
FROM Address_Master a (nolock)
inner join Tbl_OrderOneMaster o (nolock)
on a.Pk_AddressId=o.Fk_AddressId
inner join Tbl_InvoiceMaster im (nolock)
on a.Pk_AddressId=im.Fk_AddressId
where a.EnqStatus=1 and a.TypeOfEnq=''OC''' + @criteria;      
      
exec (@Qry);     
    
  end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForPartyDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForPartyDetail]      
@criteria nvarchar(max)  
AS      
begin     
  
declare @Qry nvarchar(max);         
set @Qry=''      
    
  
        
set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo  
FROM Address_Master a (nolock)  
inner join Tbl_OrderOneMaster o (nolock)  
on a.Pk_AddressId=o.Fk_AddressId  
inner join Party_Master pm (nolock)  
on a.Pk_AddressId=pm.Fk_AddressId  
  
where a.EnqStatus=1 '  + @criteria;        
        
exec (@Qry);       
      
  end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForServiceCriteria]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForServiceCriteria]            
@criteria nvarchar(max)        
AS            
begin        
declare @Qry nvarchar(max);               
set @Qry=''            
          
         
              
set @Qry='SELECT am.Pk_AddressID,am.EnqNo,am.Name        
FROM Address_Master am (nolock)      
inner join Tbl_ServiceMaster s      
on s.Fk_Address=am.Pk_AddressID            
where am.EnqStatus=EnqStatus' +  @criteria;        
  
exec (@Qry);        
        
        
end        
--SP_Get_AddressForServiceCriteria ''
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_AddressPrintNew]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_AddressPrintNew]
--exec [SP_Search_CourierPrint] '2013-07-1 17:47:23', '2013-8-31 17:47:40', 'a'
	@start datetime,
	@end datetime,
	@criteria varchar(1000)
	
AS
begin

declare @Qry varchar(1500);

set @Qry='select a.Pk_AddressID, a.Name,a.Address+'', '' + a.Area +'', ''+ a.Taluka +'', ''+ a.District + '', '' +a.City +''-''+a.Pincode +'', ''+ a.State as ''Address'', '' - ''+isnull(a.LandlineNo,a.MobileNo) as ''ContactNo''  from Address_Master a (nolock)

where c.CreateDate >= ''' + Convert(varchar,@start,112)  + ''' and c.CreateDate<=  ''' + Convert(varchar,@end,112) + @criteria  +
'order by a.CreateDate desc'

exec (@Qry);

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryAll]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryAll]      
--exec SP_Search_InquiryAll 'and  Name like ''r%'''
 @criteria varchar(1000)
AS 
begin  

declare @Qry varchar(1500);       
set @Qry=''    
    
if(@criteria<>'')
begin

set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull(fd.ByWhom,'''') ''LastFollowBy'',isnull(Convert(varchar(20),fd.F_Date,101),'''') ''LastFDate'',isnull(UserName,'''') ''EnqAllotedTo''  FROM Address_Master am (nolock)
left join Enq_FollowMaster fm (nolock)
on fm.FK_AddressID=am.Pk_AddressID
left join Enq_FollowDetails fd (nolock)
on fd.Fk_AddressID=am.Pk_AddressID
left join Tbl_UserAllotmentDetail ud
on am.Pk_AddressID=ud.Fk_AddressId
left join User_Master um

on um.Pk_UserId=ud.Fk_UserId
where am.EnqStatus=1 '+ @criteria;      
      
exec (@Qry);     
end
else
begin 

set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull(fd.ByWhom,'''') ''LastFollowBy'',isnull(Convert(varchar(20),fd.F_Date,101),'''') ''LastFDate'',isnull(UserName,'''') ''EnqAllotedTo''  FROM Address_Master am (nolock)
left join Enq_FollowMaster fm (nolock)
on fm.FK_AddressID=am.Pk_AddressID
left join Enq_FollowDetails fd (nolock)
on fd.Fk_AddressID=am.Pk_AddressID
left join Tbl_UserAllotmentDetail ud
on am.Pk_AddressID=ud.Fk_AddressId
left join User_Master um

on um.Pk_UserId=ud.Fk_UserId
where am.EnqStatus=1 ';   
exec (@Qry);  
end



    
 
  


end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryReport]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryReport]  
--exec [SP_Search_InquiryReport] ' and  city like ''ahmedabad'' ','2013-03-16', '2013-05-15'  
 @criteria varchar(1000),  
 @start datetime,  
 @end datetime  
AS  
  
declare @Qry varchar(1500);  
  
set @Qry='select Name,Address,MobileNo,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,a.EnqFor,a.EnqDate  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+'''' + @criteria ;  
  
  
  
exec (@Qry)
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryForSalesExec]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryForSalesExec]      
--exec SP_Search_Inquiry 18 '''Where  Name like ''navin'' ''',3     
 @criteria varchar(1000),  
 @User varchar(10)     
AS   
begin
declare @Qry varchar(1500);       
set @Qry=''    

      


set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=am.Pk_AddressID),0) ''Status''
 FROM Address_Master am         

where am.EnqStatus=1 and upper(am.TypeOfEnq)<>''OC''  and upper(am.TypeOfEnq)<>''OL'' and upper(am.TypeOfEnq)<>''REGRET'' and UPPER(am.TypeOfEnq)<>''POSTPOND''  
and Pk_AddressID in (select Fk_AddressID from Tbl_SecondAllotmentDetail where Fk_UserID='+@User+')'+ @criteria;      
      
exec (@Qry);     

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryForCriteria]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryForCriteria]
--exec SP_Search_Order 'and Name like ''%r%'' 
	@criteria varchar(1000)
AS

declare @Qry varchar(1500);

set @Qry=
			'select distinct Pk_AddressID,Name,EnqNo From Address_Master left join Enq_FollowDetails ef
on ef.Fk_AddressID=Pk_AddressID' + @criteria

exec (@Qry);
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_Order]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_Order]
--exec SP_Search_Order 'and Name like ''%r%'' 
	@criteria varchar(1000)
AS

declare @Qry varchar(1500);

set @Qry='select distinct Pk_AddressID,Name,EnqNo From Address_Master left join Enq_FollowDetails ef
on ef.Fk_AddressID=Pk_AddressID
left join  Tbl_OrderOneMaster o
on Pk_AddressID=o.Fk_AddressId
left join Tbl_OrderFollowupDetail f
on Pk_AddressID=f.Fk_AddressId' + @criteria ;

exec (@Qry);
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryReportByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryReportByUser]  
--exec [SP_Search_InquiryReportByUser] ' and  city like ''ahmedabad'' ','2013-03-16', '2013-05-15',1
 @criteria varchar(1000),  
 @start datetime,  
 @end datetime,
 @user int
AS  
  begin
declare @Qry varchar(1500);  
  
set @Qry='select Name,Address,MobileNo,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,a.EnqFor,a.EnqDate  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+'''' + @criteria ;  
  
  
if(@user=0)  
begin  
 exec (@QRY);      
 end  
 else  
 begin   
  set @QRY = @QRY + ' and a.Pk_AddressId in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID ='+convert(varchar(10),@user)+')';
 
 exec (@QRY);  
end    
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllVisitorReport]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllVisitorReport]
--exec [SP_SearchAllInquiryReport] '','','','',''
	@user varchar(1000),
	@salesexec  varchar(1000),
	@status varchar(1000),
	@start datetime,
@end datetime
AS
begin
DECLARE @QRY VARCHAR(max);  
SET @QRY='';  

if (@user <> '')  
  BEGIN  
   SET @QRY='select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS ''No'',am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.Remarks,vd.FollowBy ''FollowBy'',am.Pk_AddressID from Address_Master am
 inner join Enq_VisitorMaster vm
 on am.Pk_AddressID=vm.FK_AddressID
 inner join Enq_VisitorDetail vd
 on vm.FK_AddressID=vd.Fk_AddressID
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and vd.FollowBy in (' + @user + ')' 
END
IF(@salesexec<>'')    
  BEGIN  
   IF(@QRY<>'')  
    BEGIN  
     SET @QRY=@QRY+ ' INTERSECT '  
    END  
   SET @QRY=@QRY+'select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS ''No'',am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.Remarks,vd.FollowBy ''FollowBy'',am.Pk_AddressID from Address_Master am
 inner join Enq_VisitorMaster vm
 on am.Pk_AddressID=vm.FK_AddressID
 inner join Enq_VisitorDetail vd
 on vm.FK_AddressID=vd.Fk_AddressID
where am.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and  vd.SalesExc in (' + @salesexec +')' 
  END  
IF(@status<>'')    
  BEGIN  
   IF(@QRY<>'')  
    BEGIN  
     SET @QRY=@QRY+ ' INTERSECT '
    END  
   SET @QRY=@QRY+'select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS ''No'',am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.Remarks,vd.FollowBy ''FollowBy'',am.Pk_AddressID from Address_Master am
 inner join Enq_VisitorMaster vm
 on am.Pk_AddressID=vm.FK_AddressID
 inner join Enq_VisitorDetail vd
 on vm.FK_AddressID=vd.Fk_AddressID
where am.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and  vd.V_Status in (' + @status + ')' 
  END  


  else
  begin
  
  
 exec (@QRY);  
end
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_OrderForAll]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_OrderForAll]      
--exec SP_Search_InquiryAll 'and  Name like ''r%'''
 @criteria varchar(1000)
AS 
begin  

declare @Qry varchar(1500);       
set @Qry=''    
    


set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull(fd.ByWhom,'''') ''LastFollowBy'',isnull(Convert(varchar(20),fd.FDate,101),'''') ''LastFDate'',isnull(UserName,'''') ''EnqAllotedTo''  FROM Address_Master a (nolock)
left join Tbl_OrderFollowupMaster fm (nolock)
on fm.FK_AddressID=Pk_AddressID
left join Tbl_OrderFollowupDetail fd (nolock)
on fd.Fk_AddressID=Pk_AddressID
left join Tbl_OrderOneMaster o (nolock)
on Pk_AddressId=o.Fk_AddressId
left join Tbl_UserAllotmentDetail ud
on a.Pk_AddressID=ud.Fk_AddressId
left join User_Master um
on um.Pk_UserId=ud.Fk_UserId


where a.EnqStatus=1 and a.TypeOfEnq=''OC'''+ @criteria;      
      
exec (@Qry);     
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_Order_WithAllotment]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_Order_WithAllotment]        
--exec SP_Search_Inquiry 18 '''Where  Name like ''navin'' ''',3       
 @criteria varchar(1000),    
 @User varchar(10)       
AS     
declare @Qry varchar(1500);         
set @Qry=''      
declare @id1 as int;  
declare @id2 as int;  
declare @id3 as int;  
set @id1=(select Pk_UserId from User_Master where UserName='RK')  
set @id2=(select Pk_UserId from User_Master where UserName='SM')  
set @id3=(select Pk_UserId from User_Master where UserName='PP')  
    
    
if(@User='0' or @User=@id1 or @User=@id2 or @User=@id3)        
	begin    
		set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo   
		FROM Address_Master a (nolock)  
		inner join Tbl_OrderOneMaster o (nolock)  
		on a.Pk_AddressId=o.Fk_AddressId  
		where a.EnqStatus=1 '  + @criteria;        
		exec (@Qry);       
	end    
else    
  begin    
  
declare @status as varchar(50)  
declare @team as varchar(50)  
set @status=(select Status from User_Master where Pk_UserId=@User);  
set @team=(Select Fk_TeamId from User_Master where Pk_UserId=@User);  
  
print @status;  
if(@status='Head')  
begin  
  
set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo   
FROM Address_Master a (nolock)  
inner join Tbl_OrderOneMaster o (nolock)  
on a.Pk_AddressId=o.Fk_AddressId  
where a.EnqStatus=1 
 and Pk_AddressID in (select Fk_AddressID  from Tbl_UserAllotmentDetail    
 where Fk_DesignationId in (select Fk_TeamId from User_Master where Fk_TeamId='+@team+'))'+ @criteria;        
        
exec (@Qry);       
end  
else  
begin  
  
set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo   
FROM Address_Master a (nolock)  
inner join Tbl_OrderOneMaster o (nolock)  
on a.Pk_AddressId=o.Fk_AddressId  
where a.EnqStatus=1 
and Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID='''+@User+''')'+ @criteria;        
        
exec (@Qry);       
end  
  
  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllInquiryReportByUserWithCriteria]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllInquiryReportByUserWithCriteria]    
--exec [SP_SearchAllInquiryReportByUser] '','','','','','20120405','20131107','RK'  
@criteria nvarchar(max),
@start datetime,    
@end datetime,  
@User varchar(500)

    
AS    
begin    
DECLARE @QRY VARCHAR(max);      
SET @QRY='';      
    
if (@criteria <> '')      
  BEGIN      
   SET @QRY='select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) ''NO'',Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112) +''''+ @criteria;          
END    

   IF(@QRY='')    
  begin    
if(@User='All' or @User='')
begin  
      select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) as 'NO',Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus   
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
   where a.EnqStatus=1 and  a.EnqDate >=Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112)    
      
   end  
   else   
   begin  
     select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) as 'NO',Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus   
where a.EnqStatus=1 and  a.EnqDate >=Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112)    
   and a.Pk_AddressId in (select Fk_AddressID from Tbl_UserAllotmentDetail ua
 INNER JOIN User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
   where um.UserName in (SELECT value FROM dbo.fn_Split(@User,',')))
    


   end  
   end    
  else    
  begin    
      
if(@User='All' or @User='')  
begin  
 exec (@QRY);      
 end  
 else  
 begin   
-- print @QRY;  
 set @QRY = @QRY + ' and a.Pk_AddressId in (select Fk_AddressID from Tbl_UserAllotmentDetail ua
 INNER JOIN User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
   where um.UserName in (SELECT value FROM dbo.fn_Split('''+@User+''','','')))';      
  --print @QRY;  
 exec (@QRY);  
end    
end  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllInquiryReportByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllInquiryReportByUser]    
--exec [SP_SearchAllInquiryReportByUser] '','','','','','20120405','20131107','RK'  
 @city varchar(1000),    
 @state varchar(1000),    
 @reference varchar(1000),    
 @plant  varchar(1000),    
 @enq varchar(1000),    
 @start datetime,    
@end datetime,  
@User varchar(500),
@subcategory varchar(1000)
    
AS    
begin    
DECLARE @QRY VARCHAR(max);      
SET @QRY='';      
    
if (@city <> '')      
  BEGIN      
   SET @QRY='select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) SRNO,Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and City in (SELECT value FROM dbo.fn_Split('''+@city+''','',''))'
END    
IF(@state<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' UNION '      
    END      
   SET @QRY=@QRY+'select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) SRNO,Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and State in (SELECT value FROM dbo.fn_Split('''+@state+''','',''))'
  END      
IF(@reference<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' UNION '    
    END      
   SET @QRY=@QRY+'select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) SRNO,Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and  Reference in (SELECT value FROM dbo.fn_Split('''+@reference+''','',''))'
  END      
   
   
IF(@subcategory<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' UNION '      
    END      
   SET @QRY=@QRY+'select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) SRNO,Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and asd.SubCategory in (SELECT value FROM dbo.fn_Split('''+@subcategory+''','',''))'
  END     
   
    
IF(@plant<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' UNION '      
    END      
   SET @QRY=@QRY+'select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) SRNO,Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and en.EnqFor in (SELECT value FROM dbo.fn_Split('''+@plant+''','',''))'
  END      
  IF(@enq<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' UNION '      
    END      
   SET @QRY=@QRY+'select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) SRNO,Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus    
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and ty.EnqType in (SELECT value FROM dbo.fn_Split('''+@enq+''','',''))'  
  END      
      
   IF(@QRY='')    
  begin    
if(@User='All' or @User='')
begin  
      select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) as 'SRNO',Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus   
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
   where a.EnqStatus=1 and  a.EnqDate >=Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112)    
      
   end  
   else   
   begin  
     select distinct ROW_NUMBER() OVER(ORDER BY Pk_AddressId) as 'SRNO',Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a    
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID    
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID    
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID    
left join AddressSubCategory_Master asd on a.FK_SubCategoryID=asd.Pk_AddressSubID
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID    
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus   
where a.EnqStatus=1 and  a.EnqDate >=Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112)    
   and a.Pk_AddressId in (select Fk_AddressID from Tbl_UserAllotmentDetail ua
 INNER JOIN User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
   where um.UserName in (SELECT value FROM dbo.fn_Split(@User,',')))
    


   end  
   end    
  else    
  begin    
      
if(@User='All' or @User='')  
begin  
 exec (@QRY);      
 end  
 else  
 begin   
-- print @QRY;  
 set @QRY = @QRY + ' and a.Pk_AddressId in (select Fk_AddressID from Tbl_UserAllotmentDetail ua
 INNER JOIN User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
   where um.UserName in (SELECT value FROM dbo.fn_Split('''+@User+''','',''))';      
  print @QRY;  
 exec (@QRY);  
end    
end  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllInquiryReport]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllInquiryReport]  
--exec [SP_SearchAllInquiryReport] '','','','','','20120405','20130607' 
 @city varchar(1000),  
 @state varchar(1000),  
 @reference varchar(1000),  
 @plant  varchar(1000),  
 @enq varchar(1000),  
 @start datetime,  
@end datetime  
AS  
begin  
DECLARE @QRY VARCHAR(max);    
SET @QRY='';    
  
if (@city <> '')    
  BEGIN    
   SET @QRY='select distinct Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and City in (' + @city + ')'   
END  
IF(@state<>'')      
  BEGIN    
   IF(@QRY<>'')    
    BEGIN    
     SET @QRY=@QRY+ ' INTERSECT '    
    END    
   SET @QRY=@QRY+'select distinct Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and State in (' + @state +')'   
  END    
IF(@reference<>'')      
  BEGIN    
   IF(@QRY<>'')    
    BEGIN    
     SET @QRY=@QRY+ ' INTERSECT '  
    END    
   SET @QRY=@QRY+'select distinct Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and  Reference in (' + @reference + ')'   
  END    
  
IF(@plant<>'')      
  BEGIN    
   IF(@QRY<>'')    
    BEGIN    
     SET @QRY=@QRY+ ' INTERSECT '    
    END    
   SET @QRY=@QRY+'select distinct Name,Address,isnull(MobileNo,LandlineNo)  MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and en.EnqFor in (' + @plant + ')'   
  END    
  IF(@enq<>'')      
  BEGIN    
   IF(@QRY<>'')    
    BEGIN    
     SET @QRY=@QRY+ ' INTERSECT '    
    END    
   SET @QRY=@QRY+'select distinct Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo ''OfferNo'',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a  
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus  
where a.EnqStatus=1 and a.EnqDate >='''+ Convert(varchar,@start,112) +''' and a.EnqDate<='''+ Convert(varchar,@end,112)+''' and ty.EnqType in (' +@enq+ ')'   
  END    
    
   IF(@QRY='')  
  begin  
      select Name,Address,isnull(MobileNo,LandlineNo) MobileNo,City,District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,en.EnqFor as Plant,a.EnqDate,Reference  from Address_Master a 
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID  
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID  
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID  
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID  
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus and  a.EnqDate >=Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112)  
   where a.EnqStatus=1   
   end  
  else  
  begin  
    
    
 exec (@QRY);    
end  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryReportByReferenceByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryReportByReferenceByUser]
--exec [SP_Search_InquiryReport] ' and  city like ''ahmedabad'' ','2013-03-16', '2013-05-15'
@reference varchar(100),
@start datetime,
@end datetime,
@User int
AS
begin

if(@User=0)
begin
select distinct a.Name,a.Address,isnull(a.MobileNo,a.LandlineNo) as MobileNo,a.District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,a.EnqFor,a.EnqDate  from Address_Master a
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus

where a.EnqStatus=1 and a.EnqDate >= Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112) and a.Reference1 like ''+@reference + '%'
end
else
 begin
 select distinct a.Name,a.Address,isnull(a.MobileNo,a.LandlineNo) as MobileNo,a.District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,a.EnqFor,a.EnqDate  from Address_Master a
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus
where a.EnqStatus=1 and a.EnqDate >= Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112) and a.Reference1 like ''+@reference + '%'
and a.Pk_AddressId in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID=convert(varchar(10),@User))
 end
 

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryReportByReference]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryReportByReference]
--exec [SP_Search_InquiryReport] ' and  city like ''ahmedabad'' ','2013-03-16', '2013-05-15'
	@reference varchar(100),
	@start datetime,
	@end datetime
AS
begin
select distinct Name,Address,isnull(MobileNo,LandlineNo) as MobileNo,District,State,a.EnqNo 'OfferNo',ty.EnqType as CustType,st.EnqStatus as EnqType,a.EnqFor,a.EnqDate  from Address_Master a
left join Enq_Type ty on a.FK_EnqTypeID=ty.Pk_EnqTypeID
left join Enq_EnqMaster en on a.Pk_AddressID=en.Fk_AddressID
left join Enq_FollowMaster fm on a.Pk_AddressID=fm.FK_AddressID
left join Enq_FollowDetails fd on a.Pk_AddressID=fd.Fk_AddressID
left join Enq_Status st on a.EnqStatus=st.Pk_EnqStatus

where a.EnqStatus=1 and a.EnqDate >= Convert(varchar,@start,112) and a.EnqDate<=Convert(varchar,@end,112) and Reference like ''+@reference + '%'


end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_InquiryData]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_InquiryData]      
--exec SP_Search_Inquiry 18 '''Where  Name like ''navin'' ''',3     

 @User bigint
AS  
begin 
SELECT Pk_AddressID,Name FROM Address_Master       
WHERE Pk_AddressID in (select Fk_AddressID  from Tbl_UserAllotmentDetail where Fk_UserId=@User ) and EnqStatus=1 

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_Inquiry]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_Inquiry]      
--exec SP_Search_Inquiry 18 '''Where  Name like ''navin'' ''',3     
 @criteria varchar(1000),  
 @User varchar(10)     
AS   
declare @Qry varchar(1500);       
set @Qry=''    
declare @id1 as int;
declare @id2 as int;
declare @id3 as int;
set @id1=(select Pk_UserId from User_Master where UserName='RK')
set @id2=(select Pk_UserId from User_Master where UserName='SM')
set @id3=(select Pk_UserId from User_Master where UserName='PP')
  
  
if(@User='0' or @User=@id1 or @User=@id2 or @User=@id3)      
begin  
  
  
      
set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=am.Pk_AddressID),0) ''Status''
 FROM Address_Master am  (nolock)   
where am.EnqStatus=1 and upper(am.TypeOfEnq)<>''OC''  and upper(am.TypeOfEnq)<>''OL'' and upper(am.TypeOfEnq)<>''REGRET'' and UPPER(am.TypeOfEnq)<>''POSTPOND'''+ @criteria;      
      
exec (@Qry);     
    
  end  
  else  
  begin  
  
  
  

declare @status as varchar(50)
declare @team as varchar(50)
set @status=(select Status from User_Master where Pk_UserId=@User);
set @team=(Select Fk_TeamId from User_Master where Pk_UserId=@User);

print @status;
if(@status='Head')
begin

set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=am.Pk_AddressID),0) ''Status''
  FROM Address_Master am  
where am.EnqStatus=1 and upper(am.TypeOfEnq)<>''OC'' and upper(am.TypeOfEnq)<>''OL'' and upper(am.TypeOfEnq)<>''REGRET'' and UPPER(am.TypeOfEnq)<>''POSTPOND''  
 and Pk_AddressID in (select Fk_AddressID  from Tbl_UserAllotmentDetail  
 where Fk_DesignationId in (select Fk_TeamId from User_Master where Fk_TeamId='+@team+'))'+ @criteria;      
      
exec (@Qry);     
end
else
begin

set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=am.Pk_AddressID),0) ''Status''
 FROM Address_Master am         

where am.EnqStatus=1 and upper(am.TypeOfEnq)<>''OC'' and upper(am.TypeOfEnq)<>''OL'' and upper(am.TypeOfEnq)<>''REGRET'' and UPPER(am.TypeOfEnq)<>''POSTPOND''  
and Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID='''+@User+''')'+ @criteria;      
      
exec (@Qry);     
end


end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_CourierPrintnew]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_CourierPrintnew]
--exec [SP_Search_CourierPrint] '2013-07-1 17:47:23', '2013-8-31 17:47:40', 'a'
	@start datetime,
	@end datetime,
	@type nvarchar(50),
	@criteria varchar(1000)
AS

if(@type='list')
begin
select c.*,sm.SubCategory from Courier_Master c (nolock)
inner join AddressSubCategory_Master sm (nolock)
on sm.Pk_AddressSubID=c.FK_SubCategoryID 
where c.CreateDate >= Convert(varchar,@start,112)  and c.CreateDate<=Convert(varchar,@end,112)
order by c.CourierTh

end
else
begin


declare @Qry varchar(1500);

set @Qry='select c.* from Courier_Master c (nolock)
inner join AddressSubCategory_Master sm (nolock)
on sm.Pk_AddressSubID=c.FK_SubCategoryID
where c.CreateDate >= ''' + Convert(varchar,@start,112)  + ''' and c.CreateDate<=  ''' + Convert(varchar,@end,112) + ''' and c.PrintType= ''' + Convert(varchar(250),@type) + '''' + @criteria  + 
'order by c.Pk_CourierID'

exec (@Qry);


end
 
-- [SP_Search_CourierPrint] '06-SEP-2013' , '06-sEP-2013' , 'Label' , ' '
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_CourierPrint]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_CourierPrint]
--exec [SP_Search_CourierPrint] '2013-07-1 17:47:23', '2013-8-31 17:47:40', 'a'
	@start datetime,
	@end datetime,
	@type nvarchar(50)
AS

if(@type='list')
begin
select c.*,sm.SubCategory from Courier_Master c (nolock)
inner join AddressSubCategory_Master sm (nolock)
on sm.Pk_AddressSubID=c.FK_SubCategoryID 
where c.CreateDate >= Convert(varchar,@start,112)  and c.CreateDate<=Convert(varchar,@end,112)
order by c.CourierTh

end
else
begin
select c.Pk_CourierID,c.Name,c.Address+',' + c.Area +', '+ c.Taluka +', '+ c.District + ',' as 'Address',c.City +'-'+c.Pincode +', ' as 'City', c.State ,  ' - '+isnull(c.ContactNo,'') as 'ContactNo'  from Courier_Master c (nolock)


inner join AddressSubCategory_Master sm (nolock)
on sm.Pk_AddressSubID=c.FK_SubCategoryID
where c.CreateDate >= Convert(varchar,@start,112)  and c.CreateDate<=Convert(varchar,@end,112)and c.PrintType=@type
order by c.Pk_CourierID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_AllOrder_WithAllotment]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_AllOrder_WithAllotment]      
--exec SP_Search_Inquiry 18 '''Where  Name like ''navin'' ''',3     
 @criteria varchar(1000),  
 @User varchar(10)     
AS   
declare @Qry varchar(1500);       
set @Qry=''    
declare @id1 as int;
declare @id2 as int;
declare @id3 as int;
set @id1=(select Pk_UserId from User_Master where UserName='RK')
set @id2=(select Pk_UserId from User_Master where UserName='SM')
set @id3=(select Pk_UserId from User_Master where UserName='PP')
  
  
if(@User='0' or @User=@id1 or @User=@id2 or @User=@id3)      
begin  
  
  
      
set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo 
FROM Address_Master a (nolock)
left join Tbl_OrderOneMaster o (nolock)
on a.Pk_AddressId=o.Fk_AddressId
where a.EnqStatus=1 and a.TypeOfEnq=''OC''' + @criteria;      
         
exec (@Qry);     
    
  end  
  else  
  begin  

declare @status as varchar(50)
declare @team as varchar(50)
set @status=(select Status from User_Master where Pk_UserId=@User);
set @team=(Select Fk_TeamId from User_Master where Pk_UserId=@User);

print @status;
if(@status='Head')
begin

set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo 
FROM Address_Master a (nolock)
left join Tbl_OrderOneMaster o (nolock)
on a.Pk_AddressId=o.Fk_AddressId
where a.EnqStatus=1 and a.TypeOfEnq=''OC'' and Pk_AddressID in (select Fk_AddressID  from Tbl_UserAllotmentDetail  
 where Fk_DesignationId in (select Fk_TeamId from User_Master where Fk_TeamId='+@team+'))'+ @criteria;      
      
exec (@Qry);     
end
else
begin

set @Qry='SELECT distinct Pk_AddressID,a.Name,a.EnqNo 
FROM Address_Master a (nolock)
left join Tbl_OrderOneMaster o (nolock)
on a.Pk_AddressId=o.Fk_AddressId
where a.EnqStatus=1 and a.TypeOfEnq=''OC'' and Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID='''+@User+''')'+ @criteria;      
      
exec (@Qry);     
end


end
GO
/****** Object:  StoredProcedure [dbo].[SP_Search_AddressPrint]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Search_AddressPrint]
--exec [SP_Search_CourierPrint] '2013-07-1 17:47:23', '2013-8-31 17:47:40', 'a'
	@start datetime,
	@end datetime
AS
begin
select a.Pk_AddressID, a.Name,a.Address+', ' + a.Area +', '+ a.Taluka +', '+ a.District + ', ' as 'Address',a.City +'-'+a.Pincode +', ' as 'City', a.State , ' - '+isnull(a.LandlineNo,a.MobileNo) as 'ContactNo'  from Address_Master a (nolock)

where a.CreateDate >= Convert(varchar,@start,112)  and a.CreateDate<=Convert(varchar,@end,112)
order by a.CreateDate desc
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForService]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForService]      
AS      
SELECT distinct Address_Master.*,isnull(vf.SalesExc,'') RecBy,isnull(vf.FollowBy,ef.ByWhom) FollowBy      
FROM Address_Master (nolock)       
left join Enq_FollowDetails ef (nolock)      
on ef.Fk_AddressID=Pk_AddressID      
LEFT JOIN Enq_VisitorDetail vf (nolock)    
on vf.FK_AddressID=Pk_AddressID  
inner join Tbl_OrderOneMaster om (nolock)  
on om.Fk_AddressId=Pk_AddressID    
where Address_Master.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForOrderEmployeeDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForOrderEmployeeDetail]    
@Fk_AddressId bigint 
AS    
SELECT distinct isnull(vf.SalesExc,'') RecBy,isnull(vf.FollowBy,ef.ByWhom) FollowBy    
FROM Address_Master (nolock)     
left join Enq_FollowDetails ef (nolock)    
on ef.Fk_AddressID=Pk_AddressID    
LEFT JOIN Enq_VisitorDetail vf (nolock)  
on vf.FK_AddressID=Pk_AddressID  
where upper(ef.EnqType) like 'OC' or upper(ef.EnqType) like 'ORDER BOOKED'   
or upper(vf.E_Type) like 'OC' or upper(vf.E_Type) like 'ORDER BOOKED'   
and Address_Master.EnqStatus=1 
and  Pk_AddressId=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForOrderEmployee]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForOrderEmployee]    
@Fk_AddressId bigint 
AS    
SELECT distinct top 1 isnull(vf.SalesExc,'') RecBy,vf.V_Date,isnull(isnull(vf.FollowBy,ef.ByWhom),'') FollowBy    
FROM Address_Master (nolock)     
left join Enq_FollowDetails ef (nolock)    
on ef.Fk_AddressID=Pk_AddressID    
LEFT JOIN Enq_VisitorDetail vf (nolock)  
on vf.FK_AddressID=Pk_AddressID  
where Address_Master.EnqStatus=1 and Pk_AddressID=@Fk_AddressId 
order by vf.V_Date desc
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressForOrderAll]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressForOrderAll]    

AS    
SELECT Pk_AddressID,EnqNo,Name,EnqDate
FROM Address_Master (nolock)
where Address_Master.TypeOfEnq='OC' and Address_Master.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressCategory]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressCategory]
	
AS
	
	
	select * from AddressCategory_Master (nolock)
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_User_Permission]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_User_Permission]

           @SoftwareID bigint
           AS
begin

delete from Tbl_PermissionMaster
where Fk_SoftwareID=@SoftwareID
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ServiceMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ServiceMaster]
@Fk_AddressId bigint

AS
Begin

delete from  [dbo].[Tbl_ServiceMaster]

       
where Fk_Address=@Fk_AddressId

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_RIInwardStockMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_RIInwardStockMaster]
 @Pk_RIInwardId bigint
 AS
begin

delete from Tbl_ReInwardMaster
where Pk_ReInwardId=@Pk_RIInwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_RIInwardDetailByInward]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_RIInwardDetailByInward]
@RIInward bigint
AS
begin

delete from [Tbl_RIInwardDetail]
where Fk_ReInwardId=@RIInward
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_RIInwardDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_RIInwardDetail]
@RIInward bigint
AS
begin

delete from [Tbl_RIInwardDetail]
where Pk_RIInwardDetailId=@RIInward
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ProjectInformationMaster_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ProjectInformationMaster_Two]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_ProjectInformationMaster_Two
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ProjectInformationMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ProjectInformationMaster]

           @Fk_AddressId bigint
           
AS
Begin
delete from [Tbl_ProjectInformationMaster]
         
         where [Fk_AddressId]=@Fk_AddressId
     
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ProjectDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ProjectDetail]
@Fk_AddressId bigint
          
AS
Begin
Delete from [Tbl_ProjectDetail]

 where Fk_AddressId=@Fk_AddressId
        

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ProductInstallationMaster_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ProductInstallationMaster_Two]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_ProductInstallationMaster_Two
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_PartyPDCDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_PartyPDCDetail]
@Fk_AddressId bigint
         
AS
Begin
Delete From [Tbl_PartyPDCDetail]
where [Fk_AddressId]=@Fk_AddressId
          


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_PartyCFormDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_PartyCFormDetail]
@Fk_AddressId bigint
        
AS
Begin

Delete from [Tbl_PartyCFormDetail]
           where [Fk_AddressId]=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_PackagingMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_PackagingMaster]
@Fk_AddressId bigint
AS
Begin

delete from [Tbl_PackagingDetail]
where [Fk_AddressId]=@Fk_AddressId
 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OutwardMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OutwardMaster]
 
           @Pk_OutwardId bigint
           AS
begin
delete from [Tbl_OutwardMaster]

         
        where Pk_OutwardId=@Pk_OutwardId
     

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OutwardDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OutwardDetail]

@outwardId bigint        
AS
begin
delete from [Tbl_OutwardDetail]
     where Fk_OutwardId=@outwardId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderVisitorDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderVisitorDetail]

           @Fk_AddressId bigint
AS
Begin
delete from [Tbl_OrderVisitorDetail]

           where Fk_AddressId=@Fk_AddressId
    


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderServiceFollowUpDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderServiceFollowUpDetail]
           @Fk_AddressId bigint
As
begin
delete from [Tbl_OrderServiceFollowUpDetail]

       where Fk_AddressId=@Fk_AddressId
  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderPartyMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderPartyMaster]

           @Fk_AddressId bigint

AS
begin

delete from [Tbl_OrderPartyMaster]
 where Fk_AddressId=@Fk_AddressId
           
           end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderOutstandingDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderOutstandingDetail]
@Fk_AddressId bigint
        
AS
begin

Delete from [Tbl_OrderOutstandingDetail]
           where Fk_AddressId=@Fk_AddressId
    
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderOneMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderOneMaster]

          
           @Fk_AddressId bigint
AS
Begin

delete from [Tbl_OrderOneMaster]
where Fk_AddressId=@Fk_AddressId
 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderMaster_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderMaster_Two]

          
           @Fk_AddressId bigint
AS
Begin

delete from [Tbl_OrderMaster_Two]
where [Fk_AddressId]=@Fk_AddressId
 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderFollowupMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderFollowupMaster]
@Fk_AddressId bigint
   
AS
begin
DELETE from  [Tbl_OrderFollowupMaster]

where Fk_AddressId=@Fk_AddressId
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_OrderFollowupDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_OrderFollowupDetail]
@Fk_AddressId bigint
   
AS
begin
DELETE from  [Tbl_OrderFollowupDetail]

where Fk_AddressId=@Fk_AddressId
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_LetterMailComMaster_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_LetterMailComMaster_Two]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_LetterMailComMaster_Two
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_LetterMailComMaster_Detail_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_LetterMailComMaster_Detail_Two]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_LetterMailComMaster_Detail_Two
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ISIProcessMaster_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ISIProcessMaster_Two]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_ISIProcessMaster_Two
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_ISIProcess_DetailMaster_Two]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_ISIProcess_DetailMaster_Two]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_ISIProcess_DetailMaster_Two
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_InwardStockMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_InwardStockMaster]
 @Pk_InwardId bigint
 AS
begin

delete from [Tbl_InwardStockMaster]
where Pk_InwardId=@Pk_InwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_InwardDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_InwardDetail]
@inward bigint
AS
begin

delete from [Tbl_InwardDetail]
where Fk_InwardId=@inward
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_InvoiceMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_InvoiceMaster]
@Fk_AddressId bigint
     
          
           
           AS
begin

Delete from [Tbl_InvoiceMaster]

     
         where Fk_AddressId=@Fk_AddressId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_DocumentMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_DocumentMaster]

@Fk_AddressId bigint
       
           
           AS
begin

Delete from  [Tbl_DocumentMaster]
          where [Fk_AddressId]=@Fk_AddressId
         
      
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_DocumentLog]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_DocumentLog]
@Fk_DocumentId bigint
      
           AS
begin

Delete from [Tbl_DocumentLog]
 where [Fk_DocumentId]=@Fk_DocumentId
        
           
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_DocMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_DocMaster]
@Pk_DocId int
           AS
begin

Delete from  [Tbl_DocMaster]
           where Pk_DocId=@Pk_DocId
           
        
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Tbl_DetailDocument]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Tbl_DetailDocument]
@Fk_DocumentId int

           AS
begin

Delete from [Tbl_DetailDocument]
where [Fk_DocumentId]=@Fk_DocumentId
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_SP_Delete_Enq_VisitorMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_SP_Delete_Enq_VisitorMaster]
@FK_AddressID	int	

AS

delete  
from Enq_VisitorMaster 
where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Service_Address_Info]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Service_Address_Info]  
 @Fk_AddressId bigint  
AS  
delete FROM Service_Address_Info
Where Fk_AddressId=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_RowMaterialMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_RowMaterialMaster]

@Pk_RowMaterialId bigint
As
begin

Delete From [Tbl_RowMaterialMaster]
where Pk_RowMaterialId=@Pk_RowMaterialId 

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_RIInwardDetailById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_RIInwardDetailById]
@Pk_RIInwardDetailId bigint


AS
begin

delete from Tbl_RIInwardDetail
where Pk_RIInwardDetailId=@Pk_RIInwardDetailId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_ProductRegisterMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_ProductRegisterMaster]
@Pk_ProductRegisterId bigint
AS


begin

Delete from [Tbl_ProductRegisterMaster]
where Pk_ProductRegisterId=@Pk_ProductRegisterId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Party_Master]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Party_Master]
@Fk_AddressId bigint

AS
Begin

delete from  [dbo].[Party_Master]

       
where Fk_AddressId=@Fk_AddressId

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Party_DebitDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Party_DebitDetail]  
@Fk_AddressID bigint  
            
  
AS  
Begin  
delete from [dbo].[Party_Debit_Detail]  
   where [Fk_AddressID]=@Fk_AddressID
           
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Party_Debit]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Party_Debit]  
  
@Fk_AddressId bigint  
  
AS  
Begin  
delete From [dbo].[Party_Debit]  
  
    where Fk_AddressId=@Fk_AddressId  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Party_CreditDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Party_CreditDetail]  
@Fk_AddressID bigint  
  
AS  
Begin  
delete from [dbo].[Party_CreditDetail]  
        where [Fk_AddressID]=@Fk_AddressID 
             
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Party_Credit]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Party_Credit]  
  
           @Fk_AddressID bigint  
AS  
Begin  
delete from  [dbo].[Party_Credit]  
   
    where Fk_AddressID=@Fk_AddressID  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_OutwardDetailById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_OutwardDetailById]
@Pk_OutwardDetailId bigint


AS
begin

delete from Tbl_OutwardDetail
where Pk_OutwardDetailId=@Pk_OutwardDetailId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_InwardDetailById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_InwardDetailById]
@Pk_InwardDetailId bigint


AS
begin

delete from Tbl_InwardDetail
where Pk_InwardDetailId=@Pk_InwardDetailId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_WaterMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_WaterMaster]
@FK_AddressID	int	

AS

delete  from Enq_WaterMaster where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_VisitorMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_VisitorMaster]
@FK_AddressID	int	

AS

delete  from Enq_VisitorMaster where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_VisitorDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_VisitorDetails]
@FK_AddressID	int	

AS

delete from Enq_VisitorDetail 
where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_VisitorDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_VisitorDetail]
@FK_AddressID	int	

AS

delete  from Enq_VisitorDetail where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_Main]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_Main]
@FK_AddressID	int	

AS

delete from  Enq_EnqMaster

where Fk_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_FollowMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_FollowMaster]
@FK_AddressID	int	

AS

delete  
from Enq_FollowMaster 
where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_FollowDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_FollowDetails]
@FK_AddressID	int	

AS

delete  from Enq_FollowDetails where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_ClientMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_ClientMaster]
@FK_AddressID	int	

AS

delete  from Enq_ClientMaster where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Enq_BioDataMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Enq_BioDataMaster]
@FK_AddressID	int	

AS

delete  from Enq_BioDataMaster where FK_AddressID=@FK_AddressID
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Courier]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Courier]
@Pk_CourierID bigint


AS
begin

delete from Courier_Master
where Pk_CourierID=@Pk_CourierID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Category]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Category]
@categoryId bigint


AS
begin

delete from Category_Master
where Pk_CategoryID=@categoryId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Delete_Address]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Delete_Address]
@AddressId bigint


AS
begin

delete from Address_Master
where Pk_AddressID=@AddressId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckEnqNo]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_CheckEnqNo]
@EnqNo nvarchar(50)

AS
begin


if not exists(select EnqNo from Address_Master where Address_Master.EnqStatus=1 and EnqNo=@EnqNo)
begin
return 1;

end
else
begin 
return 0;
end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_VisitFollowUp_Summary]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_VisitFollowUp_Summary]
@addressId bigint

AS
Begin



Select  E.*
From
 Enq_VisitorDetail E 
where E.Fk_AddressID =@addressId
End
-- [SP_Rpt_FollowUp_Visit] 4
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_Order_RawMaterialDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_Order_RawMaterialDetail]
@addressId bigint
AS
Begin


select EnqNo,A.DeliveryArea ,A.DeliveryAddress ,A.DeliveryCity,A.DeliveryState ,A.DeliveryDistrict ,A.DeliveryPincode,A.MobileNo as Mobile1 , ContactPerson ,EmailID ,A.Address , A.Area , A.City , A.ContactPerson , A.Name ,A.State,A.Pk_AddressID,
M.ItemName,M.Qty,M.Rate,M.Amount,case M.IsOrderConfirm when 1 then 'Yes' else 'No' end as IsOrderConfirm
,case M.IsPaymentReceived when 1 then 'Yes' else 'No' end as IsPaymentReceived,OrderDate ,DisDate 
from Address_Master A (nolock) 

left join Tbl_OrderRawMaterialDetail M (nolock) on M.Fk_AddressId = A.Pk_AddressID 



where A.Pk_AddressID=@addressId and A.EnqStatus=1
End
-- [SP_Rpt_Order_RawMaterialDetail] 17
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_Get_TodayAllotDataByUsertmp]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Rpt_Get_TodayAllotDataByUsertmp]      

--SP_Rpt_Get_TodayAllotDataByUsertmp ''1'',''2'',''3'',''5''','''6'',''7'',''8'',''9'','20130101','20120101'
@byUser varchar(100),      
@Start datetime,    
@End datetime    
AS    

begin
     print @byUser;
  select addr.Pk_AddressID,addr.Name,addr.EnqNo,addr.EnqDate,addr.City,addr.Reference1,addr.Reference2,um.UserName from dbo.Address_Master as addr
  inner join Tbl_UserAllotmentDetail ua 
  on addr.Pk_AddressID=ua.Fk_AddressId  
  inner join User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
where addr.EnqStatus=1 and 
convert(varchar,ua.CreateDate,112) between convert(varchar,@Start,112) 
and  convert(varchar,@End,112)       
and um.Pk_UserId in (@byUser)





--select * from User_Master um where um.Pk_UserId in ('1','2','3')
     
 
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_Get_TodayAllotDataByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Rpt_Get_TodayAllotDataByUser]      

--[SP_Rpt_Get_TodayAllotDataByUser] 'RK,SM','20120101','20131101'
@byUser varchar(500),      
@Start datetime,    
@End datetime    
AS    
begin
if(@byUser='All' or @byUser='')
begin

  select addr.Pk_AddressID,addr.Name,addr.EnqNo,addr.EnqDate,addr.City,addr.Reference1,addr.Reference2,'All' as 'UserName' from dbo.Address_Master as addr
    inner join Tbl_UserAllotmentDetail ua 
  on addr.Pk_AddressID=ua.Fk_AddressId  
  inner join User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
   where addr.EnqStatus=1 
   and convert(varchar,addr.EnqDate,112) 
   between convert(varchar,@Start,112) and  convert(varchar,@End,112)       

 order by addr.Pk_AddressID desc    
end
else
begin
     
  select addr.Pk_AddressID,addr.Name,addr.EnqNo,addr.EnqDate,addr.City,addr.Reference1,addr.Reference2,um.UserName from dbo.Address_Master as addr
  inner join Tbl_UserAllotmentDetail ua 
  on addr.Pk_AddressID=ua.Fk_AddressId  
  inner join User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
where addr.EnqStatus=1 and 
convert(varchar,addr.EnqDate,112) between convert(varchar,@Start,112) 
and  convert(varchar,@End,112)       
and um.UserName in (SELECT value FROM dbo.fn_Split(@byUser, ','))
end
     
 
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_Get_TodayAllotData]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Rpt_Get_TodayAllotData]      
@byUser int,      
@Start datetime,    
@End datetime    
AS    
begin
if(@byUser=0)
begin

  select addr.Pk_AddressID,addr.Name,addr.EnqNo,addr.EnqDate,addr.City,addr.Reference1,addr.Reference2 from dbo.Address_Master as addr
   where addr.EnqStatus=1 and convert(varchar,addr.EnqDate,112) between convert(varchar,@Start,112) and  convert(varchar,@End,112)       

 order by addr.Pk_AddressID desc    
end
else
begin
     
  select addr.Pk_AddressID,addr.Name,addr.EnqNo,addr.EnqDate,addr.City,addr.Reference1,addr.Reference2 from dbo.Address_Master as addr
  inner join Tbl_UserAllotmentDetail ua 
  on addr.Pk_AddressID=ua.Fk_AddressId  
  inner join User_Master um 
  on um.Pk_UserId=ua.Fk_UserId
  
 where addr.EnqStatus=1 and convert(varchar,ua.CreateDate,112) between convert(varchar,@Start,112) and  convert(varchar,@End,112)       
 and ua.Fk_UserId=@byUser
 order by ua.Fk_AddressID desc    
end      
 
 end
GO
/****** Object:  StoredProcedure [dbo].[Sp_Rpt_FullStockDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_Rpt_FullStockDetail]
@From datetime,
@To datetime
as
begin

SELECT cm.CategoryName ProductCategoryName,rm.RowMaterialName,sp.OpeningStock,
  isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where Convert(varchar(20),ID.CreateDate,112) between  Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112)   and ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)  AS TotInward,
isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where  Convert(varchar(20),od.CreateDate,112) between Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112) and  OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) AS TotOutward,
isnull((select sum(Quantity)from Tbl_ReInwardMaster rm (nolock) inner join Tbl_RIInwardDetail rd (nolock) on rm.Pk_ReInwardId=rd.Fk_ReInwardId where  Convert(varchar(20),rd.CreateDate,112) between Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112) and  rd.Fk_RowMaterialId=sp.Fk_RawMaterialId and rd.Fk_ProductRegisterId=sp.Fk_CategoryId and rm.[Status]=1),0) AS TotReInward,
(sp.OpeningStock+isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where Convert(varchar(20),ID.CreateDate,112) between  Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112)   and ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)) as RemAfterInward,
(sp.OpeningStock-isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where  Convert(varchar(20),od.CreateDate,112) between Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112) and  OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0)) as RemAfterOutward,
(sp.OpeningStock+isnull((select sum(Quantity)from Tbl_ReInwardMaster rm (nolock) inner join Tbl_RIInwardDetail rd (nolock) on rm.Pk_ReInwardId=rd.Fk_ReInwardId where  Convert(varchar(20),rd.CreateDate,112) between Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112) and  rd.Fk_RowMaterialId=sp.Fk_RawMaterialId and rd.Fk_ProductRegisterId=sp.Fk_CategoryId and rm.[Status]=1),0)) as RemAfterReInward
,(isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where Convert(varchar(20),ID.CreateDate,112) between Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112) and ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)+ isnull(InwardStock,0) + OpeningStock) - 
	(isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where Convert(varchar(20),od.CreateDate,112) between Convert(varchar(20),@From,112) and Convert(varchar(20),@To,112) and  OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) + isnull(OutwardStock,0))as LatestCloseStock
	
  FROM [Tbl_Stock_ProductRegister] sp (nolock)
  inner join Tbl_ProductRegisterMaster cm (nolock)
  on sp.Fk_CategoryId=cm.Pk_ProductRegisterId
  inner join Tbl_RowMaterialMaster rm (nolock)
  on rm.Pk_RowMaterialId=sp.Fk_RawMaterialId
  inner join Tbl_InwardDetail ID (nolock) 
  on ID.Fk_ProductRegisterId=sp.Fk_CategoryId and ID.Fk_RowMaterialId=sp.Fk_RawMaterialId
  
  
  

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_Rpt_FullOutwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_Rpt_FullOutwardDetail]
@From datetime,
@To datetime
as
begin

select om.Pk_OutwardId 'Out_No',om.OutwardDate 'Out_Dt',om.CustomerName,om.Remarks,om.EngineerName, Row_Number() OVER (Partition BY om.Pk_OutwardId ORDER BY om.Pk_OutwardId) as 'No',pm.CategoryName,rm.RowMaterialName,od.Quantity,od.Remarks 'Remarks Detail',od.Unit from Tbl_OutwardMaster om
inner join Tbl_OutwardDetail od
on om.Pk_OutwardId=od.Fk_OutwardId
inner join Tbl_ProductRegisterMaster pm
on pm.Pk_ProductRegisterId=od.Fk_ProductRegisterId
inner join Tbl_RowMaterialMaster rm
on rm.Pk_RowMaterialId=od.Fk_RowMaterialId

where om.OutwardDate between @From and @To


end
GO
/****** Object:  StoredProcedure [dbo].[Sp_Rpt_FullInwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_Rpt_FullInwardDetail]
@From datetime,
@To datetime
as
begin

select im.PONo,im.InwardDate 'PO_Dt',im.BillNo,im.BillDate, Row_Number() OVER (Partition BY im.Pk_InwardId ORDER BY im.Pk_InwardId) as 'No',pm.CategoryName,rm.RowMaterialName,id.Quantity,id.Remarks,id.Unit from Tbl_InwardStockMaster im
inner join Tbl_InwardDetail id
on im.Pk_InwardId=id.Fk_InwardId
inner join Tbl_ProductRegisterMaster pm
on pm.Pk_ProductRegisterId=id.Fk_ProductRegisterId
inner join Tbl_RowMaterialMaster rm
on rm.Pk_RowMaterialId=id.Fk_RowMaterialId
where im.InwardDate between @From and @To





end
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_FollowUp_Visit]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_FollowUp_Visit]
@addressId bigint , 
@VisitorDetailID int
AS
Begin

Select  E.* , F.OfferNo , A.*,A.Address+', ' + A.Area +', '+ A.Taluka +', '+ A.District + ', ' as 'AddressAll',A.City +'-'+A.Pincode +', ' as 'City1', A.State , EM.* , V.* , B.*,ec.*
From
 Enq_VisitorDetail E 
  inner  join Address_Master A on A.Pk_AddressID = E.FK_AddressID 
 
 Left  join Enq_FollowMaster F on F.FK_AddressID = E.FK_AddressID 
 Left  join Enq_EnqMaster EM on EM.Fk_AddressID = E.FK_AddressID 
Left  join Enq_VisitorMaster V on V.Fk_AddressID = E.FK_AddressID 
Left  join  Enq_BioDataMaster B on B.FK_AddressID = E.FK_AddressID   
left  join Enq_ClientMaster ec on ec.FK_AddressID=E.FK_AddressID
where E.Fk_AddressID =@addressId
and E.Pk_VisitorDetailID = @VisitorDetailID and A.EnqStatus=1
End
-- [SP_Rpt_FollowUp_Visit] 13 ,59
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_FollowUp_ProjectSummary]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_FollowUp_ProjectSummary]
@addressId bigint
AS
Begin


select EnqNo,A.DeliveryArea ,A.DeliveryAddress ,A.DeliveryCity ,A.DeliveryState,A.DeliveryDistrict ,A.DeliveryPincode ,A.DeliveryCity,A.ContactPerson ,A.EmailID , A.Address , A.Area , A.City , A.ContactPerson , A.MobileNo as MobileNo1 , A.Name ,A.State , PIN.*, o.*  , PD.*   from
Address_Master A 
left join Tbl_ProjectDetail   o on o.Fk_AddressId = A.Pk_AddressID 
left join Tbl_ProjectInformationMaster PIN on PIN.Fk_AddressId = A.Pk_AddressID 
left join Tbl_PackagingDetail PD on PD.Fk_AddressId = A.Pk_AddressID 

where A.Pk_AddressID=@addressId and A.EnqStatus=1
End
-- [SP_Rpt_FollowUp_ProjectSummary] 4
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_FollowUp_ProjectISIProcess]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_FollowUp_ProjectISIProcess]
@addressId bigint
AS
Begin
select EnqNo,A.DeliveryArea ,A.DeliveryAddress ,A.DeliveryCity,A.DeliveryState ,A.DeliveryDistrict ,A.DeliveryPincode, ContactPerson ,EmailID ,A.Address , A.Area , A.City , A.ContactPerson , A.MobileNo , A.Name ,A.State  , M.*, 
ISI.* from
Address_Master A (nolock)

left join Tbl_ISIProcessMaster_Two M (nolock) on M.Fk_AddressId = A.Pk_AddressID 
left join Tbl_ISIProcess_DetailMaster_Two ISI (nolock) on ISI.Fk_AddressId = M.Fk_AddressId

where A.Pk_AddressID=@addressId and A.EnqStatus=1
End
-- exec [SP_Rpt_FollowUp_ProjectISIProcess] 2
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_FollowUp_Order_Service]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_FollowUp_Order_Service]
@addressId bigint
AS
Begin


select EnqNo,OrderNo ,PartyName , EntryDate  ,  OFD.ProjectDetail , OrderDate ,DispatchDate ,PONo ,BrandName ,City , State , District , 
MobileNo , ContactPerson ,EmailID , o.* from Address_Master A 
inner join Tbl_OrderServiceFollowUpDetail  o on o.Fk_AddressId = A.Pk_AddressID 
inner join Tbl_OrderFollowupMaster  OFD on OFD.Fk_AddressId = A.Pk_AddressID 
inner join Tbl_OrderOneMaster OO on OO.Fk_AddressId = A.Pk_AddressID 
where A.Pk_AddressID=@addressId and A.EnqStatus=1



End
-- [SP_Rpt_FollowUp_Order_Service] 4
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_FollowUp_LatterMail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_FollowUp_LatterMail]
@addressId bigint
AS
Begin


select EnqNo,OrderNo ,PartyName , EntryDate  ,  oo.Status as OrderStatus, OFD.ProjectDetail , OrderDate ,DispatchDate ,PONo ,BrandName ,City , State , District , 
MobileNo , ContactPerson ,EmailID , o.* from Address_Master A 
inner join Tbl_LetterMailComMaster_Detail_Two  o on o.Fk_AddressId = A.Pk_AddressID 
inner join Tbl_OrderFollowupMaster  OFD on OFD.Fk_AddressId = A.Pk_AddressID 
inner join Tbl_OrderOneMaster OO on OO.Fk_AddressId = A.Pk_AddressID 
where A.Pk_AddressID=@addressId and A.EnqStatus=1
End
-- [SP_Rpt_FollowUp_LatterMail] 4
GO
/****** Object:  StoredProcedure [dbo].[SP_Rpt_FollowUp]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[SP_Rpt_FollowUp]
@addressId bigint
AS
Begin


select EnqNo,OrderNo ,PartyName , EntryDate  ,  oo.Status as OrderStatus, OFD.ProjectDetail ,
case 
when OrderDate ='1900-01-01 00:00:00.000' then 
	null
else 
OrderDate 
End As OrderDate,
case 
when DispatchDate ='1900-01-01 00:00:00.000' then 
	null
else 
DispatchDate 
End As DispatchDate,

PONo ,BrandName ,City , State , District , 
MobileNo , ContactPerson ,EmailID , o.* from Address_Master A 
inner join Tbl_OrderFollowupDetail  o on o.Fk_AddressId = A.Pk_AddressID 
inner join Tbl_OrderFollowupMaster  OFD on OFD.Fk_AddressId = A.Pk_AddressID 
inner join Tbl_OrderOneMaster OO on OO.Fk_AddressId = A.Pk_AddressID 
where A.Pk_AddressID=@addressId and A.EnqStatus=1
End
-- [SP_Rpt_FollowUp] 4
GO
/****** Object:  StoredProcedure [dbo].[Sp_Rpt_ChangeTypeEnquiry]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_Rpt_ChangeTypeEnquiry]
@From datetime,
@To datetime,
@Old nvarchar(50),
@New nvarchar(50),
@user nvarchar(200)
As
begin

select am.EnqNo,am.Name,Fk_AddressID,t.F_Date,t.EnqType  from 
(
select  MAX(f.F_Date) as F_Date ,f.EnqType,f.Fk_AddressID from Enq_FollowDetails as f
where f.EnqType=@Old and ByWhom in (SELECT value FROM dbo.fn_Split(@user,','))
and convert(varchar,f.F_Date,112) >= convert(varchar,@From,112) and convert(varchar,f.F_Date,112)<= convert(varchar,@To,112)  
group by f.EnqType,f.Fk_AddressID


union 

select  MAX(f.F_Date) as F_Date ,f.EnqType,f.Fk_AddressID from Enq_FollowDetails as f
where f.EnqType=@New and ByWhom in (SELECT value FROM dbo.fn_Split(@user,',')) 
and convert(varchar,f.F_Date,112) >= convert(varchar,@From,112) and convert(varchar,f.F_Date,112)<= convert(varchar,@To,112)  
group by f.EnqType,f.Fk_AddressID) t inner join Address_Master am 

on am.Pk_AddressID=t.Fk_AddressID






end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_Tbl_ServiceMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_Tbl_ServiceMaster]
@EntryNo nvarchar(50),
@ServiceDate datetime,
@Fk_AddressId bigint,
@Status bit
AS
Begin

if  not exists (select * from Tbl_OrderOneMaster where Fk_AddressId=@Fk_AddressId)
begin
	return  -1 
end 

if not exists (select Fk_Address from Tbl_ServiceMaster where Fk_Address=@Fk_AddressId)
	begin

		INSERT INTO [dbo].[Tbl_ServiceMaster]
				   (enqno,Fk_Address,Servicedate,Status)
			 VALUES
				   (@EntryNo,@Fk_AddressId,@ServiceDate,@Status  )

		return Scope_Identity();
	end
else
begin
	update [Tbl_ServiceMaster] set 
		enqno =@EntryNo ,fk_Address =@Fk_AddressId ,servicedate=@ServiceDate ,status=@Status
		where fk_Address=@Fk_AddressId  
	return Scope_Identity();
end

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_Enq_WaterMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_Enq_WaterMaster]
@Pk_WaterID	bigint,
@FK_AddressID	bigint,
@Application	varchar(50),
@TypeofEnq	varchar(50),
@Remarks	varchar(MAX),
@EnqAttandBy	varchar(50),
@SalesExc	varchar(50),
@EnqAllotted	varchar(50),
@CommitVisitIn	datetime,	
@CommitVisitOut	datetime,	
@FinaliseBy  	datetime	
AS
	begin

	if(@Pk_WaterID=0)
	begin
	insert into Enq_WaterMaster
	(
	FK_AddressID,
Application,
TypeofEnq,
Remarks	,
EnqAttandBy,
SalesExc,
EnqAllotted,
CommitVisitIn,
CommitVisitOut,	
FinaliseBy	
	
	)
	values
	(
	@FK_AddressID,
@Application,
@TypeofEnq,
@Remarks	,
@EnqAttandBy,
@SalesExc,
@EnqAllotted,
@CommitVisitIn,
@CommitVisitOut,	
@FinaliseBy
	)
	end
	else
	update  Enq_WaterMaster
	set
	
Application=@Application,
TypeofEnq=@TypeofEnq,
Remarks=@Remarks	,
EnqAttandBy=@EnqAttandBy,
SalesExc=@SalesExc,
EnqAllotted=@EnqAllotted,
CommitVisitIn=@CommitVisitIn,
CommitVisitOut=@CommitVisitOut,	
FinaliseBy=@FinaliseBy
where FK_AddressID=@FK_AddressID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_Enq_VisitorMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_Enq_VisitorMaster]
@Status int,
@FK_AddressID	bigint,
@Incliness	varchar(50),
@Readiness	varchar(50),
@Mkt_Vision	varchar(50),
@ImpPerson	varchar(50),
@Influ	varchar(50),
@Strength	varchar(50),
@Sp_Expect	varchar(50),
@Fin_Strength	varchar(50),
@Mkt_Strength	varchar(50),
@AwtFo	varchar(50),
@Product	varchar(50),
@Land	varchar(50),
@Power	varchar(50),
@VFNo nvarchar(50),
@Construction nvarchar(100)
AS
begin

	if(@Status=0)
	begin
	insert into Enq_VisitorMaster
	(
	FK_AddressID,
	Incliness,
	Readiness,
	Mkt_Vision	,
	ImpPerson,
	Influ,
	Strength,
	Sp_Expect,
	Fin_Strength,
	Mkt_Strength,
	AwtFo,
	Product,
	Land,
	Power,
	VFNo,
	Contruction	
	)
	values
	(
	@FK_AddressID,
	@Incliness,
	@Readiness,
	@Mkt_Vision	,
	@ImpPerson,
	@Influ,
	@Strength,
	@Sp_Expect,
	@Fin_Strength,
	@Mkt_Strength,
	@AwtFo,
	@Product,
	@Land,
	@Power,
	@VFNo,
@Construction
	)
	end
else
	update  Enq_VisitorMaster
	set
	
	Incliness=@Incliness,
	Readiness=@Readiness,
	Mkt_Vision=@Mkt_Vision	,
	ImpPerson=@ImpPerson,
	Influ=@Influ,
	Strength=@Strength,
	Sp_Expect=@Sp_Expect,
	Fin_Strength=@Fin_Strength,
	Mkt_Strength=@Mkt_Strength,
	AwtFo=@AwtFo,
	Product=@Product,
	Land=@Land,
	Power=@Power,
	VFNo=@VFNo,
	Contruction=@Construction	
where FK_AddressID=@FK_AddressID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_insert_Update_Enq_VisitorDetails]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_insert_Update_Enq_VisitorDetails]  
@FK_AddressID bigint ,  
@V_Date  datetime,  
@VFNo varchar (50),
@V_status varchar(50) ,  
@SalesExc varchar(50) ,  
@Remarks varchar(max) ,  
@FollowBy varchar(50) ,  
@Duration varchar(50),   
@E_type  varchar(50),  
@JBVRemarks varchar(max)  
  
AS  
  
  
insert into Enq_VisitorDetail  
  
 (  
 FK_AddressID,  
 V_Date,  
 VFNo,
 V_status,  
 SalesExc,  
 Remarks,  
 FollowBy,  
 Duration,  
 E_type,  
 JBVRemarks  
 )   
values  
 (  
 @FK_AddressID,  
 @V_Date,
 @VFNo,  
 @V_status,  
 @SalesExc,  
 @Remarks,  
 @FollowBy,  
 @Duration,  
 @E_type,  
 @JBVRemarks  
 )  
return scope_identity();
GO
/****** Object:  StoredProcedure [dbo].[SP_insert_Update_Enq_FollowMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_insert_Update_Enq_FollowMaster]
	
@Pk_FollowID	bigint,	
@FK_AddressID	bigint	,
@OfferNo	varchar(50)	,
@OfferDate	datetime	,
@ProductModel	varchar(50)	,
@OfferTime	varchar(9)	,
@CourierBy	varchar(50)	,
@CommValue	varchar(50)	


AS
	
	begin

	if(@Pk_FollowID=0)
	begin
	insert into Enq_FollowMaster
	(

FK_AddressID	,
OfferNo	,
OfferDate		,
ProductModel		,
OfferTime		,
CourierBy	,
CommValue	
)
values
(
@FK_AddressID	,
@OfferNo	,
@OfferDate		,
@ProductModel		,
@OfferTime		,
@CourierBy	,
@CommValue	
)
	end
	else
	update Enq_FollowMaster
	set
	
OfferNo=@OfferNo,
OfferDate=@OfferDate		,
ProductModel=@ProductModel		,
OfferTime=@OfferTime		,
CourierBy=@CourierBy	,
CommValue=@CommValue
where FK_AddressID=@FK_AddressID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_insert_Update_Enq_FollowDetails]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_insert_Update_Enq_FollowDetails]
@FK_AddressID	int	,
@F_Date datetime,
@followup	varchar(50)	,
@N_F_FollowpDate	datetime	,
@Status	varchar(50)	,
@ByWhom	varchar(50)	,
@EnqType	varchar(50),	
@Remarks varchar(max)

AS


insert into Enq_FollowDetails

	(
	FK_AddressID,
	F_Date,
	Followup,
	N_F_FollowpDate,
	Status,
	ByWhom,
	EnqType,
	Remarks
	
	
	) 
values
	(
	@FK_AddressID,
	@F_Date,
	@Followup,
	@N_F_FollowpDate,
	@Status,
	@ByWhom,
	@EnqType,
	@Remarks
	)
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_Enq_EnqMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_Enq_EnqMaster]
	

@Pk_EnqMasterID	bigint,	
@Fk_AddressID	bigint	,
@Reference	varchar(50)	,
@EnqDate DATETIME,
@EnqTime	varchar(9)	,
@EnqFor	varchar(MAX)	,
@PerHr	varchar(50)	,
@PerDay	varchar(50)	,
@PerReg	varchar(50),
@RTDS VARCHAR(50),
@TTDS VARCHAR(50),
@RTH VARCHAR(50),
@TTH VARCHAR(50),
@RPH VARCHAR(50),
@TPH VARCHAR(50)	
AS
	
	begin

	if(@Pk_EnqMasterID=0)
	begin
	insert into Enq_EnqMaster
	(
	Fk_AddressID	,
Reference	,
EnqDate	,
EnqTime		,
EnqFor	,
PerHr		,
PerDay	,
PerReg,
RTDS,
TTDS,
RTH,
TTH,
RPH,
TPH	
	)
	values
	(
	@Fk_AddressID	,
@Reference	,
@EnqDate	,
@EnqTime		,
@EnqFor	,
@PerHr		,
@PerDay	,
@PerReg,
@RTDS,
@TTDS,
@RTH,
@TTH,
@RPH,
@TPH		
	)
	end
	else
	update Enq_EnqMaster
	set 
	
Reference=@Reference	,
EnqDate=@EnqDate	,
EnqTime=@EnqTime		,
EnqFor=@EnqFor	,
PerHr=@PerHr		,
PerDay=@PerDay	,
PerReg=@PerReg,
RTDS=@RTDS,
TTDS=@TTDS,
RTH=@RTH,
TTH=@TTH,
RPH=@RPH,
TPH=@TPH	
where Fk_AddressID=@Fk_AddressID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_Enq_ClientMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_Enq_ClientMaster]

@PK_ClientDetailsID	bigint,
@FK_AddressID	bigint,
@ClientPhoto image,
@EntryDate datetime
	
AS
	
begin

if(@PK_ClientDetailsID=0)
	begin

	insert into Enq_ClientMaster
	(

	FK_AddressID,
	ClientPhoto,
	EntryDate 
	)
	values
	(
	@FK_AddressID,
	@ClientPhoto,	
	@EntryDate 
	)
	end
else
update Enq_ClientMaster
set
FK_AddressID=@FK_AddressID,

ClientPhoto=@ClientPhoto

where FK_AddressID=@FK_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_insert_Update_Enq_ClientBioData]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_insert_Update_Enq_ClientBioData]

@Pk_BioDataID	bigint,
@FK_AddressID	bigint	,
@Photo1	image	,
@Ph1_Value1	varchar(50)	,
@Ph1_Value2	varchar(50)	,
@Ph1_Value3	varchar(50)	,
@Ph1_Value4	varchar(50)	,
@Ph1_Value5	varchar(MAX),
@Ph1_Value6	varchar(50)	,
@Photo2	image	,
@Ph2_Value1	varchar(50)	,
@Ph2_Value2	varchar(50)	,
@Ph2_Value3	varchar(50)	,
@Ph2_Value4	varchar(50)	,
@Ph2_Value5	varchar(MAX),
@Ph2_Value6	varchar(50)	,
@Photo3	image	,
@Ph3_Value1	varchar(50)	,
@Ph3_Value2	varchar(50)	,
@Ph3_Value3	varchar(50)	,
@Ph3_Value4	varchar(50)	,
@Ph3_Value5	varchar(MAX),
@Ph3_Value6	varchar(50),	
@Photo4	image	,
@Ph4_Value1	varchar(50)	,
@Ph4_Value2	varchar(50)	,
@Ph4_Value3	varchar(50)	,
@Ph4_Value4	varchar(50)	,
@Ph4_Value5	varchar(MAX),
@Ph4_Value6	varchar(50)		
AS
begin

if(@Pk_BioDataID=0)
begin
insert into Enq_BioDataMaster
(
FK_AddressID,	
Photo1		,
Ph1_Value1	,
Ph1_Value2		,
Ph1_Value3,
Ph1_Value4	,
Ph1_Value5	,
Ph1_Value6		,
Photo2	,
Ph2_Value1		,
Ph2_Value2		,
Ph2_Value3	,
Ph2_Value4		,
Ph2_Value5	,
Ph2_Value6	,
Photo3	,
Ph3_Value1		,
Ph3_Value2	,
Ph3_Value3	,
Ph3_Value4		,
Ph3_Value5,
Ph3_Value6,
Photo4	,
Ph4_Value1		,
Ph4_Value2	,
Ph4_Value3	,
Ph4_Value4		,
Ph4_Value5,
Ph4_Value6	
)
values
(
@FK_AddressID,	
@Photo1		,
@Ph1_Value1	,
@Ph1_Value2		,
@Ph1_Value3,
@Ph1_Value4	,
@Ph1_Value5	,
@Ph1_Value6		,
@Photo2	,
@Ph2_Value1		,
@Ph2_Value2		,
@Ph2_Value3	,
@Ph2_Value4		,
@Ph2_Value5	,
@Ph2_Value6	,
@Photo3	,
@Ph3_Value1		,
@Ph3_Value2	,
@Ph3_Value3	,
@Ph3_Value4		,
@Ph3_Value5,
@Ph3_Value6,
@Photo4	,
@Ph4_Value1		,
@Ph4_Value2	,
@Ph4_Value3	,
@Ph4_Value4		,
@Ph4_Value5,
@Ph4_Value6
)end
else
update Enq_BioDataMaster
set

	
Photo1=@Photo1		,
Ph1_Value1=@Ph1_Value1	,
Ph1_Value2=@Ph1_Value2		,
Ph1_Value3=@Ph1_Value3,
Ph1_Value4=@Ph1_Value4	,
Ph1_Value5=@Ph1_Value5	,
Ph1_Value6=@Ph1_Value6		,
Photo2=@Photo2	,
Ph2_Value1=@Ph2_Value1		,
Ph2_Value2=@Ph2_Value2		,
Ph2_Value3=@Ph2_Value3	,
Ph2_Value4=@Ph2_Value4		,
Ph2_Value5=@Ph2_Value5	,
Ph2_Value6=@Ph2_Value6	,
Photo3=@Photo3	,
Ph3_Value1=@Ph3_Value1		,
Ph3_Value2=@Ph3_Value2	,
Ph3_Value3=@Ph3_Value3	,
Ph3_Value4=@Ph3_Value4		,
Ph3_Value5=@Ph3_Value5,
Ph3_Value6=@Ph3_Value6,
Photo4=@Photo4	,
Ph4_Value1=@Ph4_Value1		,
Ph4_Value2=@Ph4_Value2	,
Ph4_Value3=@Ph4_Value3	,
Ph4_Value4=@Ph4_Value4		,
Ph4_Value5=@Ph4_Value5,
Ph4_Value6=@Ph4_Value6
where 	FK_AddressID=@FK_AddressID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_CourierMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_CourierMaster]
@Pk_CourierID bigint,
@FK_CategoryID int,
@FK_SubCategoryID int,
@PrintType varchar(50),
@Name varchar(50),
@Address varchar(MAX),
@Area varchar(50),
@City varchar(50),
@Pincode varchar(50),
@Taluka varchar(50),
@District varchar(50),
@State varchar(50),
@ContactPerson varchar(MAX),
@ContactNo varchar(50),
@CourierTh varchar(50),
@DocketNo varchar(50),
@CreateDate datetime,
@RecBy nvarchar(50),
@FollowBy nvarchar(50),
@Remarks nvarchar(max),
@EnqNo nvarchar(50),
@RecDate nvarchar(50)

AS
 begin

 if(@Pk_CourierID=0)
 begin
 insert into Courier_Master
 (

FK_CategoryID ,
FK_SubCategoryID ,
PrintType ,
Name ,
Address ,
Area ,
City ,
Pincode ,
Taluka ,
District ,
State ,
ContactPerson,
ContactNo ,
CourierTh ,
DocketNo ,
CreateDate ,
RecBy,
FollowBy,
Remarks,
EnqNo,
RecDate
 )
 values
 (
 
@FK_CategoryID ,
@FK_SubCategoryID ,
@PrintType ,
@Name ,
@Address ,
@Area ,
@City ,
@Pincode ,
@Taluka ,
@District ,
@State ,
@ContactPerson,
@ContactNo ,
@CourierTh ,
@DocketNo ,
@CreateDate ,
@RecBy,
@FollowBy,
@Remarks,
@EnqNo,
@RecDate
 )
 end
 ELSE
 update Courier_Master
 set

 FK_CategoryID=@FK_CategoryID ,
FK_SubCategoryID=@FK_SubCategoryID ,
PrintType=@PrintType ,
Name=@Name ,
Address=@Address ,
Area=@Area ,
City=@City ,
Pincode=@Pincode ,
Taluka=@Taluka ,
District=@District ,
State=@State ,
ContactPerson=@ContactPerson,
ContactNo=@ContactNo ,
CourierTh=@CourierTh,
DocketNo=@DocketNo,
RecBy=@RecBy,
FollowBy=@FollowBy,
Remarks=@Remarks,
EnqNo=@EnqNo,
RecDate=@RecDate

where Pk_CourierID=@Pk_CourierID
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_AddressSubCategory_Master]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_AddressSubCategory_Master]
	@Pk_AddressSubID	int,
@FK_AddCatID	int,
@SubCategory	varchar(50)
AS
	
	begin

	if(@Pk_AddressSubID=0)
	begin
	insert into AddressSubCategory_Master
	(
	FK_AddCatID,
	SubCategory

	)
	values
	(
	@FK_AddCatID,
	@SubCategory
	)
	end
	else
	update AddressSubCategory_Master
	set 
	FK_AddCatID=@FK_AddCatID,
	SubCategory=@SubCategory
	where Pk_AddressSubID=@Pk_AddressSubID
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_update_AddressCategory_Master]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_update_AddressCategory_Master]
	
	@Pk_AddressCategoryID int,
	@Category varchar(50)

AS
	
	begin
	if (@Pk_AddressCategoryID=0)
	 begin

	 insert into AddressCategory_Master
	 (Category)
	 values
	 (
	 @Category
	 )
	 end
	 else
	 update AddressCategory_Master
	 set 
	 Category=@Category
	 where Pk_AddressCategoryID=@Pk_AddressCategoryID
	 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Update_Address_Master]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Update_Address_Master]
	
@Pk_AddressID	bigint,
@FK_EnqTypeID	int,
@EnqNo	varchar(50),
@FK_CategoryID	int,
@FK_SubCategoryID	int,
@Name	varchar(50),
@Address	varchar(MAX),
@Area	varchar(50),
@City	varchar(50),
@Pincode	varchar(50),
@Taluka	varchar(50),
@District	varchar(50),
@State	varchar(50),

@ContactPerson	varchar(50),
@LandlineNo	varchar(50),
@MobileNo	varchar(50),
@EmailID	varchar(200),
@EmailID1	varchar(200),
@EmailID2	varchar(200),
@Remark	varchar(MAX),
@EnqDate	datetime,
@CreateDate	datetime,
@EnqStatus varchar(50),
@Reference1	varchar(100),
@Reference2	varchar(100),
@TypeOfEnq varchar(50),
@EnqAttendBy varchar(50),
@EnqFor	varchar(100),
@CapacityHour varchar(50),
@DeliveryAddress varchar(MAX),	
@DeliveryArea varchar(50),	
@DeliveryCity varchar(50),	
@DeliveryPincode varchar(50),	
@DeliveryTaluka	varchar(50),	
@DeliveryDistrict varchar(50),
@DeliveryState varchar(50)

AS
begin

if(@Pk_AddressID=0)
begin

if not exists(select EnqNo from Address_Master where EnqNo=@EnqNo)
begin


insert into Address_Master
(

FK_EnqTypeID,
EnqNo,
FK_CategoryID,
FK_SubCategoryID,
Name,
Address,
Area,
City,
Pincode,
Taluka,
District,
State,
ContactPerson,
LandlineNo,
MobileNo,
EmailID,
EmailID1,
EmailID2,
Remark,
EnqDate,
CreateDate,
EnqStatus,Reference1,
Reference2,
TypeOfEnq,
EnqAttendBy,
EnqFor,
CapacityHour,
DeliveryAddress,
DeliveryArea,
DeliveryCity,
DeliveryPincode,
DeliveryTaluka,
DeliveryDistrict,
DeliveryState

)
values
(
@FK_EnqTypeID,
@EnqNo,
@FK_CategoryID,
@FK_SubCategoryID,
@Name,
@Address,
@Area,
@City,
@Pincode,
@Taluka,
@District,
@State,
@ContactPerson,
@LandlineNo,
@MobileNo,

@EmailID,
@EmailID1,
@EmailID2,

@Remark,
@EnqDate,
@CreateDate,
@EnqStatus,
@Reference1,
@Reference2,
@TypeOfEnq,
@EnqAttendBy,
@EnqFor,
@CapacityHour,
@DeliveryAddress,
@DeliveryArea,
@DeliveryCity,
@DeliveryPincode,
@DeliveryTaluka,
@DeliveryDistrict,
@DeliveryState

)

return scope_identity();
end


end
else
update Address_Master
set
FK_EnqTypeID=@FK_EnqTypeID,
EnqNo=@EnqNo,
FK_CategoryID=@FK_CategoryID,
FK_SubCategoryID=@FK_SubCategoryID,
Name=@Name,
Address=@Address,
Area=@Area,
City=@City,
Pincode=@Pincode,
Taluka=@Taluka,
District=@District,
State=@State,
ContactPerson=@ContactPerson,
LandlineNo=@LandlineNo,
MobileNo=@MobileNo,
EmailID=@EmailID,
EmailID1=@EmailID1,
EmailID2=@EmailID2,

Remark=@Remark,
EnqDate=@EnqDate,
EnqStatus=@EnqStatus,
Reference1=@Reference1,
Reference2=@Reference2,
TypeOfEnq=@TypeOfEnq,
EnqAttendBy=@EnqAttendBy,
EnqFor=@EnqFor,
CapacityHour=@CapacityHour,
DeliveryAddress=@DeliveryAddress,
DeliveryArea=@DeliveryArea,
DeliveryCity=@DeliveryCity,
DeliveryPincode=@DeliveryPincode,
DeliveryTaluka=@DeliveryTaluka,
DeliveryDistrict=@DeliveryDistrict,
DeliveryState=@DeliveryState

where Pk_AddressID=@Pk_AddressID
return 0;
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_User_Permission]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_User_Permission]
@Fk_AddressId bigint,


			
			@Fk_SoftwareID int ,
            @Fk_SoftwareDetailID int ,
            @Fk_UserID int ,
            @Type smallint,             
            @AddPermission bit, 
            @UpdatePermission bit,
            @DeletePermission bit ,
            @PrintPermission bit ,
            @CreationDate datetime
           
           

AS
begin
INSERT INTO [Tbl_PermissionMaster]
           ([Fk_SoftwareID]
           ,[Fk_SoftwareDetailID]
           ,[Fk_UserID]
           ,[Type]
           ,[IsAdd]
           ,[IsUpdate]
           ,[IsDelete]
           ,[IsPrint]
           ,[CreationDate])
     VALUES
           (@Fk_SoftwareID
           ,@Fk_SoftwareDetailID
           ,@Fk_UserID
           ,@Type
           ,@AddPermission
           ,@UpdatePermission
           ,@DeletePermission
           ,@PrintPermission
           ,@CreationDate)

          return scope_identity();
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_RIInwardMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_RIInwardMaster]  
@Fk_OutwardId bigint,  
@EngineerName nvarchar(100),  
@ReInwardDate datetime,  
@Remarks nvarchar(max),  
@Status bit  
AS  
begin  

INSERT INTO [Tbl_ReInwardMaster]  
  
([Fk_OutwardId]  
           ,[EngineerName]  
           ,[ReInwardDate]  
           ,[Remarks]  
           ,[Status])  
     VALUES  
           (  
@Fk_OutwardId,  
@EngineerName,  
@ReInwardDate,  
@Remarks,  
@Status)  
  
  
return Scope_Identity();  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_RIInwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_RIInwardDetail]
@Fk_ProductRegisterId bigint,
           @Fk_RowMaterialId bigint,
           @Fk_RIInwardId bigint,
           @Quantity bigint,
           @Unit nvarchar(50),
           @Remarks nvarchar(max)           
           AS
begin

INSERT INTO [Tbl_RIInwardDetail]
           ([Fk_ProductRegisterId]
           ,[Fk_RowMaterialId]
           ,[Fk_ReInwardId]
           ,[Quantity]
           ,[Unit]
           ,[Remarks])
     VALUES
           (@Fk_ProductRegisterId,
           @Fk_RowMaterialId,
           @Fk_RIInwardId,
           @Quantity,
           @Unit,
           @Remarks)
           
           
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_ProjectInformationMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_ProjectInformationMaster_Two]
	@Fk_AddressId bigint,
	@PlantName nvarchar(500),
	@Model nvarchar(50),
	@ProjectName nvarchar(200),
	@Capacity nvarchar(50),
	@PowerAvailable nvarchar(50),
	@PlantShape nvarchar(50),
	@LandArea nvarchar(50),
	@TreatmentScheme nvarchar(max)
As
BEGIN


if not exists (select Fk_AddressId from Tbl_ProjectInformationMaster_Two where Fk_AddressId=@Fk_AddressId)
begin
	 INSERT INTO Tbl_ProjectInformationMaster_Two
		(
		Fk_AddressId,
		PlantName,
		Model,
		ProjectName,
		Capacity,
		PowerAvailable,
		PlantShape,
		LandArea,
		TreatmentScheme
		)
	 VALUES
		(
		@Fk_AddressId,
		@PlantName,
		@Model,
		@ProjectName,
		@Capacity,
		@PowerAvailable,
		@PlantShape,
		@LandArea,
		@TreatmentScheme
		)
		return scope_identity();
		end
		else
		begin
		return -1;
		end
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_ProjectInformationMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_ProjectInformationMaster]  
			@PlantName nvarchar(50),  
           @Model nvarchar(50),  
           @ProjectName nvarchar(200),  
           @Capacity nvarchar(50),  
           @PowerAvailable nvarchar(50),  
           @PlantShape nvarchar(50),  
           @LandArea nvarchar(50),  
           @DType nvarchar(50),  
           @TreatmentScheme nvarchar(max),  
           @JarDis nvarchar(50),  
           @BMouldDis nvarchar(50),  
           @TentativeDispatch datetime,  
           @Fk_AddressId bigint  
             
AS  
Begin  
  
  
if not exists (select Fk_AddressId from Tbl_ProjectInformationMaster where Fk_AddressId=@Fk_AddressId)  
	begin  
			INSERT INTO [Tbl_ProjectInformationMaster]  
           ([PlantName]  
           ,[Model]  
           ,[ProjectName]  
           ,[Capacity]  
           ,[PowerAvailable]  
           ,[PlantShape]  
           ,[LandArea]  
           ,[DType]  
           ,[TreatmentScheme]  
           ,[JarDis]  
           ,[BMouldDis]  
           ,[TentativeDispatch]  
           ,[Fk_AddressId])  
     VALUES  
           (@PlantName,  
           @Model,  
           @ProjectName,  
           @Capacity,  
           @PowerAvailable,  
           @PlantShape,  
           @LandArea,  
           @DType,  
           @TreatmentScheme,  
           @JarDis,  
           @BMouldDis,  
           @TentativeDispatch,  
           @Fk_AddressId)  
             
           return Scope_Identity();  
             
	end  
 else  
      begin  
           Update [Tbl_ProjectInformationMaster]  
           set [PlantName]  =@PlantName
           ,[Model]  =@Model
           ,[ProjectName]  =@ProjectName
           ,[Capacity]  =@Capacity
           ,[PowerAvailable]  =@PowerAvailable
           ,[PlantShape]  =@PlantShape
           ,[LandArea]  =@LandArea
           ,[DType]  =@DType
           ,[TreatmentScheme]  =@TreatmentScheme
           ,[JarDis]  =@JarDis
           ,[BMouldDis]  =@BMouldDis 
           ,[TentativeDispatch] =@TentativeDispatch 
           where 
           [Fk_AddressId]=@Fk_AddressId
           
           return @@ROWCOUNT
      end   
             
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_ProjectDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_ProjectDetail]
@Fk_AddressId bigint,
           @SrNo int,
           @PlantScheme nvarchar(50),
           @VendorName nvarchar(50),
           @DispatchDate datetime
AS
Begin
INSERT INTO [Tbl_ProjectDetail]
           ([Fk_AddressId]
           ,[SrNo]
           ,[PlantScheme]
           ,[VendorName]
           ,[DispatchDate])
     VALUES
           (@Fk_AddressId,
           @SrNo,
           @PlantScheme,
           @VendorName,
           @DispatchDate)
return Scope_identity();
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_ProductInstallationMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_ProductInstallationMaster_Two]
	@Fk_AddressId bigint,
	@PDate datetime,
	@Dis_Date datetime,
	@Product_Name nvarchar(500),
	@Vendor_Name nvarchar(500),
	@Station nvarchar(500),
	@Send_CU_To nvarchar(500),
	@Rec_CU_From nvarchar(500),
	@CU_To_Venue nvarchar(500),
	@Comp_Date_With_Inst datetime,
	@By_Whom nvarchar(500),
	@Remark nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_ProductInstallationMaster_Two
		(
		Fk_AddressId,
		PDate,
		Dis_Date,
		Product_Name,
		Vendor_Name,
		Station,
		Send_CU_To,
		Rec_CU_From,
		CU_To_Venue,
		Comp_Date_With_Inst,
		By_Whom,
		Remark
		)
	 VALUES
		(
		@Fk_AddressId,
		@PDate,
		@Dis_Date,
		@Product_Name,
		@Vendor_Name,
		@Station,
		@Send_CU_To,
		@Rec_CU_From,
		@CU_To_Venue,
		@Comp_Date_With_Inst,
		@By_Whom,
		@Remark
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_PartyPDCDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_PartyPDCDetail]
@Fk_AddressId bigint,
           @PDCDate datetime,
           @Amount numeric(18,2),
           @Status bit
AS
Begin
INSERT INTO [Tbl_PartyPDCDetail]
           ([Fk_AddressId]
           ,[PDCDate]
           ,[Amount]
           ,[Status])
     VALUES
           (@Fk_AddressId,
           @PDCDate,
           @Amount,
           @Status)
     

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_PartyCFormDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_PartyCFormDetail]
@Fk_AddressId bigint,
           @CFormRequired bit,
           @CFormStatus bit,
           @CFormRecDate datetime,
           @CFormNo nvarchar(50),
           @CFormRemarks nvarchar(200)        
AS
Begin


INSERT INTO [Tbl_PartyCFormDetail]
           ([Fk_AddressId]
           ,[CFormRequired]
           ,[CFormStatus]
           ,[CFormRecDate]
           ,[CFormNo]
           ,[CFormRemarks])
     VALUES
           (@Fk_AddressId,
           @CFormRequired,
           @CFormStatus,
           @CFormRecDate,
           @CFormNo,
           @CFormRemarks)
           


 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_PackagingMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_PackagingMaster]
@Bottling nvarchar(50),
           @BottlingType nvarchar(50),
           @BottllingCapacity nvarchar(50),
           @BottlingVendor1 nvarchar(50),
           @BottlingVendor2 nvarchar(50),
           @Pouch nvarchar(50),
           @PouchType nvarchar(50),
           @PouchCapacity nvarchar(50),
           @PouchVendor1 nvarchar(50),
           @PouchVendor2 nvarchar(50),
           @Chiller nvarchar(50),
           @ChillerVendor1 nvarchar(50),
           @ChillerVendor2 nvarchar(50),
           @Compressor nvarchar(50),
           @Lab nvarchar(50),
           @LabVendor1 nvarchar(50),
           @LabVendor2 nvarchar(50),
           @Letter11 nvarchar(30),
           @Letter12 nvarchar(30),
           @Letter21 nvarchar(30),
           @Letter22 nvarchar(30),
           @Letter31 nvarchar(30),
           @Letter32 nvarchar(30),
           @Letter41 nvarchar(30),
           @Letter42 nvarchar(30),
           @Letter51 nvarchar(30),
           @Letter52 nvarchar(30),
           @Remarks nvarchar(max),
           @ProjectType nvarchar(50),
           @DipatchStatus nvarchar(50),
           @DispatchRemarks nvarchar(max),
           @Fk_AddressId bigint,
           @MouldBottle nvarchar(50),
		   @VendorBottleMould nvarchar(50),
		   @MouldPouch	nvarchar(50),
		   @VendorMouldPouch nvarchar(50),
		   @JarWashing	nvarchar(50),	
@JarWashingType	nvarchar(50),	
@JarWashingCapacity	nvarchar(50),
@JarWashingVendor	nvarchar(50),
@Blow	nvarchar(50),	

@BlowType	nvarchar(50),	
@BlowCapacity	nvarchar(50),
@BlowVendor	nvarchar(50),	
@PackBulk	nvarchar(50),	
@BulkType	nvarchar(50),	
@BulkCapacity	nvarchar(50),
@BulkVendor	nvarchar(50),	
@Soda	nvarchar(50),	
@SodaType	nvarchar(50),
@SodaCapacity	nvarchar(50),
@SodaVendor	nvarchar(50),	
@BatchCoding	nvarchar(50),	
@BatchCodingType	nvarchar(50),
@BatchCodingCapacity	nvarchar(50),
@BatchCodingVendor	nvarchar(50),
@Glass	       nvarchar(50),	
@GlassType	   nvarchar(50),
@GlassCapacity	nvarchar(50),
@GlassVendor	nvarchar(50)         

AS
Begin
if not exists (select Fk_AddressId from [Tbl_PackagingDetail] where Fk_AddressId=@Fk_AddressId)
begin
INSERT INTO [Tbl_PackagingDetail]
           ([Bottling]
           ,[BottlingType]
           ,[BottllingCapacity]
           ,[BottlingVendor1]
           ,[BottlingVendor2]
           ,[Pouch]
           ,[PouchType]
           ,[PouchCapacity]
           ,[PouchVendor1]
           ,[PouchVendor2]
           ,[Chiller]
           ,[ChillerVendor1]
           ,[ChillerVendor2]
           ,[Compressor]
           ,[Lab]
           ,[LabVendor1]
           ,[LabVendor2]
           ,[Letter11]
           ,[Letter12]
           ,[Letter21]
           ,[Letter22]
           ,[Letter31]
           ,[Letter32]
           ,[Letter41]
           ,[Letter42]
           ,[Letter51]
           ,[Letter52]
           ,[Remarks]
           ,[ProjectType]
           ,[DipatchStatus]
           ,[DispatchRemarks]
           ,[Fk_AddressId]
           ,MouldBottle
		   ,VendorBottleMould
		   ,MouldPouch
		   ,VendorMouldPouch
		   ,JarWashing ,	
 JarWashingType,	
 JarWashingCapacity,
 JarWashingVendor,
 Blow,	

 BlowType,	
 BlowCapacity,
 BlowVendor,	
 PackBulk,	
 BulkType,	
 BulkCapacity,
 BulkVendor,	
 Soda,	
 SodaType,
 SodaCapacity,
 SodaVendor,	
 BatchCoding,	
 BatchCodingType,
 BatchCodingCapacity,
 BatchCodingVendor,
 Glass,
 GlassType,
 GlassCapacity,
 GlassVendor		   
		   )
     VALUES
           (@Bottling,
           @BottlingType,
           @BottllingCapacity,
           @BottlingVendor1,
           @BottlingVendor2,
           @Pouch,
           @PouchType,
           @PouchCapacity,
           @PouchVendor1,
           @PouchVendor2,
           @Chiller,
           @ChillerVendor1,
           @ChillerVendor2,
           @Compressor,
           @Lab,
           @LabVendor1,
           @LabVendor2,
           @Letter11,
           @Letter12,
           @Letter21,
           @Letter22,
           @Letter31,
           @Letter32,
           @Letter41,
           @Letter42,
           @Letter51,
           @Letter52,
           @Remarks,
           @ProjectType,
           @DipatchStatus,
           @DispatchRemarks,
           @Fk_AddressId,
           @MouldBottle,
		   @VendorBottleMould,
		   @MouldPouch,
		   @VendorMouldPouch
		   ,@JarWashing ,	
 @JarWashingType,	
  @JarWashingCapacity,
  @JarWashingVendor,
  @Blow,	

  @BlowType,	
  @BlowCapacity,
  @BlowVendor,	
  @PackBulk,	
  @BulkType,	
  @BulkCapacity,
  @BulkVendor,	
  @Soda,	
  @SodaType,
  @SodaCapacity,
  @SodaVendor,	
  @BatchCoding,	
  @BatchCodingType,
  @BatchCodingCapacity,
  @BatchCodingVendor,
  @Glass,
 @GlassType,
 @GlassCapacity,
 @GlassVendor 
           )
           
return Scope_Identity();


end
else
begin
return -1;

end
         


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OutwardMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OutwardMaster]
@Fk_AddressId bigint,
           @CustomerName nvarchar(200),
           @Address nvarchar(max),
           @ContactNo nvarchar(50),
           @Remarks nvarchar(max),
		   @OutwardDate datetime,
           @EngineerName nvarchar(50)
AS
begin
INSERT INTO [Tbl_OutwardMaster]
           ([Fk_AddressId]
           ,[CustomerName]
           ,[Address]
           ,[ContactNo]
           ,[Remarks]
         ,[OutwardDate]
           ,[EngineerName]
           )
     VALUES
           (@Fk_AddressId,
 @CustomerName,
 @Address,
 @ContactNo,
 @Remarks,                     
           @OutwardDate,
           @EngineerName
          )
     
           
          return scope_identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OutwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OutwardDetail]
@Fk_OutwardId bigint,
@Fk_ProductRegisterId bigint,
@Fk_RowMaterialId bigint,
@Quantity bigint,
@Unit nvarchar(50),
 @Remarks nvarchar(max)
AS
begin
INSERT INTO [Tbl_OutwardDetail]
           ([Fk_OutwardId]
           ,[Fk_ProductRegisterId]
           ,[Fk_RowMaterialId]
           ,[Quantity]
           ,[Unit]
           ,Remarks)
     VALUES
           (@Fk_OutwardId ,
           @Fk_ProductRegisterId,
           @Fk_RowMaterialId,
           @Quantity,
           @Unit,
           @Remarks)         
      
     return scope_Identity();

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderVisitorDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderVisitorDetail]
@Visitor1Image1 nvarchar(max),
           @Visitor1Value1 nvarchar(100),
           @Visitor1Value2 nvarchar(100),
           @Visitor1Value3 nvarchar(100),
           @Visitor1Value4 nvarchar(100),
           @Visitor2Image1 nvarchar(max),
           @Visitor2Value1 nvarchar(100),
           @Visitor2Value2 nvarchar(100),
           @Visitor2Value3 nvarchar(100),
           @Visitor2Value4 nvarchar(100),
           @Visitor3Image1 nvarchar(max),
           @Visitor3Value1 nvarchar(100),
           @Visitor3Value2 nvarchar(100),
           @Visitor3Value3 nvarchar(100),
           @Visitor3Value4 nvarchar(100),
           @Fk_AddressId bigint
AS
Begin

if not exists (select Fk_AddressId from Tbl_OrderServiceFollowUpDetail where Fk_AddressId=@Fk_AddressId)
begin
INSERT INTO [Tbl_OrderVisitorDetail]
           ([Visitor1Image1]
           ,[Visitor1Value1]
           ,[Visitor1Value2]
           ,[Visitor1Value3]
           ,[Visitor1Value4]
           ,[Visitor2Image1]
           ,[Visitor2Value1]
           ,[Visitor2Value2]
           ,[Visitor2Value3]
           ,[Visitor2Value4]
           ,[Visitor3Image1]
           ,[Visitor3Value1]
           ,[Visitor3Value2]
           ,[Visitor3Value3]
           ,[Visitor3Value4]
           ,Fk_AddressId)
     VALUES
           (@Visitor1Image1,
           @Visitor1Value1,
           @Visitor1Value2,
           @Visitor1Value3,
           @Visitor1Value4,
           @Visitor2Image1,
           @Visitor2Value1,
           @Visitor2Value2,
           @Visitor2Value3,
           @Visitor2Value4,
           @Visitor3Image1,
           @Visitor3Value1,
           @Visitor3Value2,
           @Visitor3Value3,
           @Visitor3Value4,
           @Fk_AddressId)
return Scope_Identity();

end

else
begin
 return -1;
end

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderServiceFollowUpDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderServiceFollowUpDetail]
@SFDate datetime,
           @ServiceType nvarchar(50),
           @ComplainNo nvarchar(30),
           @AttendDate datetime,
           @AttendBy nvarchar(50),
           @Engineer nvarchar(50),
           @FollowUp nvarchar(100),
           @TentativeADate datetime,
           @Status nvarchar(50),
           @Remarks nvarchar(300),
           @Fk_AddressId bigint
As
begin

if not exists (select Fk_AddressId from Tbl_OrderServiceFollowUpDetail where Fk_AddressId=@Fk_AddressId)
begin
INSERT INTO [Tbl_OrderServiceFollowUpDetail]
           ([SFDate]
           ,[ServiceType]
           ,[ComplainNo]
           ,[AttendDate]
           ,[AttendBy]
           ,[Engineer]
           ,[FollowUp]
           ,[TentativeADate]
           ,[Status]
           ,[Remarks]
           ,[Fk_AddressId])
     VALUES
           (@SFDate,
           @ServiceType, 
           @ComplainNo,
           @AttendDate,
           @AttendBy,
           @Engineer,
           @FollowUp,
           @TentativeADate,
           @Status,
           @Remarks,
           @Fk_AddressId)
          return scope_identity();
          end
          
          else
           begin
          return -1;
          
          
          end
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderPartyMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderPartyMaster]
@Fk_AddressId bigint,
         
         
           @ContactPersonOne nvarchar(100),
           @ContactPersonTwo nvarchar(100),
           @VatNoTinNo nvarchar(80),
           @CSTNo nvarchar(50),
           @RoadPermit nvarchar(100),
           @CForm nvarchar(50),
           @TransportExpences decimal(18,2),
           @Remarks nvarchar(300)

AS
begin

if not exists(Select Pk_OrderPartyId from Tbl_OrderPartyMaster where Fk_AddressId=@Fk_AddressId)
begin
INSERT INTO [Tbl_OrderPartyMaster]
           ([Fk_AddressId]
           ,[ContactPersonOne]
           ,[ContactPersonTwo]
           ,[VatNoTinNo]
           ,[CSTNo]
           ,[RoadPermit]
           ,[CForm]
           ,[TransportExpences]
           ,[Remarks]
          )
     VALUES
           (@Fk_AddressId,
           
           @ContactPersonOne,
           @ContactPersonTwo,
           @VatNoTinNo,
           @CSTNo,
           @RoadPermit,
           @CForm,
           @TransportExpences,
           @Remarks
         )
           
           return Scope_Identity();
           
           end
           else
           begin 
           return 0;
           end
           
           end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderOutstandingDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderOutstandingDetail]
@OrderStatus nvarchar(50),
           @OrderAmount numeric(18,2),
           @AdvanceRecieved numeric(18,2),
           @PaymentRecieved numeric(18,2),
           @OutstandingAmount numeric(18,2),
           @Remarks nvarchar(max),
           @Fk_AddressId bigint
        
AS
begin
if not exists (select Fk_AddressId from Tbl_OrderOutstandingDetail where Fk_AddressId=@Fk_AddressId)
begin
INSERT INTO [Tbl_OrderOutstandingDetail]
           ([OrderStatus]
           ,[OrderAmount]
           ,[AdvanceRecieved]
           ,[PaymentRecieved]
           ,[OutstandingAmount]
           ,[Remarks]
           ,[Fk_AddressId])
     VALUES
           (@OrderStatus,
           @OrderAmount,
           @AdvanceRecieved,
           @PaymentRecieved,
           @OutstandingAmount,
           @Remarks,
           @Fk_AddressId)
          return scope_identity();
          
          end
          else
          begin
          return -1;
          
          end
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderOneMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderOneMaster]
@EntryNo nvarchar(50),
           @EntryDate datetime,
           @PONo nvarchar(50),
           @OrderNo nvarchar(50),
           @OrderDate datetime,
           @DispatchDate datetime,
           @PartyName nvarchar(100),
           @BrandName nvarchar(200),
          
           @Fk_AddressId bigint,
           @OrderRecBy	nvarchar(50),
		   @OrderFollowBy	nvarchar(50),
		   @OrderStatus	 nvarchar(50),
		   @OrderRecFromMkt datetime
AS
Begin

if not exists (select Fk_AddressId from Tbl_OrderOneMaster where Fk_AddressId=@Fk_AddressId)
begin

INSERT INTO [Tbl_OrderOneMaster]
           ([EntryNo]
           ,[EntryDate]
           ,[PONo]
           ,[OrderNo]
           ,[OrderDate]
           ,[DispatchDate]
           ,[PartyName]
           ,[BrandName]
          ,[Fk_AddressId]
,OrderRecBy	
,OrderFollowBy
         ,OrderStatus
         ,OrderRecFromMkt
          )
     VALUES
           (@EntryNo,
           @EntryDate,
           @PONo,
           @OrderNo,
           @OrderDate,
           @DispatchDate,
           @PartyName,
           @BrandName,
                  @Fk_AddressId,
@OrderRecBy,
@OrderFollowBy
,@OrderStatus
,@OrderRecFromMkt)
return Scope_Identity();
end

else
begin
return -1;

end


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderMaster_Two]
@EntryNo nvarchar(50),
@EntryDate datetime,
@PONo nvarchar(50),
@OrderNo nvarchar(50),
@OrderDate datetime,
@DispatchDate datetime,
@PartyName nvarchar(100),
@BrandName nvarchar(200),

@Fk_AddressId bigint
AS
Begin
INSERT INTO [Tbl_OrderMaster_Two]
([EntryNo]
,[EntryDate]
,[PONo]
,[OrderNo]
,[OrderDate]
,[DispatchDate]
,[PartyName]
,[BrandName]
,[Fk_AddressId])
VALUES
(@EntryNo,
@EntryDate,
@PONo,
@OrderNo,
@OrderDate,
@DispatchDate,
@PartyName,
@BrandName,
@Fk_AddressId)
return Scope_Identity();


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderFollowupMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderFollowupMaster]
@ProjectDetail nvarchar(200),
           @Fk_AddressId bigint
           AS
begin

if not exists (select Fk_AddressId from Tbl_OrderFollowupMaster where Fk_AddressId=@Fk_AddressId)
begin
INSERT INTO [Tbl_OrderFollowupMaster]
           ([ProjectDetail]
           ,[Fk_AddressId])
     VALUES
           (@ProjectDetail,
           @Fk_AddressId)
           return Scope_Identity();
           end
           else
           begin
           return -1;
           
           end
           
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_OrderFollowupDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_OrderFollowupDetail]  
@Fk_AddressId bigint,  
           @FDate datetime,  
          
           @FollowUp nvarchar(50),  
           @NFDate datetime,  
           @Status nvarchar(50),  
           @ByWhom nvarchar(50),  
           @ProjectType nvarchar(50),  
           @Remarks nvarchar(200)  
AS  
begin  
  
begin  
  
INSERT INTO [Tbl_OrderFollowupDetail]  
           ([Fk_AddressId]  
           ,[FDate]  
           
           ,[FollowUp]  
           ,[NFDate]  
           ,[Status]  
           ,[ByWhom]  
           ,[ProjectType]  
           ,[Remarks])  
     VALUES  
           (@Fk_AddressId,  
           @FDate,  
          
           @FollowUp,  
           @NFDate,  
           @Status,  
           @ByWhom,  
           @ProjectType,  
           @Remarks)  
  
          return scope_identity();  
          end  
     
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_LetterMailComMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_LetterMailComMaster_Two]
	@Fk_AddressId bigint,
	@ProjectDetail nvarchar(200)
As
BEGIN

if not exists (select Fk_AddressId from Tbl_LetterMailComMaster_Two where Fk_AddressId=@Fk_AddressId)
begin

	 INSERT INTO Tbl_LetterMailComMaster_Two
		(
		Fk_AddressId,
		ProjectDetail
		)
	 VALUES
		(
		@Fk_AddressId,
		@ProjectDetail
		)
		return Scope_Identity()
		end
		else
		begin
		return -1;
		end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_LetterMailComMaster_Detail_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_LetterMailComMaster_Detail_Two]
	@Fk_AddressId bigint,
	@LDate datetime,
	@Card_Date datetime,
	@Card_Rem nvarchar(500),
	@Mail_Rec nvarchar(200),
	@Mail_Send nvarchar(200),
	@BY_Whom nvarchar(200),
	@Mail_Rem nvarchar(500)
As
BEGIN
	 INSERT INTO Tbl_LetterMailComMaster_Detail_Two
		(
		Fk_AddressId,
		LDate,
		Card_Date,
		Card_Rem,
		Mail_Rec,
		Mail_Send,
		BY_Whom,
		Mail_Rem
		)
	 VALUES
		(
		@Fk_AddressId,
		@LDate,
		@Card_Date,
		@Card_Rem,
		@Mail_Rec,
		@Mail_Send,
		@BY_Whom,
		@Mail_Rem
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_ISIProcessMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_ISIProcessMaster_Two]
	@Fk_AddressId bigint,
	@Scheme_Name nvarchar(500),
	@D_Rec_Date datetime,
	@P_Doc_F_Date datetime,
	@P_Doc_R_Date datetime,
	@F_Ok_S_Tc_P nvarchar(500),
	@F_Submit_P nvarchar(500),
	@File_Reg_Date datetime,
	@BIS_Insp_Date datetime,
	@Licence_Date datetime,
	@Vender nvarchar(max),
	@ISIRemark nvarchar(max)
As
BEGIN


if not exists (select Fk_AddressId from Tbl_ISIProcessMaster_Two where Fk_AddressId=@Fk_AddressId)
begin
	 INSERT INTO Tbl_ISIProcessMaster_Two
		(
		Fk_AddressId,
		Scheme_Name,
		D_Rec_Date,
		P_Doc_F_Date,
		P_Doc_R_Date,
		F_Ok_S_Tc_P,
		F_Submit_P,
		File_Reg_Date,
		BIS_Insp_Date,
		Licence_Date,
		Vender,
		ISIRemark
		)
	 VALUES
		(
		@Fk_AddressId,
		@Scheme_Name,
		@D_Rec_Date,
		@P_Doc_F_Date,
		@P_Doc_R_Date,
		@F_Ok_S_Tc_P,
		@F_Submit_P,
		@File_Reg_Date,
		@BIS_Insp_Date,
		@Licence_Date,
		@Vender,
		@ISIRemark
		)
		return Scope_Identity()
		end
		else
		begin
		
		return -1;
		end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_ISIProcess_DetailMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_ISIProcess_DetailMaster_Two]
	@Fk_AddressId bigint,
         @FDate datetime,
        
           @FollowUp nvarchar(50),
           @NFDate datetime,
           @Status nvarchar(50),
           @ByWhom nvarchar(50),
           @ProjectType nvarchar(50),
           @Remarks nvarchar(200)
As
BEGIN
	 INSERT INTO Tbl_ISIProcess_DetailMaster_Two
		([Fk_AddressId]
           ,[FDate]
         
           ,[FollowUp]
           ,[NFDate]
           ,[Status]
           ,[ByWhom]
           ,[ProjectType]
           ,[Remarks])
     VALUES
           (@Fk_AddressId,
           @FDate,
        
           @FollowUp,
           @NFDate,
           @Status,
           @ByWhom,
           @ProjectType,
           @Remarks)

          return scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_InwardStockMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_InwardStockMaster]
@PONo nvarchar(50),
@InwardDate datetime,
@BillNo nvarchar(50),
@BillDate datetime,
@SupplierName nvarchar(100),
@SupplierAddress nvarchar(max)
AS
begin

if not exists(select Pk_InwardId from Tbl_InwardStockMaster where PONo=@PONo and BillNo=@BillNo)
begin

INSERT INTO [Tbl_InwardStockMaster]
(
 [PONo]
 ,[InwardDate]
 ,[BillNo]
 ,[BillDate]
 ,[SupplierName]
 ,[SupplierAddress]
)
VALUES
(
 @PONo,
 @InwardDate,
 @BillNo,
 @BillDate,
 @SupplierName,
@SupplierAddress)
return Scope_Identity();
end
else
begin
return -1;

end
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_InwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_InwardDetail]
@Fk_ProductRegisterId bigint,
           @Fk_RowMaterialId bigint,
           @Fk_InwardId bigint,
           @Quantity bigint,
           @Unit nvarchar(50),
           @Remarks nvarchar(max)           
           AS
begin

INSERT INTO [Tbl_InwardDetail]
           ([Fk_ProductRegisterId]
           ,[Fk_RowMaterialId]
           ,[Fk_InwardId]
           ,[Quantity]
           ,[Unit]
           ,[Remarks])
     VALUES
           (@Fk_ProductRegisterId,
           @Fk_RowMaterialId,
           @Fk_InwardId,
           @Quantity,
           @Unit,
           @Remarks)
           
           
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_InvoiceMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_InvoiceMaster]
@Fk_AddressId bigint,
           @Fk_PartyDebitid bigint,
           @Fk_PartyId bigint,
           @InvoiceNo nvarchar(50),
           @Transporter nvarchar(100),
           @RRLRNo nvarchar(50),
           @BasicAmount decimal(18,2),
           @Discount numeric(18,2),
           @Total numeric(18,2),
           @CSTPerc int,
           @SubTotal numeric(18,2),
           @Advance numeric(18,2),
           @NetAmount numeric(18,2),
           @BankDetail nvarchar(100),
           @InvoiceDate datetime,
           @CSTNo nvarchar(50),
           @GSTNo nvarchar(50)
           AS
begin

if not exists(select Fk_AddressId from Tbl_InvoiceMaster where Fk_AddressId=@Fk_AddressId)
begin

INSERT INTO [Tbl_InvoiceMaster]
           ([Fk_AddressId]
           ,[Fk_PartyDebitid]
           ,[Fk_PartyId]
           ,[InvoiceNo]
           ,[Transporter]
           ,[RRLRNo]
           ,[BasicAmount]
           ,[Discount]
           ,[Total]
           ,[CSTPerc]
           ,[SubTotal]
           ,[Advance]
           ,[NetAmount]
           ,[BankDetail]
           ,InvoiceDate
,CSTNo
 ,GSTNo
           
           )
     VALUES
           (@Fk_AddressId,
           @Fk_PartyDebitid,
           @Fk_PartyId,
           @InvoiceNo,
           @Transporter,
           @RRLRNo,
           @BasicAmount,
           @Discount,
           @Total,
           @CSTPerc,
           @SubTotal,
           @Advance,
           @NetAmount,
           @BankDetail
           ,@InvoiceDate
, @CSTNo
 ,@GSTNo
           
           )           
           
           return Scope_Identity();
           end
           else
           begin
           return 0
           end
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_DocumentMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_DocumentMaster]

@Fk_AddressId bigint,
          @Fk_DocId int
           
           AS
begin

INSERT INTO [Tbl_DocumentMaster]
           ([Fk_AddressId]
           ,Fk_DocId)
     VALUES
           (@Fk_AddressId,
           @Fk_DocId)
      
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_DocumentLog]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_DocumentLog]
@Fk_DocumentId bigint,
           @MailBy nvarchar(50),
           @MailDate nvarchar(50),
           @CourierBy nvarchar(50),
           @CourierDate nvarchar(50)
           AS
begin

INSERT INTO [Tbl_DocumentLog]
           ([Fk_DocumentId]
           ,[MailBy]
           ,[MailDate]
           ,[CourierBy]
           ,[CourierDate])
     VALUES
           (@Fk_DocumentId,
           @MailBy,
           @MailDate,
           @CourierBy,
           @CourierDate)
      
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_DocMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_DocMaster]
@DocumentName nvarchar(100)

           AS
begin

INSERT INTO [Tbl_DocMaster]
           ([DocumentName])
     VALUES
           (@DocumentName)
           
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Tbl_DetailDocument]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Tbl_DetailDocument]
@Fk_DocumentId int,
           @DetailDocumentName nvarchar(100)
           AS
begin

INSERT INTO [Tbl_DetailDocument]
           ([Fk_DocumentId]
           ,[DetailDocumentName])
     VALUES
           (@Fk_DocumentId,
           @DetailDocumentName)
      
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Service_Address_Info]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Service_Address_Info]  
   @fk_AddressId bigint  
           ,@Name varchar(50)  
           ,@Post varchar(50)  
           ,@ContactNo varchar(12)  
           ,@EmailId varchar(50)  
             
       as   
       begin  
             
INSERT INTO [Service_Address_Info]  
           ([Fk_AddressId]  
           ,[Name]  
           ,[Post]  
           ,[ContactNumber]  
           ,[EmailId])  
     VALUES  
           (@fk_AddressId   
           ,@Name   
           ,@Post   
           ,@ContactNo  
           ,@EmailId )  
             
           return Scope_Identity();  
             
           end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_RowMaterialMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_RowMaterialMaster]

@RowMaterialName nvarchar(200),
           @Unit nvarchar(30),
           @ReOrder bigint,
           @Fk_ProductRegisterId bigint,
           @Status bit
           
           AS
begin

INSERT INTO [Tbl_RowMaterialMaster]
           ([RowMaterialName]
           ,[Unit]
           ,[ReOrder]
           ,[Fk_ProductRegisterId]
           ,[Status]
           )
     VALUES
           (@RowMaterialName,
           @Unit,
           @ReOrder,
           @Fk_ProductRegisterId,
           @Status)
      
           return Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_Quotation_Master]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[sp_Insert_Quotation_Master]
	@QType varchar(50),
	@Fk_EnqTypeID varchar(50),
	@Quot_No int,
	@Q_Year int,
	@Enq_No varchar(50),
	@Ref varchar(50),
	@Quot_Type varchar(50),
	@Name varchar(50),
	@Address varchar(max) ,
	@Capacity_Type varchar(50) ,
	@Capacity_Single varchar(50) ,
	@Capacity_Multiple varchar(50) ,
	@KindAtt varchar(max),
	@Subject varchar (max) ,
	@Buss_Excecutive varchar(50) ,
	@Buss_Name varchar(50) ,
	@Later_Description varchar(max) ,
	@Later_Date varchar(50) ,
	@Capacity_Word varchar(max) ,
	@UserName varchar(50) ,
	@DefaultImage varchar(max) ,
	@QDate varchar(50) ,
	@Quatation_Type varchar(50) ,
	@LanguageId int 
as
begin
INSERT INTO Quotation_Master
           (QType
           ,Fk_EnqTypeID
           ,Quot_No
           ,Q_Year
           ,Enq_No
           ,Ref
           ,Quot_Type
           ,Name
           ,Address
           ,Capacity_Type
           ,Capacity_Single
           ,Capacity_Multiple
           ,KindAtt
           ,Subject
           ,Buss_Excecutive
           ,Buss_Name
           ,Later_Description
           ,Later_Date
           ,Capacity_Word
           ,UserName
           ,DefaultImage
           ,QDate
           ,Quatation_Type
           ,LanguageId)
     VALUES
           (@Qtype
           ,@Fk_EnqTypeID,
           @Quot_No 
           ,@Q_Year 
           ,@Enq_No 
           ,@Ref 
           ,@Quot_Type 
           ,@Name 
           ,@Address 
           ,@Capacity_Type 
           ,@Capacity_Single 
           ,@Capacity_Multiple 
           ,@KindAtt 
           ,@Subject 
           ,@Buss_Excecutive 
           ,@Buss_Name 
           ,@Later_Description 
           ,@Later_Date 
           ,@Capacity_Word 
           ,@UserName 
           ,@DefaultImage 
           ,@QDate 
           ,@Quatation_Type 
           ,@LanguageId)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_ProductRegisterMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_ProductRegisterMaster]
@CategoryName nvarchar(200),
@Status bit
AS


begin

INSERT INTO [Tbl_ProductRegisterMaster]
           (
           [CategoryName]
           ,[Status]
           )
     VALUES
           (
           @CategoryName,
           @Status
           )
           
           return Scope_Identity();
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Party_Master]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Party_Master]
@EntryNo nvarchar(50),
@OrderDate datetime,
@PONo nvarchar(50),
@OType nvarchar(100),
@Fk_AddressId bigint,
@PlantName nvarchar(150),
@Capacity nvarchar(50),
@DispatchDate datetime,
@ExecutiveName nvarchar(80),
@OrderStatus nvarchar(50),
@Remarks nvarchar(max),
@BreakSrNo numeric(18,0),
@PDCReminder nvarchar(max)

AS
Begin

if  not exists (select * from Tbl_OrderOneMaster where Fk_AddressId=@Fk_AddressId)
begin
	return  -1 
end 

if not exists (select Fk_AddressId from Party_Master where Fk_AddressId=@Fk_AddressId)
begin

INSERT INTO [dbo].[Party_Master]
           ([EntryNo]
           ,[OrderDate]
           ,[PONo]
           ,[OType]
           ,[Fk_AddressId]
           ,[PlantName]
           ,[Capacity]
           ,[DispatchDate]
           ,[ExecutiveName]
           ,[OrderStatus]
           ,[Remarks]
           ,[BreakSrNo]
           ,[PDCReminder]
          )
     VALUES
           (@EntryNo,
@OrderDate,
@PONo,
@OType,
@Fk_AddressId,
@PlantName,
@Capacity,
@DispatchDate,
@ExecutiveName,
@OrderStatus,
@Remarks,
@BreakSrNo,
@PDCReminder)

return Scope_Identity();
end
else
begin
return 0;
end


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Party_DebitDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Party_DebitDetail]  
@Fk_AddressID bigint,  
           @DebitEntryNo int,  
           @PlantScheme nvarchar(80),  
           @Qty int,  
           @Amount numeric(18,2)  
  
AS  
Begin  
INSERT INTO [dbo].[Party_Debit_Detail]  
           ([Fk_AddressID]  
           ,[DebitEntryNo]  
           ,[PlantScheme]  
           ,Qty  
           ,[Amount])  
     VALUES  
           (@Fk_AddressID,  
           @DebitEntryNo,  
           @PlantScheme,  
           @Qty,  
           @Amount)  
return Scope_Identity();  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Party_Debit]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Party_Debit]  
@Fk_AddressId bigint,  
@TotalDebit numeric(18,2),  
@Discount numeric(18,2),  
@NetDebit numeric(20,2)  
AS  
Begin  
INSERT INTO [dbo].[Party_Debit]  
           ([Fk_AddressId]  
           ,[TotalDebit]  
           ,[Discount]  
           ,[NetDebit])  
     VALUES  
           (@Fk_AddressId ,  
@TotalDebit,  
@Discount,  
@NetDebit)  
  
return Scope_Identity();  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Party_CreditDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Party_CreditDetail]  
@Fk_AddressID bigint,  
@EntryNo int,  
@PType nvarchar(50),  
@PDate datetime,  
@Amount numeric(18,3)  
  
AS  
Begin  
 INSERT INTO [dbo].[Party_CreditDetail]  
           ([Fk_AddressID]  
           ,[EntryNo]  
           ,[PType]  
           ,[PDate]  
           ,[Amount])  
     VALUES  
           (@Fk_AddressID,  
@EntryNo,  
@PType,  
@PDate,  
@Amount)  
return Scope_Identity();  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Party_Credit]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Insert_Party_Credit]  
@Fk_AddressID bigint,  
           @TotalCredit numeric(18,2),  
           @Kasar numeric(18,2),  
           @Outstanding numeric(20,2),  
           @Advance numeric(18,2)  
AS  
Begin  
INSERT INTO [dbo].[Party_Credit]  
           ([Fk_AddressID]
           ,[TotalCredit]  
           ,[Kasar]  
           ,[Outstanding]  
           ,[Advance])  
     VALUES  
           (@Fk_AddressID,  
           @TotalCredit,  
           @Kasar,  
           @Outstanding,  
           @Advance)  
  
return Scope_Identity();  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryPOUCH]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryPOUCH]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select pd.Pouch 'POUCH',COUNT(pd.Pouch) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         
group by pd.Pouch

end
else 
begin
select pd.Pouch 'POUCH',COUNT(pd.Pouch) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status
group by pd.Pouch



end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryLAB]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryLAB]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select pd.Lab 'LAB',COUNT(pd.Lab) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID

where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         
group by pd.Lab




end
else 
begin
select pd.Lab 'LAB',COUNT(pd.Lab) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID

where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status
group by pd.Lab



end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryJAR]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryJAR]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select pd.JarWashingCapacity 'JAR',COUNT(pd.JarWashingCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         
group by pd.JarWashingCapacity

end
else 
begin
select pd.JarWashingCapacity,COUNT(pd.JarWashingCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status
group by pd.JarWashingCapacity



end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryGLASS]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryGLASS]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select pd.GlassCapacity 'GLASS',COUNT(pd.GlassCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         
group by pd.GlassCapacity

end
else 
begin
select pd.GlassCapacity 'GLASS',COUNT(pd.GlassCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status
group by pd.GlassCapacity



end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryCountByType]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryCountByType]
@From as datetime,
@To as datetime,
@Status as nvarchar(50),
@TYPE AS VARCHAR(40),
@NAME AS VARCHAR(20)
AS
begin

DECLARE @QRY AS NVARCHAR(MAX);
BEGIN TRY
    -- Generate divide-by-zero error.
   
if(@Status<>'')
begin

SET @QRY='select pd.'+@TYPE+' '+@NAME+',COUNT(pd.'+@TYPE+') as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
 where om.EntryDate between ''' + CONVERT(varchar(250) , @From , 101 ) + ' 00:00:00'' And ''' + CONVERT(varchar(250) , @To , 101 ) + ' 23:59:59''
group by pd.'+@TYPE+''
exec (@Qry);   

end
else 
begin
SET @QRY='select pd.'+@TYPE+' '''+@NAME+''',COUNT(pd.'+@TYPE+') as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID

 where om.EntryDate between ''' + CONVERT(varchar(250) , @From , 101 ) + ' 00:00:00'' And ''' + CONVERT(varchar(250) , @To , 101 ) + ' 23:59:59''

and om.OrderStatus like '''+@Status+'''
group by pd.'+@TYPE+''
exec (@Qry);   



end


 
END TRY
BEGIN CATCH
    -- Execute error retrieval routine.
    EXECUTE usp_GetErrorInfo;
END CATCH; 
end

--EXEC Sp_GetWeeklyOrderReportSummaryCountByType '2012/12/12','2013/12/12','','BulkCapacity','BULK'
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryBOTTLE]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryBOTTLE]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select pd.BottllingCapacity 'BOTTLE',COUNT(pd.BottllingCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         
group by pd.BottllingCapacity

end
else 
begin
select pd.BottllingCapacity 'BOTTLE',COUNT(pd.BottllingCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status
group by pd.BottllingCapacity



end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummaryBLOW]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummaryBLOW]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select pd.BlowCapacity 'BLOW',COUNT(pd.BlowCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         
group by pd.BlowCapacity

end
else 
begin
select pd.BlowCapacity 'BLOW',COUNT(pd.BlowCapacity) as Count from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status
group by pd.BlowCapacity



end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReportSummary]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReportSummary]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select ROW_NUMBER() OVER(ORDER BY am.Pk_AddressId ) AS SrNo,am.EnqNo,am.Name,am.City,pm.Model,pm.Capacity,pd.Lab,pd.JarWashingCapacity,pd.Pouch,pd.BottllingCapacity,pd.BlowCapacity,pd.GlassCapacity,pd.BatchCoding,pd.BulkCapacity,pd.Chiller,pd.SodaCapacity,om.OrderStatus from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID

where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         




end
else 
begin
select ROW_NUMBER() OVER(ORDER BY am.Pk_AddressId ) AS SrNo,am.EnqNo,am.Name,am.City,pm.Model,pm.Capacity,pd.Lab,pd.JarWashingCapacity,pd.Pouch,pd.BottllingCapacity,pd.BlowCapacity,pd.GlassCapacity,pd.BatchCoding,pd.BulkCapacity,pd.Chiller,pd.SodaCapacity,om.OrderStatus from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status


end

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetWeeklyOrderReport]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetWeeklyOrderReport]
@From as datetime,
@To as datetime,
@Status as nvarchar(50)

AS
begin

if(@Status<>'')
begin
select ROW_NUMBER() OVER(ORDER BY am.Pk_AddressId ) AS SrNo,am.EnqNo,am.Name,am.City,pm.Model,pm.Capacity,pd.Lab,pd.JarWashingCapacity,pd.Pouch,pd.BottllingCapacity,pd.BlowCapacity,pd.GlassCapacity,pd.BatchCoding,pd.BulkCapacity,pd.Chiller,pd.SodaCapacity,om.OrderStatus from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)         

end
else 
begin
select ROW_NUMBER() OVER(ORDER BY am.Pk_AddressId ) AS SrNo,am.EnqNo,am.Name,am.City,pm.Model,pm.Capacity,pd.Lab,pd.JarWashingCapacity,pd.Pouch,pd.BottllingCapacity,pd.BlowCapacity,pd.GlassCapacity,pd.BatchCoding,pd.BulkCapacity,pd.Chiller,pd.SodaCapacity,om.OrderStatus from Address_Master am
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=am.Pk_AddressID
inner join Tbl_ProjectInformationMaster pm
on pm.Fk_AddressId=am.Pk_AddressID
inner join Tbl_PackagingDetail pd
on pd.Fk_AddressId=am.Pk_AddressID
where convert(varchar,om.EntryDate,112) between convert(varchar,@From,112) and  convert(varchar,@To,112)
and om.OrderStatus like @Status


end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserSoftwareName]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_GetUserSoftwareName]
@userId bigint
As
BEGIN

select distinct(sm.Name),Pk_SoftwareId,sm.ViewIndex from Tbl_PermissionMaster pm
inner join Tbl_SoftwareMaster sm 
on sm.Pk_SoftwareId=pm.FK_SoftwareID
left join Tbl_SoftwareDetail sd
on sd.Pk_SoftwareDetail=pm.Fk_SoftwareDetailId
where pm.Fk_UserId=@userId
order by ViewIndex

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserSoftwareDetailName]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_GetUserSoftwareDetailName]
@userId bigint,
@name nvarchar(50),
@type nvarchar(40)
As
BEGIN

select sm.Name, sd.DetailName,pm.Type from Tbl_PermissionMaster pm
inner join Tbl_SoftwareMaster sm
on sm.Pk_SoftwareId=pm.FK_SoftwareID
left join Tbl_SoftwareDetail sd
on sd.Pk_SoftwareDetail=pm.Fk_SoftwareDetailId
where pm.Fk_UserId=@userId AND sm.Name=@name and sm.Status=1

	--WHERE	Pk_SoftwareId=@Pk_SoftwareId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSoftwareDetailBySoftwareName]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_GetSoftwareDetailBySoftwareName]

@name nvarchar(50)

As
BEGIN

select sd.DetailName from Tbl_SoftwareMaster sm
inner join Tbl_SoftwareDetail sd
on sm.Pk_SoftwareId=sd.Fk_SoftwareId
where sm.Name=@name and sm.Status=1 and sd.Status=1 and sd.Status=1

end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetOrderRecFollowDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetOrderRecFollowDetail]
@Fk_AddressId as bigint
as
begin

select vf.FollowBy as FollowBy,vf.SalesExc as RecBy from Enq_VisitorDetail vf
where upper(vf.E_Type)='OB' and Fk_AddressID=@Fk_AddressId


end
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetOrderEntryDate]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Sp_GetOrderEntryDate]
@Fk_AddressId as bigint
as
begin
select F_Date from Enq_FollowDetails ef
where upper(ef.EnqType)='OC' and Fk_AddressID=@Fk_AddressId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOrderDetailFromAgreements]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_GetOrderDetailFromAgreements]
@EnqNo nvarchar(50)
AS
begin
	SELECT q1.Ref,q1.Enq_No,c1.Plant,c1.Model,
	STUFF((SELECT distinct ' - ' + cast(Description as varchar(400)) + ' ' 
           from AnnexureData t2
           where c1.Pk_CompanyID = t2.Fk_CompanyID and t2.IsSelected='Yes'
           FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') TreatmentScheme
	from Quotation_Master q1
	inner join Company_Master c1
	on c1.FK_QuatationID=q1.Pk_QuotationID
	where q1.Enq_No=@EnqNo
end
GO
/****** Object:  StoredProcedure [dbo].[SP_getIdEnqStatus]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_getIdEnqStatus]  
    @EnqStatus varchar(100)  
  
AS  
begin  
declare @id as int  
  
set @id=(select top 1 Pk_EnqStatus from Enq_Status  
where EnqStatus=@EnqStatus)  
  return @id
  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAddressByName]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_GetAddressByName]
@Address varchar(200),
@Name varchar(50)


AS
begin
select Pk_AddressID  from Address_Master (nolock)
where Name like '%' + @Name + '%'
and Address like '%' + @Address + '%'
and EnqStatus=1

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_UserListByTeam]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_UserListByTeam]
@Fk_TeamId int
	
AS
begin
		select Pk_UserId,UserName from User_Master
	where Fk_TeamId=@Fk_TeamId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_UserList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_UserList]
	
AS
	
	
	select * from User_Master
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Total_RIInward]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Total_RIInward]
-- exec SP_Get_Total_Inward 5,3
@Category bigint,
@RowMaterial bigint
AS

begin
 select sum(Quantity) Total from Tbl_ReInwardMaster RIM inner join Tbl_RIInwardDetail RID on RIM.Pk_ReInwardId=RID.Fk_ReInwardId where Fk_ProductRegisterId=@Category and Fk_RowMaterialId=@RowMaterial

	


	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Total_Inward]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Total_Inward]
-- exec SP_Get_Total_Inward 5,3
@Category bigint,
@RowMaterial bigint
AS

begin
 select sum(Quantity) Total from Tbl_InwardStockMaster IM inner join Tbl_InwardDetail ID on im.Pk_InwardId=id.Fk_InwardId where Fk_ProductRegisterId=@Category and Fk_RowMaterialId=@RowMaterial

	


	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Total_INOUT_Detail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Total_INOUT_Detail]

AS
SELECT CategoryName, RowMaterialName,
     
      isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0) + isnull(InwardStock,0) AS TotInward,
	isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) + isnull(OutwardStock,0) AS TotOutward,
	(isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)+ isnull(InwardStock,0) + OpeningStock) - 
	(isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) + isnull(OutwardStock,0))as 'Remaining Stock',
rm.Fk_ProductRegisterId,rm.Pk_RowMaterialId,sp.Unit          
      
  FROM [Tbl_Stock_ProductRegister] sp (nolock)
  inner join Tbl_ProductRegisterMaster cm (nolock)
  on sp.Fk_CategoryId=cm.Pk_ProductRegisterId
   inner join Tbl_RowMaterialMaster rm (nolock)
        on rm.Pk_RowMaterialId=sp.Fk_RawMaterialId 

	RETURN

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_TodayAllotDataByUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_TodayAllotDataByUser]      
@byUser VARCHAR(200),      
@Start datetime,    
@End datetime    
AS    
begin
        
if(@byUser='All')
begin
  select addr.Pk_AddressID,addr.Name,addr.EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=addr.Pk_AddressID),0) 'Status' from dbo.Address_Master as addr (nolock)
  inner join Tbl_UserAllotmentDetail ua  (nolock)
  on addr.Pk_AddressID=ua.Fk_AddressId  
  where addr.EnqStatus=1 and convert(varchar,addr.EnqDate,112) between convert(varchar,@Start,112) 
  and  convert(varchar,@End,112)       
  order by addr.EnqDate desc    
    end
    else
    begin
    select addr.Pk_AddressID,addr.Name,addr.EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=addr.Pk_AddressID),0) 'Status' from dbo.Address_Master as addr (nolock)
  inner join Tbl_UserAllotmentDetail ua  (nolock)
  on addr.Pk_AddressID=ua.Fk_AddressId  
  INNER JOIN User_Master u
  on u.Pk_UserId=ua.Fk_UserId
  where addr.EnqStatus=1 and convert(varchar,addr.EnqDate,112) between convert(varchar,@Start,112) 
  and  convert(varchar,@End,112)
  and u.UserName in (@byUser)
         
  order by addr.EnqDate desc   
    
    end
      
 
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_TodayAllotData]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_TodayAllotData]      
@byUser int,      
@Start datetime,    
@End datetime    
AS    
begin
    
  select addr.Pk_AddressID,addr.Name,addr.EnqNo,isnull((select COUNT(*) from Enq_FollowDetails where Fk_AddressID=addr.Pk_AddressID),0) 'Status' from dbo.Address_Master as addr (nolock)
  inner join Tbl_UserAllotmentDetail ua  (nolock)
  on addr.Pk_AddressID=ua.Fk_AddressId  
  where addr.EnqStatus=1 and convert(varchar,ua.CreateDate,112) between convert(varchar,@Start,112) and  convert(varchar,@End,112)       
  and ua.Fk_UserId=@byUser
 order by ua.CreateDate desc    
      
 
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Tbl_ProjectInformationMaster_DType_AutoComp]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Tbl_ProjectInformationMaster_DType_AutoComp]
@Status int
as
Begin
if(@Status=0)
begin
 select distinct DType as F_Type from Tbl_ProjectInformationMaster where DType!=''
 end
 if(@Status=1)
 begin
  select distinct PlantName as F_Type from Tbl_ProjectInformationMaster where PlantName!=''
 end
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_SoftwareDetails_For_Permission]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_SoftwareDetails_For_Permission]
AS
Begin
Select * from Tbl_SoftwareMaster S (nolock) Left join 
       Tbl_SoftwareDetail D (nolock) on S.Pk_SoftwareId = D.Fk_SoftwareId 
Order By S.Pk_SoftwareId

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ServiceList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_ServiceList]
@Fk_AddressId bigint
AS
select top 1 ServiceType from Tbl_Service_ServiceLogMaster (nolock)
where Status like 'Pending' and Fk_AddressId=@Fk_AddressId order by ServiceType
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ServiceDate]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_ServiceDate]
@Fk_AddressId bigint,
@ServiceType nvarchar(50)
AS
select ServiceDate from Tbl_Service_ServiceLogMaster (nolock)
where Status like 'Pending' and Fk_AddressId=@Fk_AddressId and ServiceType=@ServiceType
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Service_Address_Info]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Service_Address_Info]  
 @Fk_AddressId bigint  
AS  
SELECT *  
FROM Service_Address_Info (nolock)
Where Fk_AddressId=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_RemainingEnqForAllotment]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SP_Get_AddressForOrder 
Alter PROCEDURE [dbo].[SP_Get_RemainingEnqForAllotment]    
@teamid int
AS    
begin  
 declare @dept as varchar(30)
set @dept= (select Department from Tbl_TeamMaster
where Pk_TeamId=@teamid)

if(upper(@dept)='ORDER')
begin
SELECT distinct Pk_AddressID,a.Name,a.EnqNo 
FROM Address_Master a (nolock)
left join Tbl_OrderOneMaster o (nolock)
on a.Pk_AddressId=o.Fk_AddressId
where a.EnqStatus=1 and a.TypeOfEnq='OC' and Convert(varchar(20),o.DispatchDate,112) like '19000101'
and Pk_AddressID not in (select Fk_AddressID from Tbl_UserAllotmentDetail)
    end
    else if(upper(@dept)='SERVICE')
    begin
    SELECT distinct Pk_AddressID,a.Name,a.EnqNo 
FROM Address_Master a     
left join Enq_FollowDetails ef    
on ef.Fk_AddressID=Pk_AddressID    
LEFT JOIN Enq_VisitorDetail vf  
on vf.FK_AddressID=Pk_AddressID
inner join Tbl_OrderOneMaster om
on om.Fk_AddressId=Pk_AddressID  
where a.EnqStatus=1 and (a.TypeOfEnq='OC' and Convert(varchar(20),om.DispatchDate,112) not like '19000101') or  (a.TypeOfEnq='OLC')
and Pk_AddressID not in (select Fk_AddressId from Tbl_UserAllotmentDetail )
    end
    else 
    begin
    SELECT Pk_AddressID,Name,EnqNo
 FROM Address_Master am         

where am.EnqStatus=1 and upper(am.TypeOfEnq)<>'OC' and upper(am.TypeOfEnq)<>'OB' and upper(am.TypeOfEnq)<>'OL' and upper(am.TypeOfEnq)<>'REGRET' and UPPER(am.TypeOfEnq)<>'POSTPOND'  
and Pk_AddressID not in (select Fk_AddressID from Tbl_UserAllotmentDetail)
    end
    
    
  end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_RawMaterialIdFromName]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_RawMaterialIdFromName]
@Name nvarchar(200)
AS
	
select top 1 Pk_RowMaterialId from Tbl_RowMaterialMaster rm (nolock)
where RowMaterialName=@Name
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_PartyCredit_PaymentList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_PartyCredit_PaymentList]
as
Begin

select  distinct PType from dbo.Party_CreditDetail
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderTentativeDispatchDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderTentativeDispatchDetails]  
@Start datetime,        
@End datetime,  
@user int      
AS         
begin          
  
  if @user = 0 
	set @user = null 
	       
  select addr.Pk_AddressID,addr.Name,addr.EnqNo from dbo.Address_Master (nolock) as addr      
 inner join Tbl_OrderOneMaster (nolock) as o on o.Fk_AddressId=addr.Pk_AddressID       
 inner join Tbl_ProjectInformationMaster (nolock) as pm on pm.Fk_AddressId=addr.Pk_AddressID  
  where  addr.EnqStatus=1 and addr.TypeOfEnq='OC' and Convert(varchar(20),o.DispatchDate,112) like '19000101' and  convert(varchar,pm.TentativeDispatch,112) not like  '19000101' and  
  convert(varchar,pm.TentativeDispatch,112) between convert(varchar,@Start,112) and  convert(varchar,@End,112)           
  and addr.Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID=ISNULL(@user,Fk_Userid))  
  
 order by addr.Pk_AddressID desc        
  
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderReport_ISIProcessList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderReport_ISIProcessList]


as
Begin
select addr.Name,addr.City as Station,ISI_M.Scheme_Name,ISI_M.F_Submit_P,ISI_M.BIS_Insp_Date, ISI_D.ByWhom,ISI_D.FDate,ISI_D.NFDate,ISI_D.Remarks from dbo.Tbl_ISIProcessMaster_Two as ISI_M
inner join dbo.Tbl_ISIProcess_DetailMaster_Two aS ISI_D on ISI_M.Fk_AddressId=ISI_D.Fk_AddressId
inner join dbo.Address_Master as addr on ISI_M.Fk_AddressId=addr.Pk_AddressID
where addr.EnqStatus=1
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderReceiveDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderReceiveDetails]              
           
@Start datetime,            
@End datetime,      
@user int            
AS             
          
        
begin              
  
  if @user = 0
	set @user =null
         
            
  select addr.Pk_AddressID,addr.Name,addr.EnqNo from dbo.Address_Master (nolock) as addr          
 inner join Tbl_OrderOneMaster (nolock) as o on o.Fk_AddressId=addr.Pk_AddressID       
     left join Enq_FollowDetails (nolock) as fl on fl.Fk_AddressID=addr.Pk_AddressID      
  where  addr.EnqStatus=1 and addr.TypeOfEnq='OC'   
   and      
  o.OrderRecFromMkt > = @Start and o.OrderRecFromMkt < = @End  
  and addr.Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID=isnull(@user,Fk_UserId))      
      
 order by addr.Pk_AddressID desc            
      
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderPlantDrawingPrint]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderPlantDrawingPrint]        
@Fk_Address as  bigint,
@Type as nvarchar(30)
AS       
    
  -- SP_Get_OrderPlantDrawingPrint 3, '1'
begin        
     
if(@Type='1')  
begin 
 select Draw1Image1 as 'image',Draw1Val1 as 'name',Draw1Val2 as 'detail' from Tbl_OrderPlanDrawing (nolock)
 where Fk_AddressId=@Fk_Address 
 end
 if(@Type='2')
 begin
  select Draw2Image2 as 'image',Draw2Val1 as 'name',Draw2Val2 as 'detail'  from Tbl_OrderPlanDrawing (nolock)
 where Fk_AddressId=@Fk_Address 

 end
 if(@Type='3')
 begin
 select Draw3Image3 as 'image',Draw3Val1 as 'name',Draw3Val2 as 'detail' from Tbl_OrderPlanDrawing (nolock)
 where Fk_AddressId=@Fk_Address 
 end
 

 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderManager_HotList_Report]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderManager_HotList_Report]
as
begin
	
	declare @Qry as varchar(500)
	
	select ROW_NUMBER() OVER (ORDER BY Project_Info.Fk_AddressId) AS SrNo, 
	Addr.Name,Addr.City as Station,Project_Info.Capacity,Project_Info.Model,Packi_D.Lab,Packi_D.JarWashing,
	Packi_D.Pouch,Packi_D.Blow,Packi_D.BatchCoding,Packi_D.PackBulk,Packi_D.Glass,Packi_D.Chiller,Packi_D.Soda ,OD.OrderStatus 
	into #SP_Get_OrderManager_HotList_Report from Address_Master as Addr
	inner join  dbo.Tbl_ProjectInformationMaster as Project_Info on Addr.Pk_AddressID=Project_Info.Fk_AddressId 
	inner join dbo.Tbl_PackagingDetail as Packi_D on Project_Info.Fk_AddressId=Packi_D.Fk_AddressId
	left outer join dbo.Tbl_OrderOneMaster OD on Project_Info.Fk_AddressId=Od.Fk_AddressId
		
	--update #SP_Get_OrderManager_HotList_Report set Capacity =trn.cnt  
	--from (select COUNT(*) as cnt from #SP_Get_OrderManager_HotList_Report
	--where capacity <> '') as trn

	--IF EXISTS (SELECT * FROM #SP_Get_OrderManager_HotList_Report) 
	--Begin
	
	--	IF  EXISTS (SELECT * FROM sys.objects 
	--			WHERE object_id = OBJECT_ID(N'[dbo].[SP_Get_OrderManager_HotList_Report1]') AND type in (N'U'))
	--		BEGIN
	--			Drop table SP_Get_OrderManager_HotList_Report1
	--		END
		
	--	CREATE TABLE SP_Get_OrderManager_HotList_Report1          
	--	( 
	--		srno bigint
	--	) 
		
	--	insert into SP_Get_OrderManager_HotList_Report1 (srno) values(null)
		 
	--	DECLARE @name VARCHAR(50)
	--	DECLARE tempCursor CURSOR
	--	FOR select name  from tempdb.sys.columns where object_id = object_id('tempdb..#SP_Get_OrderManager_HotList_Report');
	--	OPEN tempCursor

	--	FETCH NEXT FROM tempCursor 
	--	INTO @name
		
	--	WHILE @@FETCH_STATUS = 0
	--	BEGIN
			
	--		if @name <> 'srno'
	--		begin
			
	--			set @Qry = ''
	--			set @Qry = ' ALTER TABLE SP_Get_OrderManager_HotList_Report1 	ADD ' +  @name + ' Varchar(50) '
	--			exec(@Qry) 
	
	--			if UPPER(@name) <> upper('name') and UPPER(@name) <> upper('Station') and  UPPER(@name) <> upper('Capacity')
	--				begin
	--					set @Qry = ''
	--					set @Qry = ' update SP_Get_OrderManager_HotList_Report1 set ' + @name + '  =trn.cnt   '
	--					set @Qry = @Qry + ' from (select COUNT(*) as cnt from #SP_Get_OrderManager_HotList_Report where ' +  @name + ' <> '''') as trn '
	--				end 
	--			else
	--				begin
	--					if UPPER(@name) = upper('name')
	--						begin
	--							set @Qry = ''
	--							set @Qry = ' update SP_Get_OrderManager_HotList_Report1 set ' + @name + '  =''Total'''
	--						end 
	--					else
	--						begin
	--							set @Qry = ''
	--							set @Qry = ' update SP_Get_OrderManager_HotList_Report1 set ' + @name + '  ='''''
	--						end 
	--				end 
	--			exec(@Qry) 
	--		end 
			
	--		FETCH NEXT FROM tempCursor 
	--		INTO @name

	--	END 
	--	close tempCursor
	--	DEALLOCATE   tempCursor
	--end 
	
	select * from #SP_Get_OrderManager_HotList_Report
	--union all
	--select * from SP_Get_OrderManager_HotList_Report1
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderGeneralReport_List]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderGeneralReport_List]

@criteria varchar(max)
as

Begin
DECLARE @QRY VARCHAR(max);




select order1.PartyName,addr.City,addr.State,addr.MobileNo,addr.EmailID , project.PlantName,project.Capacity,order1.OrderDate,project.DType,order1.DispatchDate as Dispatchstatus, order1.OrderStatus from dbo.Address_Master as addr 
inner join dbo.Tbl_OrderOneMaster as order1 on addr.Pk_AddressID=order1.Fk_AddressId
inner join  dbo.Tbl_ProjectInformationMaster as project on project.Fk_AddressId =addr.Pk_AddressID

            

print (@Qry);

end
--exec SP_Get_OrderGeneralReport_List addr.City in (SELECT value FROM dbo.fn_Split('NALNDA',','))
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderFollowDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderFollowDetails]               
@Start datetime,          
@End datetime,    
@user int          
AS           
        
 create table #Enq_FollowDetails        
  (        
   Next_FollowpDate datetime,        
   Fk_AddressID int         
  )        
begin            
         
  if @user =0
	set @user = null
	        
  insert  into #Enq_FollowDetails select MAX(NFDate),Fk_AddressID from Tbl_OrderFollowupDetail (nolock)      
  group by Fk_AddressID        
          
          
  select addr.Pk_AddressID,addr.Name,addr.EnqNo from dbo.Address_Master (nolock) as addr        
  inner join #Enq_FollowDetails as inq on addr.Pk_AddressID=inq.Fk_AddressID     
  left join Tbl_OrderOneMaster (nolock) as o on o.Fk_AddressId=addr.Pk_AddressID         
  where  addr.EnqStatus=1 and addr.TypeOfEnq='OC' and Convert(varchar(20),o.DispatchDate,112) like '19000101'  
   and Next_FollowpDate > =@Start and Next_FollowpDate < =@End  
   and addr.Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID=isnull(@user,Fk_UserId))    
 order by inq.Fk_AddressID desc          
            
      
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_OrderDispatchDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_OrderDispatchDetails]          
       
@Start datetime,        
@End datetime,  
@user int  
AS         
      
    
begin          
       
  if @user = 0
	set @user = null  
        
  select addr.Pk_AddressID,addr.Name,addr.EnqNo from dbo.Address_Master (nolock) as addr  
 inner join Tbl_OrderOneMaster (nolock) as o on o.Fk_AddressId=addr.Pk_AddressID       
  where  addr.EnqStatus=1 and addr.TypeOfEnq='OC' and convert(varchar,o.DispatchDate,112) between convert(varchar,@Start,112) and  convert(varchar,@End,112)  
  and addr.Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID=isnull(@user,Fk_UserId))  
 order by addr.Pk_AddressID desc        
  
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Order_FollowDetailsByCriteriaForDaily]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Order_FollowDetailsByCriteriaForDaily]    
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime   
AS    
   begin 
  
 select ROW_NUMBER() OVER (ORDER BY tmp.Pk_AddressId) AS 'No',tmp.* from (select distinct am.Name,isnull(am.Area,'') 'Station',fd.NFDate 'NextFollowUp',fd.ProjectType 'Type',fd.Remarks,fd.ByWhom 'FollowBy',am.Pk_AddressID from Address_Master am (nolock)
 inner join Tbl_OrderFollowupMaster fm (nolock)
 on am.Pk_AddressID=fm.FK_AddressID
 inner join Tbl_OrderFollowupDetail fd (nolock)
 on fm.FK_AddressID=fd.Fk_AddressID
 where  am.EnqStatus=1 and convert(varchar,FDate,112) >= convert(varchar,@ByFollowDate,112) and convert(varchar,FDate,112) <= convert(varchar,@NextFollowDate,112) and ByWhom like @byUser) as tmp
    
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Order_FollowDetailsByCriteria]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Order_FollowDetailsByCriteria]      
@byUser nvarchar(40),      
@ByFollowDate datetime,      
@NextFollowDate datetime     
AS      
   begin   
    
select ROW_NUMBER() OVER (ORDER BY tmp.Pk_AddressId) AS 'No',tmp.* from (select distinct fd.FDate 'FollowUp',am.Name,isnull(am.Area,'') 'Station',am.MobileNo 'Mobile',fd.ProjectType 'Type',fd.ByWhom 'FollowBy',fd.Remarks,am.Pk_AddressID from Address_Master am (nolock)  
 inner join Tbl_OrderFollowupMaster fm (nolock)  
 on am.Pk_AddressID=fm.FK_AddressID  
 inner join Tbl_OrderFollowupDetail fd (nolock)  
 on fm.FK_AddressID=fd.Fk_AddressID    
  where  am.EnqStatus=1 and convert(varchar(10),fd.NFDate,112) >= convert(varchar(10),@ByFollowDate,112) and convert(varchar(10),fd.NFDate,112)<= convert(varchar(10),@NextFollowDate,112) and ByWhom like @byUser) as tmp  
     
     
     
       
     
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ISIProcessListReport]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_ISIProcessListReport]


as
Begin
select addr.Name,addr.City as Station,ISI_M.Scheme_Name,ISI_M.F_Submit_P,ISI_M.BIS_Insp_Date, ISI_D.ByWhom,ISI_D.FDate,ISI_D.NFDate,ISI_D.Remarks from dbo.Tbl_ISIProcessMaster_Two as ISI_M
inner join dbo.Tbl_ISIProcess_DetailMaster_Two aS ISI_D on ISI_M.Fk_AddressId=ISI_D.Fk_AddressId
inner join dbo.Address_Master as addr on ISI_M.Fk_AddressId=addr.Pk_AddressID
where addr.EnqStatus=1
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ISI_FollowDetailsByCriteriaForDaily]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_ISI_FollowDetailsByCriteriaForDaily]    
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime   
AS    
begin 
   
select ROW_NUMBER() OVER (ORDER BY tmp.Pk_AddressId) AS 'No',tmp.* from (select distinct id.FDate 'F.Date',am.Name,isnull(am.Area,'') 'Station',om.OrderNo,id.ProjectType as 'Project Details',am.MobileNo 'Mobile',am.Pk_AddressID from Address_Master am (nolock)
inner join Tbl_OrderOneMaster om (nolock)
on om.Fk_AddressId=am.Pk_AddressID
 inner join Tbl_ISIProcessMaster_Two im (nolock)
 on am.Pk_AddressID=im.FK_AddressID
 inner join Tbl_ISIProcess_DetailMaster_Two id (nolock)
 on im.FK_AddressID=id.Fk_AddressID 
 where am.EnqStatus=1 and convert(varchar,id.FDate,112) >= convert(varchar,@ByFollowDate,112) and convert(varchar,id.FDate,112) <= convert(varchar,@NextFollowDate,112) and id.ByWhom like @byUser) as tmp
    
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ISI_FollowDetailsByCriteria]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_ISI_FollowDetailsByCriteria]    
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime   
AS    
   begin 
  
select ROW_NUMBER() OVER (ORDER BY tmp.Pk_AddressId) AS 'No',tmp.* from (select distinct id.FDate 'F.Date',am.Name,isnull(am.Area,'') 'Station',om.OrderNo,id.ProjectType as 'Project Details',am.MobileNo 'Mobile',am.Pk_AddressID from Address_Master am (nolock)
inner join Tbl_OrderOneMaster om (nolock)
on om.Fk_AddressId=am.Pk_AddressID
 inner join Tbl_ISIProcessMaster_Two im (nolock)
 on am.Pk_AddressID=im.FK_AddressID
 inner join Tbl_ISIProcess_DetailMaster_Two id (nolock)
 on im.FK_AddressID=id.Fk_AddressID  
  where am.EnqStatus=1 and convert(varchar,id.NFDate,112) >= convert(varchar,@ByFollowDate,112) and convert(varchar,id.NFDate,112)<= convert(varchar,@NextFollowDate,112) and ByWhom like @byUser) as tmp
   
   
   
     
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_FollowDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_FollowDetails]      
@byUser nvarchar(40),      
@Start datetime,    
@End datetime    
AS     
  
 create table #Enq_FollowDetails  
  (  
   Next_FollowpDate datetime,  
   Fk_AddressID int   
  )  
begin      
   
    
  insert  into #Enq_FollowDetails select MAX(N_F_FollowpDate),Fk_AddressID from dbo.Enq_FollowDetails  
  group by Fk_AddressID  
    
    
  select addr.Pk_AddressID,addr.Name,addr.EnqNo,isnull((select COUNT(*) from Enq_FollowDetails (nolock) where Fk_AddressID=addr.Pk_AddressID),0) 'Status'  from dbo.Address_Master as addr  (nolock)
inner join #Enq_FollowDetails as inq on addr.Pk_AddressID=inq.Fk_AddressID  
  
  
  
    
  where addr.EnqStatus=1 and convert(varchar,Next_FollowpDate,112) between convert(varchar,@Start,112) and  convert(varchar,@End,112)       
    and addr.Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail where Fk_UserID=@byUser)

 order by Fk_AddressID desc    
      
   return  
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_EnqTypeList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_EnqTypeList]

AS


select * from Enq_Type (nolock)
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_EnqTypeAllByType]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_EnqTypeAllByType]
	
AS
	
	begin

	select EnqType from Enq_Type (nolock)
	
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_EnqTypeAll]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_EnqTypeAll]
	
AS
	
	begin

	select SubCategory from AddressSubCategory_Master (nolock)
	where FK_AddCatID=1
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_EnqStatusList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_EnqStatusList]
	
AS
	
	begin

	select * from Enq_Status (nolock)
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_WaterMasterListByID]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_WaterMasterListByID]
@Fk_AddressId bigint
AS
begin

select * from Enq_WaterMaster (nolock)
where Fk_AddressId=@Fk_AddressId
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_WaterMasterList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_WaterMasterList]
AS
begin

select * from Enq_WaterMaster (nolock)
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_VisitorMasterListById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_VisitorMasterListById]
@Fk_AddressId bigint
AS
	select * from Enq_VisitorMaster (nolock)
	where Fk_AddressID=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_VisitorMasterList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_VisitorMasterList]
	
AS
	select * from Enq_VisitorMaster (nolock)
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_VisitorDetailsListById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_VisitorDetailsListById]
@Fk_AddressId bigint
AS
	select * from Enq_VisitorDetail (nolock)
where FK_AddressID=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_VisitorDetailsList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_VisitorDetailsList]
	
AS
	select * from Enq_VisitorDetail (nolock)
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_Report_EnqTypeList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_Report_EnqTypeList]  
@StartDate datetime,  
@EndDate Datetime,  
@OldStatus varchar(50),  
@NewStatus varchar(50)  
As  
Begin  
  
select distinct ROW_NUMBER() OVER(ORDER BY final.Pk_AddressId) as 'SrNO', final.Name,final.Station,final.EnqNo,final.EnqType,final.Pk_AddressID,final.F_Date,final.Remarks from  
(  
  select distinct addr_M.Pk_AddressID, addr_M.Name,addr_M.City as Station,addr_M.EnqNo, Enq_FD1.EnqType, Enq_FD1.F_Date,Enq_FD1.Remarks from dbo.Address_Master as addr_M  
  inner join dbo.Enq_FollowDetails as Enq_FD1 on addr_M.Pk_AddressID=Enq_FD1.Fk_AddressID  
  where  (Enq_FD1.EnqType=@OldStatus or Enq_FD1.EnqType=@NewStatus) and  Enq_FD1.F_Date >= CONVERT(varchar(10), @StartDate,112) and  Enq_FD1.F_Date <=  CONVERT(varchar(10), @EndDate,112)  
 ) as final where final.Pk_AddressID in(select Distinct Enq_FD.Fk_AddressID as Fk_AddressID from dbo.Enq_FollowDetails as Enq_FD  
 where Enq_FD.EnqType=@NewStatus and(Enq_FD.F_Date >= CONVERT(varchar(10), @StartDate,112) and Enq_FD.F_Date <= CONVERT(varchar(10), @EndDate,112))) order by final.Pk_AddressID
    
  End
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_References]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_References]
AS
begin

select distinct Reference from Enq_EnqMaster (nolock)
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowMasterListByID]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowMasterListByID]
@Fk_AdressId bigint 

AS
	select * from Enq_FollowMaster (nolock)
	Where FK_AddressID=@Fk_AdressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowMasterList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowMasterList]
	
AS
	select * from Enq_FollowMaster (nolock)
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowDetailsListById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowDetailsListById]
@Fk_AddressID bigint
AS
	select * from Enq_FollowDetails (nolock)
	where Fk_AddressID=@Fk_AddressID
	
	order by F_Date desc
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowDetailsList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowDetailsList]
	
AS
	select * from Enq_FollowDetails (nolock)
	order by F_Date desc
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowDetailsByCriteriaForDailyByUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowDetailsByCriteriaForDailyByUser]      
@byUser nvarchar(200),      
@ByFollowDate datetime,      
@NextFollowDate datetime,      
@Type varchar(50)      
AS      
  begin  
 if(@byUser='All' or @byUser='') 
 begin  
        print @ByFollowDate
        print @NextFollowDate
		 select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS 'No',am.Name,isnull(am.Area,'') 'Station',fd.N_F_FollowpDate 'NextFollowUp',fd.EnqType 'EnqType',fd.Remarks,fd.ByWhom 'FollowBy',am.Pk_AddressID from Address_Master am (nolock)    
		 inner join Enq_FollowDetails fd (nolock)  
		 on am.Pk_AddressID=fd.Fk_AddressID  
		 where am.EnqStatus=1 and  convert(varchar(10),F_Date,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),F_Date,112)<=convert(varchar(10),@NextFollowDate,112)  -- and ByWhom like @byUser  
    end  
    else  
    begin  
	     select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS 'No',am.Name,isnull(am.Area,'') 'Station',fd.N_F_FollowpDate 'NextFollowUp',fd.EnqType 'EnqType',fd.Remarks,fd.ByWhom 'FollowBy',am.Pk_AddressID from Address_Master am (nolock)  		  
		 inner join Enq_FollowDetails fd (nolock)  
		 on am.Pk_AddressID=fd.Fk_AddressID  
		 where am.EnqStatus=1 and  convert(varchar(10),F_Date,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),F_Date,112)  <=convert(varchar(10),@NextFollowDate,112) 
		and ByWhom in (SELECT value FROM dbo.fn_Split(@byUser,','))  
  
    end  
     
end

--exec SP_Get_Enq_FollowDetailsByCriteriaForDailyByUser 'ALL', '08/11/2013','11/25/2013','a'
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowDetailsByCriteriaForDaily]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowDetailsByCriteriaForDaily]    
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime,    
@Type varchar(50)    
AS    
   begin 
  
 select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS 'No',am.Name,isnull(am.Area,'') 'Station',fd.N_F_FollowpDate 'NextFollowUp',fd.EnqType 'EnqType',fd.Remarks,fd.ByWhom 'FollowBy',am.Pk_AddressID from Address_Master am (nolock)

 inner join Enq_FollowDetails fd (nolock)
 on am.Pk_AddressID=fd.Fk_AddressID
 where am.EnqStatus=1 and  convert(varchar,F_Date,112) between convert(varchar,@ByFollowDate,112) and convert(varchar,@NextFollowDate,112) and ByWhom like @byUser
    
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowDetailsByCriteriaByUserByUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowDetailsByCriteriaByUserByUser]      
@byUser nvarchar(200),      
@ByFollowDate datetime,      
@NextFollowDate datetime,      
@Type varchar(50)      
AS      
  begin  
if(@byUser='All' or @byUser='')  
  begin   
    
	 select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS 'No',fd.F_Date 'FollowUp',am.Name,isnull(am.Area,'') 'Station',am.MobileNo 'Mobile',fd.EnqType 'EnqType',fd.ByWhom 'FollowBy',fd.Remarks,am.Pk_AddressID from Address_Master am (nolock)  
	 left join Enq_FollowMaster fm (nolock)  
	 on am.Pk_AddressID=fm.FK_AddressID  
	 inner join Enq_FollowDetails fd (nolock)  
	 on fm.FK_AddressID=fd.Fk_AddressID  	    
     where am.EnqStatus=1 and  convert(varchar(10),fd.N_F_FollowpDate,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),fd.N_F_FollowpDate,112)<=convert(varchar(10),@NextFollowDate,112) 
	 
--  and ByWhom like @byUser  
   end  
   else  
   begin  
	  
	 select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS 'No',fd.F_Date 'FollowUp',am.Name,isnull(am.Area,'') 'Station',am.MobileNo 'Mobile',fd.EnqType 'EnqType',fd.ByWhom 'FollowBy',fd.Remarks,am.Pk_AddressID from Address_Master am (nolock)  
	 left join Enq_FollowMaster fm (nolock)  
	 on am.Pk_AddressID=fm.FK_AddressID  
	 inner join Enq_FollowDetails fd (nolock)  
	 on fm.FK_AddressID=fd.Fk_AddressID  	    
	 where am.EnqStatus=1 and  convert(varchar(10),fd.N_F_FollowpDate,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),fd.N_F_FollowpDate,112)<=convert(varchar(10),@NextFollowDate,112) 
	 and ByWhom in (SELECT value FROM dbo.fn_Split(@byUser,','))  
   end  
  
     
     
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_FollowDetailsByCriteria]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_FollowDetailsByCriteria]    
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime,    
@Type varchar(50)    
AS    
   begin 
  
select ROW_NUMBER() OVER (ORDER BY am.Pk_AddressId) AS 'No',fd.F_Date 'FollowUp',am.Name,isnull(am.Area,'') 'Station',am.MobileNo 'Mobile',fd.EnqType 'EnqType',fd.ByWhom 'FollowBy',fd.Remarks,am.Pk_AddressID from Address_Master am (nolock)
 left join Enq_FollowMaster fm (nolock)
 on am.Pk_AddressID=fm.FK_AddressID
 inner join Enq_FollowDetails fd (nolock)
 on fm.FK_AddressID=fd.Fk_AddressID
  
  where am.EnqStatus=1 and convert(varchar,fd.N_F_FollowpDate,112) between convert(varchar,@ByFollowDate,112) and convert(varchar,@NextFollowDate,112) and ByWhom like @byUser
   

   
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_EnqMasterListByID]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_EnqMasterListByID]
@Fk_AddressId bigint

AS
begin

select * from Enq_EnqMaster (nolock)
where Fk_AddressId=@Fk_AddressId

	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_EnqMasterList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_EnqMasterList]
AS
begin

select * from Enq_EnqMaster (nolock)
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_ClientMasterListByID]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_ClientMasterListByID]
@Fk_AddressID bigint

AS
begin

select * from Enq_ClientMaster (nolock)
where FK_AddressID=@Fk_AddressID

	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_ClientMasterList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_ClientMasterList]
AS
begin

select * from Enq_ClientMaster (nolock)
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_BioDataMasterListByID]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_BioDataMasterListByID]
@Fk_AddressId bigint

AS
	select * from Enq_BioDataMaster (nolock)
where FK_AddressID=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Enq_BioDataMasterList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Enq_BioDataMasterList]
	
AS
	select * from Enq_BioDataMaster (nolock)
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_CourierList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_CourierList]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
SELECT        AddressSubCategory_Master.SubCategory, AddressCategory_Master.Category, Courier_Master.*
FROM            Courier_Master (nolock) INNER JOIN
                         AddressCategory_Master (nolock) ON Courier_Master.FK_CategoryID = AddressCategory_Master.Pk_AddressCategoryID INNER JOIN
                         AddressSubCategory_Master (nolock) ON Courier_Master.FK_SubCategoryID = AddressSubCategory_Master.Pk_AddressSubID
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Count_UserTeam]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Count_UserTeam]
-- exec SP_Get_Total_Inward 5,3
@Fk_TeamId int
AS

begin
declare @total as int;
set @total = (select COUNT(*) Total from User_Master (nolock)
where Fk_TeamId = @Fk_TeamId and Status='Head');

return @total;


	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Count_UserDesignation]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Count_UserDesignation]
-- exec SP_Get_Total_Inward 5,3
@Designation nvarchar(50)
AS

begin
declare @total as int;
set @total = (select COUNT(*) Total from User_Master (nolock)
where Fk_TeamId = @Designation and Status='Head');

return @total;


	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ComplainDetails]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_ComplainDetails]      
@ComplainNo nvarchar(50)  
AS     
  
 create table #Complain_FollowDetails  
  (  
   FollowUp nvarchar(100),
   TentativeADate datetime,
   Status nvarchar(50),
   Remarks nvarchar(50),
   Fk_AddressID bigint   
  )  
begin      
   
    
  insert  into #Complain_FollowDetails select FollowUp,MAX(TentativeADate) as 'DoneDate',Status,Remarks,Fk_AddressID from dbo.Tbl_Service_GeneralServiceDetail  

  group by FollowUp,Status,Remarks,Fk_AddressID 
	
  
    
    
  select top 1 complainmaster.ServiceType,complainmaster.Engineer,complainmaster.GSDate 'ComplainDate',complainmaster.AttendDate,complainmaster.AttendBy,complainmaster.Engineer,complaindetail.FollowUp,complaindetail.Remarks,complaindetail.Status 'ComlainStatus',complaindetail.TentativeADate 'DoneDate' from Tbl_Service_GeneralServiceMaster complainmaster
  inner join #Complain_FollowDetails as complaindetail on complainmaster.Fk_AddressId=complaindetail.Fk_AddressID    
  
  where ComplainNo=@ComplainNo
order by DoneDate desc
      
     return
 end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Card_FollowDetailsByCriteriaForDaily]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_Card_FollowDetailsByCriteriaForDaily]    
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime   
AS    
begin 
   
select ROW_NUMBER() OVER (ORDER BY tmp.Pk_AddressId) AS 'No',tmp.* from (select distinct ld.LDate 'F.Date',am.Name,isnull(am.Area,'') 'Station',om.OrderNo,lm.ProjectDetail as 'Project Details',am.MobileNo 'Mobile',ld.Card_Date 'Card Date',ld.Mail_Send 'Mail (S)',am.Pk_AddressID from Address_Master am (nolock)
inner join Tbl_OrderOneMaster om (nolock)
on om.Fk_AddressId=am.Pk_AddressID
 inner join Tbl_LetterMailComMaster_Two lm (nolock)
 on am.Pk_AddressID=lm.FK_AddressID
 inner join Tbl_LetterMailComMaster_Detail_Two ld (nolock)
 on am.Pk_AddressID=ld.FK_AddressID
 
 where convert(varchar,ld.LDate,112) >= convert(varchar,@ByFollowDate,112) and convert(varchar,ld.LDate,112) <= convert(varchar,@NextFollowDate,112) and ld.BY_Whom like @byUser) as tmp
    
   
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AllotedUserForAddress]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AllotedUserForAddress]
@Fk_AddressId bigint
AS
select isnull(ud.Fk_DesignationId,'') 'Fk_TeamId',ISNULL(Fk_UserId,'') 'Fk_UserId' from Tbl_UserAllotmentDetail ud (nolock)
where ud.Fk_AddressId=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AllotedUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AllotedUser]
@Fk_AddressId bigint
AS
select top 1 isnull(UserName,'') 'EnqAllotedTo' from Address_Master am (nolock)
inner join Tbl_UserAllotmentDetail ud (nolock)
on am.Pk_AddressID=ud.Fk_AddressId
inner join User_Master um (nolock)

on um.Pk_UserId=ud.Fk_UserId
where am.Pk_AddressID=@Fk_AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AllotedTeamUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AllotedTeamUser]
@AddressId	bigint
AS
select isnull(um.Fk_TeamId,'') 'Fk_TeamId',ISNULL(Fk_UserId,'') 'Fk_UserId'  from Tbl_UserAllotmentDetail ud (nolock)
inner join User_Master um (nolock)
on um.Pk_UserId=ud.Fk_UserId
where Fk_AddressId=@AddressId
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AllotedStateTeam]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AllotedStateTeam]
@StateName nvarchar(50)
AS
select isnull(Fk_TeamId,'') 'Fk_TeamId',ISNULL(Fk_UserId,'') 'Fk_UserId'  from Tbl_StateTeamAllotment st
where st.StateName=@StateName
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_All_AddressList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_All_AddressList]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
SELECT Pk_AddressID,Name,EnqNo

FROM Address_Master  (nolock)

where EnqNo<>'' and EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddSubCategory]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddSubCategory]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	SELECT        AddressSubCategory_Master.*, AddressCategory_Master.Category, AddressSubCategory_Master.SubCategory AS Expr1
FROM            AddressSubCategory_Master (nolock) INNER JOIN
                         AddressCategory_Master  (nolock) ON AddressSubCategory_Master.FK_AddCatID = AddressCategory_Master.Pk_AddressCategoryID
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListForQuatation]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListForQuatation]
	@type int
AS
if(@type=1)
begin

SELECT Pk_AddressID,EnqNo,Name,Address
FROM Address_Master (NOLOCK) 
where Address_Master.Pk_AddressID not in 
(select isnull(Fk_AddressId,0) from Quotation_Master) 
and Address_Master.EnqStatus=1
order by Pk_AddressID desc
end
else
begin
SELECT Pk_AddressID,EnqNo,Name,Address
FROM Address_Master (NOLOCK) 
where Address_Master.EnqStatus=1
order by Pk_AddressID desc
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListByUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListByUser]    
@user varchar(10),
@criteria nvarchar(1000)    
AS    
declare @id1 as int;
declare @id2 as int;
declare @id3 as int;
declare @Qry varchar(1500);       
set @Qry=''   
set @id1=(select Pk_UserId from User_Master (nolock) where UserName='RK')
set @id2=(select Pk_UserId from User_Master (nolock) where UserName='SM')




   
set @id3=(select Pk_UserId from User_Master (nolock) where UserName='PP')
  
  
if(@User='0' or @User=@id1 or @User=@id2 or @User=@id3)      
begin    
    
set @Qry='SELECT Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails (nolock) where Fk_AddressID=Pk_AddressID),0) ''Status''
FROM Address_Master (NOLOCK) where Address_Master.EnqStatus=1' + @criteria;      
         
exec (@Qry);  
end    
else    
begin    

declare @status as varchar(50);
set @status=(select Status from User_Master where Pk_UserId=@user);
declare @team as varchar(50)
set @team=(Select Fk_TeamId from User_Master where Pk_UserId=@User);

if(@status='Head')
begin

set @Qry='SELECT  Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails (nolock) where Fk_AddressID=Pk_AddressID),0) ''Status'' 
FROM Address_Master (NOLOCK) where Address_Master.EnqStatus=1 
and Pk_AddressID in (select Fk_AddressID  from Tbl_UserAllotmentDetail (nolock)
 where Fk_DesignationId in (select Fk_TeamId from User_Master (nolock) where Fk_TeamId='+@team+'))' + @criteria;      
         
exec (@Qry);  
end
else
begin

set @Qry='SELECT  Pk_AddressID,Name,EnqNo,isnull((select COUNT(*) from Enq_FollowDetails (nolock) where Fk_AddressID=Pk_AddressID),0) ''Status'' 
FROM Address_Master (NOLOCK) where Address_Master.EnqStatus=1 
and Pk_AddressID in (select Fk_AddressID from Tbl_UserAllotmentDetail (nolock) where Fk_UserID='+@User+')'+ @criteria;      
         
exec (@Qry);  


end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListByStatus]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListByStatus]
	@EnqStatus varchar(50)
AS
SELECT Enq_Type.EnqType, Enq_Type.Code, AddressCategory_Master.Category, AddressSubCategory_Master.SubCategory, Address_Master.*
FROM Address_Master INNER JOIN
Enq_Type ON Address_Master.FK_EnqTypeID = Enq_Type.Pk_EnqTypeID INNER JOIN
AddressCategory_Master ON Address_Master.FK_CategoryID = AddressCategory_Master.Pk_AddressCategoryID INNER JOIN
AddressSubCategory_Master ON Address_Master.FK_SubCategoryID = AddressSubCategory_Master.Pk_AddressSubID
where EnqStatus=@EnqStatus
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListByNameForOrder]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListByNameForOrder]
	@name nvarchar(100)
	AS
SELECT Pk_AddressID
FROM Address_Master (NOLOCK)
where Name like @name and Address_Master.EnqStatus=1 and TypeOfEnq='OC'
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListByName]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListByName]
	@name nvarchar(100)
	AS
SELECT        Pk_AddressID
FROM            Address_Master (NOLOCK)

where Name like @name and Address_Master.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListById]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListById]
	@Address bigint
AS
SELECT  Enq_Type.EnqType, Enq_Type.Code, AddressCategory_Master.Category, AddressSubCategory_Master.SubCategory, Address_Master.*
FROM  Address_Master (NOLOCK) INNER JOIN
 Enq_Type (nolock) ON Address_Master.FK_EnqTypeID = Enq_Type.Pk_EnqTypeID INNER JOIN
AddressCategory_Master (nolock) ON Address_Master.FK_CategoryID = AddressCategory_Master.Pk_AddressCategoryID INNER JOIN
   AddressSubCategory_Master (nolock) ON Address_Master.FK_SubCategoryID = AddressSubCategory_Master.Pk_AddressSubID
where Pk_AddressID=@Address and Address_Master.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListByEnqNo]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListByEnqNo]
	@EnqNo varchar(50)
AS
SELECT        Enq_Type.EnqType, Enq_Type.Code, AddressCategory_Master.Category, AddressSubCategory_Master.SubCategory, Address_Master.*
FROM            Address_Master (NOLOCK) INNER JOIN
                         Enq_Type (nolock) ON Address_Master.FK_EnqTypeID = Enq_Type.Pk_EnqTypeID INNER JOIN
                         AddressCategory_Master (nolock) ON Address_Master.FK_CategoryID = AddressCategory_Master.Pk_AddressCategoryID INNER JOIN
                         AddressSubCategory_Master (nolock) ON Address_Master.FK_SubCategoryID = AddressSubCategory_Master.Pk_AddressSubID
where EnqNo=@EnqNo and Address_Master.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListAutoCompleteForOrder]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListAutoCompleteForOrder]
@type nvarchar(20)
AS
begin
if(@type='Name')
begin
select distinct isnull(Name,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1 and TypeOfEnq='OC'
end
if (@type='City')
begin
select distinct isnull(City,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1 and TypeOfEnq='OC'
end
if (@type='State')
begin
select distinct isnull(StateName,'') as Result from Tbl_StateTeamAllotment (nolock)
end
if (@type='Area')
begin
select distinct isnull(Area,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1 and TypeOfEnq='OC'
end
if (@type='District')
begin
select distinct isnull(District,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1 and TypeOfEnq='OC'
end
if (@type='EnqNo')
begin
select distinct isnull(EnqNo,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1 and TypeOfEnq='OC'
end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListAutoCompleteByType]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListAutoCompleteByType]
@type nvarchar(20),
@EnqType nvarchar(50)
AS
begin
if(@type='Name')
begin
select DISTINCT isnull(Name,'') as Result from Address_Master as am (NOLOCK) inner join Enq_Type e (NOLOCK)
on e.Pk_EnqTypeID=am.FK_EnqTypeID  where am.EnqStatus=1 and e.EnqType=@EnqType
end

if (@type='EnqNo')
begin
select distinct isnull(EnqNo,'') as Result from Address_Master am (NOLOCK) inner join Enq_Type e (NOLOCK)
on e.Pk_EnqTypeID=am.FK_EnqTypeID  where am.EnqStatus=1 and e.EnqType=@EnqType
end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListAutoCompleteByForAllotment]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListAutoCompleteByForAllotment]
@type nvarchar(20)
AS
begin
if(@type='Name')
begin
select DISTINCT isnull(Name,'') as Result from Address_Master as am (NOLOCK) 
where am.Pk_AddressID in(select Fk_AddressId from Tbl_UserAllotmentDetail where Fk_UserId=0 or Fk_UserId is null)

end

if (@type='EnqNo')
begin
select distinct isnull(EnqNo,'') as Result from Address_Master am (NOLOCK) 
where am.Pk_AddressID in(select Fk_AddressId from Tbl_UserAllotmentDetail where  Fk_UserId=0 or Fk_UserId is null)

end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressListAutoComplete]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressListAutoComplete]
@type nvarchar(20)
AS
begin
if(@type='Name')
begin
select distinct isnull(Name,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1
end
if (@type='City')
begin
select distinct isnull(City,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1
end
if (@type='State')
begin
select distinct isnull(StateName,'') as Result from Tbl_StateTeamAllotment (nolock)
end
if (@type='Area')
begin
select distinct isnull(Area,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1
end
if (@type='District')
begin
select distinct isnull(District,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1
end
if (@type='EnqNo')
begin
select distinct isnull(EnqNo,'') as Result from Address_Master am (NOLOCK)where am.EnqStatus=1
end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_AddressList]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Get_AddressList]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
SELECT Enq_Type.EnqType, Enq_Type.Code, AddressCategory_Master.Category,
 AddressSubCategory_Master.SubCategory, Address_Master.*
FROM Address_Master (NOLOCK) INNER JOIN
Enq_Type (NOLOCK) ON Address_Master.FK_EnqTypeID = Enq_Type.Pk_EnqTypeID 
INNER JOIN AddressCategory_Master (NOLOCK) ON Address_Master.FK_CategoryID = AddressCategory_Master.Pk_AddressCategoryID INNER JOIN
AddressSubCategory_Master (NOLOCK) ON Address_Master.FK_SubCategoryID = AddressSubCategory_Master.Pk_AddressSubID
where Address_Master.EnqStatus=1
order by Pk_AddressID desc
GO
/****** Object:  StoredProcedure [dbo].[GetVisitorStatusCount]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetVisitorStatusCount]
AS
	select E_Type Type,COUNT(Pk_VisitorDetailID) Count from Enq_VisitorDetail (nolock)
group by E_Type
GO
/****** Object:  StoredProcedure [dbo].[getTodayInOutData]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[getTodayInOutData]
@Fk_Category bigint,
@Fk_Rawmaterial bigint,
@datefor datetime
as
begin

SELECT sp.OpeningStock,
  isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where Convert(varchar(20),ID.CreateDate,112)= Convert(varchar(20),@datefor,112) and ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)  AS TotInward,
isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where  Convert(varchar(20),od.CreateDate,112)= Convert(varchar(20),@datefor,112) and OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) AS TotOutward
,(isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where Convert(varchar(20),ID.CreateDate,112)= Convert(varchar(20),@datefor,112) and ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)+ isnull(InwardStock,0) + OpeningStock) - 
	(isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where Convert(varchar(20),od.CreateDate,112)= Convert(varchar(20),@datefor,112) and OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) + isnull(OutwardStock,0))as LatestCloseStock

  FROM [Tbl_Stock_ProductRegister] sp (nolock)
  inner join Tbl_ProductRegisterMaster cm (nolock)
  on sp.Fk_CategoryId=cm.Pk_ProductRegisterId
  inner join Tbl_RowMaterialMaster rm (nolock)
  on rm.Pk_RowMaterialId=sp.Fk_RawMaterialId
  where sp.Fk_CategoryId=@Fk_Category and sp.Fk_RawMaterialId=@Fk_Rawmaterial
end
GO
/****** Object:  StoredProcedure [dbo].[GetReInwardIdFromOutward]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetReInwardIdFromOutward]
@outwardid bigint
as
begin
select top 1 isnull(Pk_ReInwardId,0) as ReIwardId from Tbl_ReInwardMaster as RM
inner join Tbl_OutwardMaster om
on RM.Fk_OutwardId=om.Pk_OutwardId
where RM.Fk_OutwardId=@outwardid
end
GO
/****** Object:  StoredProcedure [dbo].[GetMaxOutwardId]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetMaxOutwardId]
as
begin
Declare @OutwardId as bigint;

if (select max(Pk_OutwardId) from Tbl_OutwardMaster) is null
Begin
set @OutwardId = 1;
End
Else
Begin
set @OutwardId = (select max(Pk_OutwardId) from Tbl_OutwardMaster);
set @OutwardId = @OutwardId + 1;
End
return @OutwardId
end
GO
/****** Object:  StoredProcedure [dbo].[GetMaxInwardId]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetMaxInwardId]
as
begin
Declare @InwardId as bigint;

if (select max(Pk_InwardId) from Tbl_InwardStockMaster) is null
Begin
set @InwardId = 1;
End
Else
Begin
set @InwardId = (select max(Pk_InwardId) from Tbl_InwardStockMaster);
set @InwardId = @InwardId + 1;
End
return @InwardId
end
GO
/****** Object:  StoredProcedure [dbo].[GetFollowUpCountTodayByUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetFollowUpCountTodayByUser]  
@byUser nvarchar(200),      
@ByFollowDate datetime,      
@NextFollowDate datetime      
  
AS  
begin  
if(@byUser='All' or @byUser='')  
  begin  
  
	select EnqType,COUNT(Pk_FollowDetailID) Count from Address_Master am (nolock)  
	inner join Enq_FollowDetails fd (nolock)  
	on am.PK_AddressID=fd.Fk_AddressID 	
	where am.EnqStatus=1 and  convert(varchar(10),N_F_FollowpDate,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),N_F_FollowpDate,112)<=convert(varchar(10),@NextFollowDate,112)group by EnqType   

  end  
  else  
  begin  
    
	 select EnqType,COUNT(Pk_FollowDetailID) Count from Address_Master am (nolock)  
	 inner join Enq_FollowDetails fd (nolock)  
	 on am.PK_AddressID=fd.Fk_AddressID  
     where am.EnqStatus=1 and  convert(varchar(10),N_F_FollowpDate,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),N_F_FollowpDate,112)<=convert(varchar(10),@NextFollowDate,112)
	 and ByWhom in (SELECT value FROM dbo.fn_Split(@byUser,','))
	 group by EnqType   
  end  
  end
GO
/****** Object:  StoredProcedure [dbo].[GetFollowUpCountToday]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetFollowUpCountToday]
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime    

AS


  
  select EnqType,COUNT(Pk_FollowDetailID) Count from Address_Master am (nolock)
 inner join Enq_FollowDetails fd (nolock)
 on am.PK_AddressID=fd.Fk_AddressID
	 where convert(varchar,N_F_FollowpDate,112) >= convert(varchar,@ByFollowDate,112) and convert(varchar,N_F_FollowpDate,112)<= convert(varchar,@NextFollowDate,112) 
	 and ByWhom like @byUser and am.EnqStatus=1     
    
group by EnqType
GO
/****** Object:  StoredProcedure [dbo].[GetFollowUpCountDailyByUser]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetFollowUpCountDailyByUser]  
@byUser nvarchar(200),      
@ByFollowDate datetime,      
@NextFollowDate datetime      
AS  
begin  
if(@byUser='All' or @byUser='')  
  begin  
  
	select DISTINCT fd.EnqType,Count(Pk_FollowDetailID) as Count from Address_Master am (nolock)  
	inner join Enq_FollowDetails fd (nolock)  on am.Pk_AddressID=fd.Fk_AddressID  
	where am.EnqStatus=1 and  convert(varchar(10),F_Date,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),F_Date,112)<=convert(varchar(10),@NextFollowDate,112)group by EnqType  
  end  
  else  
  begin  
  select DISTINCT fd.EnqType,Count(Pk_FollowDetailID) as Count from Address_Master am (nolock)  
inner join Enq_FollowDetails fd (nolock)  on am.Pk_AddressID=fd.Fk_AddressID  

 where am.EnqStatus=1 and  convert(varchar(10),F_Date,112) >= convert(varchar(10),@ByFollowDate,112) and  convert(varchar(10),F_Date,112)  <=convert(varchar(10),@NextFollowDate,112) 
		and ByWhom in (SELECT value FROM dbo.fn_Split(@byUser,','))  
  
group by EnqType  
  end  
end
GO
/****** Object:  StoredProcedure [dbo].[GetFollowUpCountDaily]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetFollowUpCountDaily]
@byUser nvarchar(40),    
@ByFollowDate datetime,    
@NextFollowDate datetime    

AS
	select DISTINCT fd.EnqType,Count(Pk_FollowDetailID) as Count from Address_Master am (nolock)

 inner join Enq_FollowDetails fd (nolock)
 on am.Pk_AddressID=fd.Fk_AddressID
where convert(varchar,F_Date,112) >= convert(varchar,@ByFollowDate,112) and convert(varchar,F_Date,112) <= convert(varchar,@NextFollowDate,112) and ByWhom like @byUser and am.EnqStatus=1
group by EnqType
GO
/****** Object:  StoredProcedure [dbo].[GetFollowUpCount]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetFollowUpCount]
AS
	select EnqType,COUNT(Pk_FollowDetailID) Count from Enq_FollowDetails (nolock)
group by EnqType
GO
/****** Object:  StoredProcedure [dbo].[GetAddressOForderData]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[GetAddressOForderData]      
--exec SP_Search_InquiryAll 'and  Name like ''r%'''

AS 
begin 
    
 SELECT Pk_AddressID,Name,EnqNo from  Address_Master a (nolock)
where a.EnqStatus=1 and a.TypeOfEnq='OC'
  


end
GO
/****** Object:  StoredProcedure [dbo].[Get_Select_PartyMaster]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Select_PartyMaster]

@AddressID int	
AS
	
select * from Party_Master pm (nolock)

	where Fk_AddressId=@AddressID
GO
/****** Object:  StoredProcedure [dbo].[Get_Select_PartyDebitDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Select_PartyDebitDetail]    
    
@AddressID int     
AS    
     
select * from Party_Master pm (nolock)    
inner join Party_Debit pd (nolock)    
on pd.Fk_AddressId=pm.Pk_PartyId    
inner join Party_Debit_Detail pdd (nolock)    
on pdd.Fk_AddressID=pd.Fk_AddressID    
    
    
    
    
 where  pd.Fk_AddressId=@AddressID
GO
/****** Object:  StoredProcedure [dbo].[Get_Select_PartyCreditDetail]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Select_PartyCreditDetail]  
  
@AddressID int   
AS  
   
select * from Party_Master pm (nolock)  
inner join Party_Credit pc (nolock)  
on pc.Fk_AddressID=pm.Fk_AddressID  
inner join Party_CreditDetail pcd (nolock)  
on pcd.Fk_AddressID=pc.Pk_PartyCreditId  
 where pc.Fk_AddressID=@AddressID
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_PaymentTypeWiseReceivePayment]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_PaymentTypeWiseReceivePayment]    
@From datetime,  
@To datetime,  
@PType nvarchar(50)  
AS    
begin  
  
select pcd.PDate 'P. Rec. Date',am.Name,am.City,am.ContactPerson,am.MobileNo,pcd.Amount 'Rec. Amount',am.Pk_AddressID from Address_Master am (nolock)  
inner join Party_Master pm (nolock)  
on pm.Fk_AddressId=am.Pk_AddressID  
left join Party_Credit pc (nolock)  
on pc.Fk_AddressID=pm.Fk_AddressID  
LEFT JOIN Party_CreditDetail pcd (nolock)  
on pcd.Fk_AddressID=pc.Fk_AddressID  
where Convert(varchar(10),pcd.PDate,112)>= Convert(varchar(10),@From,112) and Convert(varchar(10),pcd.PDate,112)<=Convert(varchar(10),@To,112) and pcd.PType like @PType and am.EnqStatus=1  
  
end
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_Party_OutsatndingDetailAfterDispatchByExecutive]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_Party_OutsatndingDetailAfterDispatchByExecutive]    
@From datetime,  
@To datetime  
AS    
begin  
select pm.DispatchDate,am.Name,am.City,pm.Remarks,pm.ExecutiveName,ISNULL(pc.Outstanding,0) 'Outstanding',am.Pk_AddressID from Address_Master am  
inner join Party_Master pm   
on pm.Fk_AddressId=am.Pk_AddressID  
left join Party_Credit pc  
on pc.Fk_AddressID=pm.Fk_AddressID  
where Convert(varchar(20),pm.DispatchDate,112) not like '19000101' and Convert(varchar(20),pm.DispatchDate,112)>= Convert(varchar(20),@From,112) and Convert(varchar(20),pm.DispatchDate,112)<=Convert(varchar(20),@To,112) and am.EnqStatus=1  
  
end
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_Party_OutsatndingDetailAfterDispatch]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_Party_OutsatndingDetailAfterDispatch]    
@From datetime,  
@To datetime  
AS    
begin  
select pm.DispatchDate,am.Name,am.City,am.ContactPerson,am.MobileNo,ISNULL(pc.Outstanding,0) 'Outstanding',am.Pk_AddressID from Address_Master am (nolock)  
inner join Party_Master pm  (nolock)  
on pm.Fk_AddressId=am.Pk_AddressID  
left join Party_Credit pc (nolock)  
on pc.Fk_AddressID=pm.Fk_AddressID  
where Convert(varchar(20),pm.DispatchDate,112) not like '19000101' and Convert(varchar(20),pm.DispatchDate,112)>= Convert(varchar(10),@From,112) and Convert(varchar(10),pm.DispatchDate,112)<=Convert(varchar(10),@To,112) and am.EnqStatus=1 
  
end
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_Party_KasarData]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_Party_KasarData]    
AS    
begin  
 select pm.DispatchDate,am.Name,am.City,am.ContactPerson,am.MobileNo, SUM(pc.Kasar) as Kasar ,am.Pk_AddressID from Address_Master am (nolock)  
inner join Party_Master pm (nolock)  
on pm.Fk_AddressId=am.Pk_AddressID  
inner join Party_Credit pc (nolock)  
on pc.Fk_AddressID=pm.Fk_AddressId  
where am.EnqStatus=1  
group by pm.DispatchDate,am.Name,am.City,am.ContactPerson,am.MobileNo,am.Pk_AddressID   
having SUM(pc.Kasar)>0
end
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_Party_Details]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_Party_Details]    
AS    
begin  
 select tmp.ContactPerson,tmp.City,tmp.MobileNo,tmp.Name,tmp.OrderDate, SUM(tmp.TotalCredit) as 'Net Credit' , SUM(tmp.NetDebit) as 'Net Debit' ,SUM(tmp.NetDebit)-SUM(tmp.TotalCredit) as 'Outstanding',tmp.Advance  from   
(  
 select pm.OrderDate,am.Name,am.City,am.ContactPerson,am.MobileNo,'0' AS NetDebit,ISNULL(pc.TotalCredit,0) as TotalCredit,isnull(pc.Advance,0) as Advance ,am.Pk_AddressID from Address_Master am (nolock)  
inner join Party_Master pm  (nolock)  
on pm.Fk_AddressId=am.Pk_AddressID  
left join Party_Credit pc (nolock)  
on pc.Fk_AddressID=pm.Pk_PartyId  
left join Party_Debit pd (nolock)  
on pd.Fk_AddressID=pm.Pk_PartyId  
where am.EnqStatus=1  
union   
  
 select pm.OrderDate,am.Name,am.City,am.ContactPerson,am.MobileNo,ISNULL(pd.NetDebit,0) as NetDebit ,'0' as TotalCredit,isnull(pc.Advance,0) as Advance,am.Pk_AddressID from Address_Master am (nolock)  
inner join Party_Master pm  (nolock)  
on pm.Fk_AddressId=am.Pk_AddressID  
left join Party_Credit pc (nolock)  
on pc.Fk_AddressID=pm.Pk_PartyId  
left join Party_Debit pd (nolock)  
on pd.Fk_AddressID=pm.Pk_PartyId  
where am.EnqStatus=1  
)as tmp  
group by tmp.ContactPerson,tmp.City,tmp.MobileNo,tmp.Name,tmp.OrderDate,tmp.Advance  
end
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_FolloupSheet]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_FolloupSheet]
@AddressID int	
AS
	
select  top 1 trn.cnt ,trn.Fk_AddressID ,trn.Weekno , trn.F_Date ,Enq.Status  
from Enq_FollowDetails Enq 
inner join (select  Fk_AddressID ,DATEPART(WEEK , F_Date) 
as Weekno , COUNT(DATEPART(WEEK , F_Date)) as cnt,MAX(F_Date) 
as F_Date  from Enq_FollowDetails 
where Fk_AddressID = @AddressID
group by DATEPART(WEEK , F_Date),Fk_AddressID ) as trn
on Enq.Fk_AddressID =trn.Fk_AddressID and Enq.F_Date =trn.F_Date 
order by Enq.Pk_FollowDetailID desc
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_Enquiry_Formate]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_Enquiry_Formate]        
@AddressID int         
AS        
 SELECT  AM.Pk_AddressID,         
  AM.FK_EnqTypeID,        
  AM.EnqNo, AM.Name,        
  AM.Address,        
  AM.Area,        
  AM.City,        
  AM.Pincode,        
  AM.Taluka,        
  AM.District,        
  AM.State,        
  AM.ContactPerson,        
  AM.LandlineNo,        
  AM.MobileNo,        
  AM.EmailID,        
  AM.Remark,        
  AM.EnqDate AMEnqDate,        
  AM.CreateDate,        
  AM.EnqStatus,        
  EM.Reference,        
  EM.EnqDate AS EMEnqDate,        
  EM.EnqTime,        
  EM.EnqFor,        
  EM.PerHr,        
  EM.PerDay,        
  EM.PerReg,        
  EM.RTDS,        
  EM.TTDS,        
  EM.RTH,        
  EM.TTH,        
  EM.RPH,        
  EM.TPH,        
  WM.Application,        
  WM.TypeofEnq,        
  WM.Remarks,        
  WM.EnqAttandBy,        
  WM.SalesExc,        
  WM.EnqAllotted ,  
  wm.CommitVisitIn ,  
  wm.CommitVisitOut ,  
  wm.FinaliseBy ,       
  VM.Strength ,    
  VM.Readiness,    
  VM.Power,
  VD.Remarks      
 FROM dbo.Address_Master AS AM  (nolock)      
  left outer JOIN dbo.Enq_EnqMaster AS EM (nolock)      
 ON AM.Pk_AddressID = EM.Fk_AddressID     
 left outer JOIN  dbo.Enq_WaterMaster AS WM  (nolock)  ON AM.Pk_AddressID = WM.FK_AddressID     
 left outer JOIN  dbo.Enq_VisitorMaster AS VM  (nolock)  ON AM.Pk_AddressID = VM.FK_AddressID     
 left outer JOIN (select top 1 * from Enq_VisitorDetail     
					where FK_AddressID =@AddressID order by Pk_VisitorDetailID desc ) as VD ON AM.Pk_AddressID = Vd.FK_AddressID     
 where AM.Pk_AddressID=@AddressID  and AM.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[Get_Rpt_Enquiry_AssessmentSheet]    Script Date: 12/20/2013 14:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[Get_Rpt_Enquiry_AssessmentSheet]  
@AddressID bigint   
AS  
 Select AM.Pk_AddressID, AM.EnqDate,AM.Name,AM.EnqNo,AM.Address,AM.City,AM.State,AM.District,isnull(AM.MobileNo,AM.LandlineNo) ContactNo,AM.CreateDate,VM.*,WM.* from Address_Master AM
 inner join Enq_VisitorMaster VM
 on VM.Fk_AddressID=AM.Pk_AddressID
 inner join Enq_WaterMaster WM
 on WM.FK_AddressID=AM.Pk_AddressID
  where AM.Pk_AddressID=@AddressID  and AM.EnqStatus=1
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserAllotment]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateUserAllotment]
@Fk_AddressId bigint,
@TeamId int,
@UserId int
AS
begin

Update Tbl_UserAllotmentDetail
set Fk_UserId=@UserId,
Fk_DesignationId=@TeamId

where Fk_AddressId=@Fk_AddressId


end
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddressStatus]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateAddressStatus]
    @EnqType varchar(100),
      
@Pk_AddressID bigint
AS
begin

update Address_Master
set
TypeOfEnq=@EnqType
where Pk_AddressID=@Pk_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddressProject]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateAddressProject]
@Name	varchar(50),
@Address	varchar(MAX),

@City	varchar(50),

@State	varchar(50),

@Pk_AddressID bigint

AS
begin

update Address_Master
set
Name=@Name,
Address=@Address,

City=@City,

State=@State


where Pk_AddressID=@Pk_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddressOtherDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateAddressOtherDetail]
     @Reference1 nvarchar(100),
      @Reference2 nvarchar(100),
      @TypeOfEnq nvarchar(50),
      @EnqFor nvarchar(50),
      @CapacityHour nvarchar(50),
      @Remarks nvarchar(max),
@Pk_AddressID bigint
AS
begin

update Address_Master
set
Reference1=@Reference1,
      Reference2=@Reference2,
      TypeOfEnq=@TypeOfEnq,
      EnqFor=@EnqFor,
      CapacityHour=@CapacityHour,
      Remark=@Remarks
      
where Pk_AddressID=@Pk_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddressContactDetailProject]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateAddressContactDetailProject]
@ContactPerson nvarchar(50),
@MobileNo nvarchar(50),
@EmailID varchar(200),

@Pk_AddressID bigint
AS
begin

update Address_Master
set
ContactPerson=@ContactPerson,
MobileNo=@MobileNo,
EmailID=@EmailID

where Pk_AddressID=@Pk_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddressContactDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateAddressContactDetail]
@ContactPerson nvarchar(50),
@MobileNo nvarchar(50),
@EmailID varchar(200),
@EmailID1 varchar(200),
@EmailID2 varchar(200),
@Pk_AddressID bigint
AS
begin

update Address_Master
set
ContactPerson=@ContactPerson,
MobileNo=@MobileNo,
EmailID=@EmailID,
EmailID1=@EmailID1,
EmailID2=@EmailID2
		
where Pk_AddressID=@Pk_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddress]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_UpdateAddress]
@Name	varchar(50),
@Address	varchar(MAX),
@Area	varchar(50),
@City	varchar(50),
@Pincode	varchar(50),
@Taluka	varchar(50),
@District	varchar(50),
@State	varchar(50),
@DeliveryAddress varchar(MAX),	
@DeliveryArea varchar(50),	
@DeliveryCity varchar(50),	
@DeliveryPincode varchar(50),	
@DeliveryTaluka	varchar(50),	
@DeliveryDistrict varchar(50),
@DeliveryState varchar(50),
@Pk_AddressID bigint

AS
begin

update Address_Master
set
Name=@Name,
Address=@Address,
Area=@Area,
City=@City,
Pincode=@Pincode,
Taluka=@Taluka,
District=@District,
State=@State,
DeliveryAddress=@DeliveryAddress,
DeliveryArea=@DeliveryArea,
DeliveryCity=@DeliveryCity,
DeliveryPincode=@DeliveryPincode,
DeliveryTaluka=@DeliveryTaluka,
DeliveryDistrict=@DeliveryDistrict,
DeliveryState=@DeliveryState

where Pk_AddressID=@Pk_AddressID

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ReInwardTransaction]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ReInwardTransaction]
@reinwardId bigint,
@status bit
AS
begin

update [Tbl_ReInwardMaster]
set Status=@status
where Pk_ReInwardId=@reinwardId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_REInwardMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_REInwardMaster]
@Fk_OutwardId bigint,  
@EngineerName nvarchar(100),  
@ReInwardDate datetime,  
@Remarks nvarchar(max) 

           AS
begin

Update [Tbl_ReInwardMaster]  
set EngineerName=@EngineerName,
ReInwardDate=@ReInwardDate,
Remarks=@Remarks
    where Fk_OutwardId=@Fk_OutwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ReInwardDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ReInwardDetail]
@Fk_ProductRegisterId bigint,
           @Fk_RowMaterialId bigint,
           @Fk_RIInwardId bigint,
           @Quantity bigint,
           @Unit nvarchar(50),
           @Remarks nvarchar(max),
           @Pk_REInwardDetailId bigint
          
           AS
begin

Update [Tbl_RIInwardDetail]
set [Fk_ProductRegisterId]=@Fk_ProductRegisterId
           ,[Fk_RowMaterialId]=@Fk_RowMaterialId
           ,[Fk_ReInwardId]=@Fk_RIInwardId
           ,[Quantity]=@Quantity
           ,[Unit]=@Unit
           ,Remarks=@Remarks
           where Pk_RIInwardDetailId=@Pk_REInwardDetailId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ProjectInformationMaster_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ProjectInformationMaster_Two]
	
	@Fk_AddressId bigint,
	@PlantName nvarchar(500),
	@Model nvarchar(50),
	@ProjectName nvarchar(200),
	@Capacity nvarchar(50),
	@PowerAvailable nvarchar(50),
	@PlantShape nvarchar(50),
	@LandArea nvarchar(50),
	@TreatmentScheme nvarchar(max)

	
As
BEGIN
	UPDATE	Tbl_ProjectInformationMaster_Two
	SET	
	PlantName=@PlantName,
			Model=@Model,
			ProjectName=@ProjectName,
			Capacity=@Capacity,
			PowerAvailable=@PowerAvailable,
			PlantShape=@PlantShape,
			LandArea=@LandArea,
			TreatmentScheme=@TreatmentScheme
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ProjectInformationMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ProjectInformationMaster]
@PlantName nvarchar(50),
           @Model nvarchar(50),
           @ProjectName nvarchar(200),
           @Capacity nvarchar(50),
           @PowerAvailable nvarchar(50),
           @PlantShape nvarchar(50),
           @LandArea nvarchar(50),
           @DType nvarchar(50),
           @TreatmentScheme nvarchar(max),
           @JarDis nvarchar(50),
           @BMouldDis nvarchar(50),
           @TentativeDispatch datetime,
           @Fk_AddressId bigint
           
AS
Begin
Update [Tbl_ProjectInformationMaster]
set [PlantName]=@PlantName
           ,[Model]=@Model
           ,[ProjectName]=@ProjectName
           ,[Capacity]=@Capacity
           ,[PowerAvailable]=@PowerAvailable
           ,[PlantShape]=@PlantShape
           ,[LandArea]=@LandArea
           ,[DType]=@DType
           ,[TreatmentScheme]=@TreatmentScheme
           ,[JarDis]=@JarDis
           ,[BMouldDis]=@BMouldDis
           ,[TentativeDispatch]=@TentativeDispatch
           Where [Fk_AddressId]=@Fk_AddressId
    
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ProjectDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ProjectDetail]
@Fk_AddressId bigint,
           @SrNo int,
           @PlantScheme nvarchar(50),
           @VendorName nvarchar(50),
           @DispatchDate datetime
AS
Begin
Update [Tbl_ProjectDetail]
set 
           [SrNo]=@SrNo
           ,[PlantScheme]=@PlantScheme
           ,[VendorName]=@VendorName
           ,[DispatchDate]=@DispatchDate
 where Fk_AddressId=@Fk_AddressId
        

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ProductInstallationMaster_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ProductInstallationMaster_Two]
	@Fk_AddressId bigint,
	@PDate datetime,
	@Dis_Date datetime,
	@Product_Name nvarchar(500),
	@Vendor_Name nvarchar(500),
	@Station nvarchar(500),
	@Send_CU_To nvarchar(500),
	@Rec_CU_From nvarchar(500),
	@CU_To_Venue nvarchar(500),
	@Comp_Date_With_Inst datetime,
	@By_Whom nvarchar(500),
	@Remark nvarchar(max)
As
BEGIN
	UPDATE	Tbl_ProductInstallationMaster_Two
	SET	PDate=@PDate,
			Dis_Date=@Dis_Date,
			Product_Name=@Product_Name,
			Vendor_Name=@Vendor_Name,
			Station=@Station,
			Send_CU_To=@Send_CU_To,
			Rec_CU_From=@Rec_CU_From,
			CU_To_Venue=@CU_To_Venue,
			Comp_Date_With_Inst=@Comp_Date_With_Inst,
			By_Whom=@By_Whom,
			Remark=@Remark
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_PackagingMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_PackagingMaster]
@Bottling nvarchar(50),
           @BottlingType nvarchar(50),
           @BottllingCapacity nvarchar(50),
           @BottlingVendor1 nvarchar(50),
           @BottlingVendor2 nvarchar(50),
           @Pouch nvarchar(50),
           @PouchType nvarchar(50),
           @PouchCapacity nvarchar(50),
           @PouchVendor1 nvarchar(50),
           @PouchVendor2 nvarchar(50),
           @Chiller nvarchar(50),
           @ChillerVendor1 nvarchar(50),
           @ChillerVendor2 nvarchar(50),
           @Compressor nvarchar(50),
           @Lab nvarchar(50),
           @LabVendor1 nvarchar(50),
           @LabVendor2 nvarchar(50),
           @Letter11 nvarchar(30),
           @Letter12 nvarchar(30),
           @Letter21 nvarchar(30),
           @Letter22 nvarchar(30),
           @Letter31 nvarchar(30),
           @Letter32 nvarchar(30),
           @Letter41 nvarchar(30),
           @Letter42 nvarchar(30),
           @Letter51 nvarchar(30),
           @Letter52 nvarchar(30),
           @Remarks nvarchar(max),
           @ProjectType nvarchar(50),
           @DipatchStatus nvarchar(50),
           @DispatchRemarks nvarchar(max),
           @Fk_AddressId bigint,
           @MouldBottle nvarchar(50),
		   @VendorBottleMould nvarchar(50),
		   @MouldPouch	nvarchar(50),
		   @VendorMouldPouch nvarchar(50),
		   @JarWashing	nvarchar(50),	
@JarWashingType	nvarchar(50),	
@JarWashingCapacity	nvarchar(50),
@JarWashingVendor	nvarchar(50),
@Blow	nvarchar(50),	

@BlowType	nvarchar(50),	
@BlowCapacity	nvarchar(50),
@BlowVendor	nvarchar(50),	
@PackBulk	nvarchar(50),	
@BulkType	nvarchar(50),	
@BulkCapacity	nvarchar(50),
@BulkVendor	nvarchar(50),	
@Soda	nvarchar(50),	
@SodaType	nvarchar(50),
@SodaCapacity	nvarchar(50),
@SodaVendor	nvarchar(50),	
@BatchCoding	nvarchar(50),	
@BatchCodingType	nvarchar(50),
@BatchCodingCapacity	nvarchar(50),
@BatchCodingVendor	nvarchar(50),
@Glass	       nvarchar(50),	
@GlassType	   nvarchar(50),
@GlassCapacity	nvarchar(50),
@GlassVendor	nvarchar(50)	
AS
Begin

Update [Tbl_PackagingDetail]
set [Bottling]=@Bottling
           ,[BottlingType]=@BottlingType
           ,[BottllingCapacity]=@BottllingCapacity
           ,[BottlingVendor1]=@BottlingVendor1
           ,[BottlingVendor2]=@BottlingVendor2
           ,[Pouch]=@Pouch
           ,[PouchType]=@PouchType
           ,[PouchCapacity]=@PouchCapacity
           ,[PouchVendor1]=@PouchVendor1
           ,[PouchVendor2]=@PouchVendor2
           ,[Chiller]=@Chiller
           ,[ChillerVendor1]=@ChillerVendor1
           ,[ChillerVendor2]=@ChillerVendor2
           ,[Compressor]=@Compressor
           ,[Lab]=@Lab
           ,[LabVendor1]=@LabVendor1
           ,[LabVendor2]=@LabVendor2
           ,[Letter11]=@Letter11
           ,[Letter12]=@Letter12
           ,[Letter21]=@Letter21
           ,[Letter22]=@Letter22
           ,[Letter31]= @Letter31
           ,[Letter32]=@Letter32
           ,[Letter41]=@Letter41
           ,[Letter42]=@Letter42
           ,[Letter51]=@Letter51
           ,[Letter52]=@Letter52
           ,[Remarks]=@Remarks
           ,[ProjectType]= @ProjectType
           ,[DipatchStatus]=@DipatchStatus
           ,[DispatchRemarks]=@DispatchRemarks
           ,MouldBottle=@MouldBottle
		   ,VendorBottleMould=@VendorBottleMould
		   ,MouldPouch=@MouldPouch
		   ,VendorMouldPouch=@VendorMouldPouch,
		 
 JarWashing=@JarWashing,	
 JarWashingType=@JarWashingType,	
 JarWashingCapacity=@JarWashingCapacity,
 JarWashingVendor=@JarWashingVendor,
 Blow=@Blow,	
 
 BlowType=@BlowType,	
 BlowCapacity=@BlowCapacity,
 BlowVendor=@BlowVendor,	
 PackBulk=@PackBulk,	
 BulkType=@BulkType,	
 BulkCapacity=@BulkCapacity,
 BulkVendor=@BulkVendor,	
 Soda=@Soda,	
 SodaType=@SodaType,
 SodaCapacity=@SodaCapacity,
 SodaVendor=@SodaVendor,	
 BatchCoding=@BatchCoding,	
 BatchCodingType=@BatchCodingType,
 BatchCodingCapacity=@BatchCodingCapacity,
 BatchCodingVendor=@BatchCodingVendor,
Glass=@Glass,	    
GlassType=@GlassType,
GlassCapacity=@GlassCapacity,
GlassVendor=@GlassVendor
		   
		   
		   
           where [Fk_AddressId]=@Fk_AddressId
  
           



End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OutwardTransaction]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OutwardTransaction]
@outwardId bigint,
@status bit
AS
begin

update [Tbl_OutwardMaster]
set Status=@status
where Pk_OutwardId=@outwardId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OutwardMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OutwardMaster]
@Fk_AddressId bigint,
@CustomerName nvarchar(200),
@Address nvarchar(max),
@ContactNo nvarchar(50),
@Remarks nvarchar(max),
@EntryDate datetime,
@EngineerName nvarchar(50),
@Pk_OutwardId bigint
           AS
begin
update [Tbl_OutwardMaster]
set [Fk_AddressId]=@Fk_AddressId
    ,[CustomerName]=@CustomerName
    ,[Address]=@Address
    ,[ContactNo]=@ContactNo
    ,[Remarks]=@Remarks
    ,OutwardDate=@EntryDate
    ,[EngineerName]=@EngineerName
     where Pk_OutwardId=@Pk_OutwardId
     

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OutwardDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OutwardDetail]
@Fk_OutwardId bigint,
@Fk_ProductRegisterId bigint,
@Fk_RowMaterialId bigint,
@Quantity bigint,
@Unit nvarchar(50),
@Remarks nvarchar(max),
@Pk_OutwardDetailId bigint        
AS
begin
update [Tbl_OutwardDetail]
set [Fk_OutwardId]=@Fk_OutwardId
           ,[Fk_ProductRegisterId]=@Fk_ProductRegisterId
           ,[Fk_RowMaterialId]=@Fk_RowMaterialId
           ,[Quantity]=@Quantity
           ,[Unit]=@Unit
   ,Remarks=@Remarks
   where Pk_OutwardDetailId=@Pk_OutwardDetailId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderVisitorDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderVisitorDetail]
@Visitor1Image1 nvarchar(max),
           @Visitor1Value1 nvarchar(100),
           @Visitor1Value2 nvarchar(100),
           @Visitor1Value3 nvarchar(100),
           @Visitor1Value4 nvarchar(100),
           @Visitor2Image1 nvarchar(max),
           @Visitor2Value1 nvarchar(100),
           @Visitor2Value2 nvarchar(100),
           @Visitor2Value3 nvarchar(100),
           @Visitor2Value4 nvarchar(100),
           @Visitor3Image1 nvarchar(max),
           @Visitor3Value1 nvarchar(100),
           @Visitor3Value2 nvarchar(100),
           @Visitor3Value3 nvarchar(100),
           @Visitor3Value4 nvarchar(100),
           @Fk_AddressId bigint
AS
Begin
Update [Tbl_OrderVisitorDetail]
set [Visitor1Image1]=@Visitor1Image1
           ,[Visitor1Value1]=@Visitor1Value1
           ,[Visitor1Value2]=@Visitor1Value2
           ,[Visitor1Value3]=@Visitor1Value3
           ,[Visitor1Value4]=@Visitor1Value4
           ,[Visitor2Image1]=@Visitor2Image1
           ,[Visitor2Value1]=@Visitor2Value1
           ,[Visitor2Value2]=@Visitor2Value2
           ,[Visitor2Value3]=@Visitor2Value3
           ,[Visitor2Value4]=@Visitor2Value4
           ,[Visitor3Image1]=@Visitor3Image1
           ,[Visitor3Value1]=@Visitor3Value1
           ,[Visitor3Value2]=@Visitor3Value2
           ,[Visitor3Value3]=@Visitor3Value3
           ,[Visitor3Value4]=@Visitor3Value4
           where Fk_AddressId=@Fk_AddressId
    


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderServiceFollowUpDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderServiceFollowUpDetail]
@SFDate datetime,
           @ServiceType nvarchar(50),
           @ComplainNo nvarchar(30),
           @AttendDate datetime,
           @AttendBy nvarchar(50),
           @Engineer nvarchar(50),
           @FollowUp nvarchar(100),
           @TentativeADate datetime,
           @Status nvarchar(50),
           @Remarks nvarchar(300),
           @Fk_AddressId bigint
As
begin
Update [Tbl_OrderServiceFollowUpDetail]
set [SFDate]=@SFDate
           ,[ServiceType]=@ServiceType
           ,[ComplainNo]=@ComplainNo
           ,[AttendDate]=@AttendDate
           ,[AttendBy]=@AttendBy
           ,[Engineer]=@Engineer
           ,[FollowUp]=@FollowUp
           ,[TentativeADate]=@TentativeADate
           ,[Status]=@Status
           ,[Remarks]=@Remarks
       where [Fk_AddressId]=@Fk_AddressId
  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderPartyMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderPartyMaster]
@Fk_AddressId bigint,
           
           @ContactPersonOne nvarchar(100),
           @ContactPersonTwo nvarchar(100),
           @VatNoTinNo nvarchar(80),
           @CSTNo nvarchar(50),
           @RoadPermit nvarchar(100),
           @CForm nvarchar(50),
           @TransportExpences decimal(18,2),
           @Remarks nvarchar(300)
          

AS
begin

update [Tbl_OrderPartyMaster]
set [Fk_AddressId]=@Fk_AddressId
          
           ,[ContactPersonOne]=@ContactPersonOne
           ,[ContactPersonTwo]=@ContactPersonTwo
           ,[VatNoTinNo]=@VatNoTinNo
           ,[CSTNo]=@CSTNo
           ,[RoadPermit]=@RoadPermit
           ,[CForm]=@CForm
           ,[TransportExpences]=@TransportExpences
           ,[Remarks]=@Remarks
  
     
  where Fk_AddressId=@Fk_AddressId
           
           end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderOutstandingDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderOutstandingDetail]
@OrderStatus nvarchar(50),
           @OrderAmount numeric(18,2),
           @AdvanceRecieved numeric(18,2),
           @PaymentRecieved numeric(18,2),
           @OutstandingAmount numeric(18,2),
           @Remarks nvarchar(max),
           @Fk_AddressId bigint
        
AS
begin

Update [Tbl_OrderOutstandingDetail]
set [OrderStatus]=@OrderStatus
           ,[OrderAmount]=@OrderAmount
           ,[AdvanceRecieved]=@AdvanceRecieved
           ,[PaymentRecieved]=@PaymentRecieved
           ,[OutstandingAmount]=@OutstandingAmount
           ,[Remarks]=@Remarks
           
           where [Fk_AddressId]=@Fk_AddressId
    
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderOneMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderOneMaster]
@EntryNo nvarchar(50),
           @EntryDate datetime,
           @PONo nvarchar(50),
           @OrderNo nvarchar(50),
           @OrderDate datetime,
           @DispatchDate datetime,
           @PartyName nvarchar(100),
           @BrandName nvarchar(200),
          
           @Fk_AddressId bigint,
             @OrderRecBy	nvarchar(50),
		   @OrderFollowBy	nvarchar(50),
		   @OrderStatus nvarchar(50),
		   @OrderRecFromMkt datetime	
AS
Begin
Update [Tbl_OrderOneMaster]
set [EntryNo]=@EntryNo
           ,[EntryDate]=@EntryDate
           ,[PONo]=@PONo
           ,[OrderNo]=@OrderNo
           ,[OrderDate]=@OrderDate
           ,[DispatchDate]=@DispatchDate
           ,[PartyName]=@PartyName
           ,[BrandName]=@BrandName
           ,OrderRecBy=@OrderRecBy
		  ,OrderFollowBy=@OrderFollowBy
		  ,OrderStatus=@OrderStatus
		  ,OrderRecFromMkt=@OrderRecFromMkt
        where [Fk_AddressId]=@Fk_AddressId
 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderMaster_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderMaster_Two]
@EntryNo nvarchar(50),
           @EntryDate datetime,
           @PONo nvarchar(50),
           @OrderNo nvarchar(50),
           @OrderDate datetime,
           @DispatchDate datetime,
           @PartyName nvarchar(100),
           @BrandName nvarchar(200),
          
           @Fk_AddressId bigint
AS
Begin
Update [Tbl_OrderMaster_Two]
set [EntryNo]=@EntryNo
           ,[EntryDate]=@EntryDate
           ,[PONo]=@PONo
           ,[OrderNo]=@OrderNo
           ,[OrderDate]=@OrderDate
           ,[DispatchDate]=@DispatchDate
           ,[PartyName]=@PartyName
           ,[BrandName]=@BrandName
        where [Fk_AddressId]=@Fk_AddressId
 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderFollowupMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderFollowupMaster]
@ProjectDetail nvarchar(200),
@Fk_AddressId bigint
           AS
begin

update [Tbl_OrderFollowupMaster]
set [ProjectDetail]=@ProjectDetail
where Fk_AddressId=@Fk_AddressId
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_OrderFollowupDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_OrderFollowupDetail]
@Fk_AddressId bigint,
           @FDate datetime,
        
           @FollowUp nvarchar(50),
           @NFDate datetime,
           @Status nvarchar(50),
           @ByWhom nvarchar(50),
           @ProjectType nvarchar(50),
           @Remarks nvarchar(200)
AS
begin
Update [Tbl_OrderFollowupDetail]
set 
         [FDate]=@FDate
         
           ,[FollowUp]=@FollowUp
           ,[NFDate]=@NFDate
           ,[Status]=@Status
           ,[ByWhom]=@ByWhom
           ,[ProjectType]=@ProjectType
           ,[Remarks]=@Remarks
where Fk_AddressId=@Fk_AddressId
        
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_LetterMailComMaster_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_LetterMailComMaster_Two]
    
	@Fk_AddressId bigint,
	@ProjectDetail nvarchar(200)

	
As
BEGIN
	UPDATE	Tbl_LetterMailComMaster_Two
	SET	
			ProjectDetail=@ProjectDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_LetterMailComMaster_Detail_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_LetterMailComMaster_Detail_Two]
	
	@Fk_AddressId bigint,
	@LDate datetime,
	@Card_Date datetime,
	@Card_Rem nvarchar(500),
	@Mail_Rec nvarchar(200),
	@Mail_Send nvarchar(200),
	@BY_Whom nvarchar(200),
	@Mail_Rem nvarchar(500)

	
As
BEGIN
	UPDATE	Tbl_LetterMailComMaster_Detail_Two
	SET	
			LDate=@LDate,
			Card_Date=@Card_Date,
			Card_Rem=@Card_Rem,
			Mail_Rec=@Mail_Rec,
			Mail_Send=@Mail_Send,
			BY_Whom=@BY_Whom,
			Mail_Rem=@Mail_Rem
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ISIProcessMaster_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ISIProcessMaster_Two]
	
	@Fk_AddressId bigint,
	@Scheme_Name nvarchar(500),
	@D_Rec_Date datetime,
	@P_Doc_F_Date datetime,
	@P_Doc_R_Date datetime,
	@F_Ok_S_Tc_P nvarchar(500),
	@F_Submit_P nvarchar(500),
	@File_Reg_Date datetime,
	@BIS_Insp_Date datetime,
	@Licence_Date datetime,
	@Vender nvarchar(max),
	@ISIRemark nvarchar(max)
	
As
BEGIN
	UPDATE	Tbl_ISIProcessMaster_Two
	SET	
	        Scheme_Name=@Scheme_Name,
			D_Rec_Date=@D_Rec_Date,
			P_Doc_F_Date=@P_Doc_F_Date,
			P_Doc_R_Date=@P_Doc_R_Date,
			F_Ok_S_Tc_P=@F_Ok_S_Tc_P,
			F_Submit_P=@F_Submit_P,
			File_Reg_Date=@File_Reg_Date,
			BIS_Insp_Date=@BIS_Insp_Date,
			Licence_Date=@Licence_Date,
			Vender=@Vender,
			ISIRemark=@ISIRemark
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_ISIProcess_DetailMaster_Two]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_ISIProcess_DetailMaster_Two]
	@Fk_AddressId bigint,
           @FDate datetime,
        
           @FollowUp nvarchar(50),
           @NFDate datetime,
           @Status nvarchar(50),
           @ByWhom nvarchar(50),
           @ProjectType nvarchar(50),
           @Remarks nvarchar(200)
AS
begin
Update Tbl_ISIProcess_DetailMaster_Two
set 
         [FDate]=@FDate
         
           ,[FollowUp]=@FollowUp
           ,[NFDate]=@NFDate
           ,[Status]=@Status
           ,[ByWhom]=@ByWhom
           ,[ProjectType]=@ProjectType
           ,[Remarks]=@Remarks
where Fk_AddressId=@Fk_AddressId
  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_InwardTransaction]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_InwardTransaction]
@inwardId bigint,
@status bit
AS
begin

update [Tbl_InwardStockMaster]
set Status=@status
where Pk_InwardId=@inwardId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_InwardStockMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_InwardStockMaster]
@PONo nvarchar(50),
@InwardDate datetime,
@BillNo nvarchar(50),
@BillDate datetime,
@Pk_InwardId bigint,
@SupplierName nvarchar(100),
@SupplierAddress nvarchar(max)
           AS
begin

Update [Tbl_InwardStockMaster]
set [PONo]=@PONo
    ,[InwardDate]=@InwardDate
    ,[BillNo]=@BillNo
    ,[BillDate]=@BillDate
    ,SupplierName=@SupplierName
	 ,SupplierAddress=@SupplierAddress
    where Pk_InwardId=@Pk_InwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_InwardDetail]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_InwardDetail]
@Fk_ProductRegisterId bigint,
           @Fk_RowMaterialId bigint,
           @Fk_InwardId bigint,
           @Quantity bigint,
           @Unit nvarchar(50),
           @Pk_InwardDetailId bigint,
           @Remarks nvarchar(max)  
           AS
begin

Update [Tbl_InwardDetail]
set [Fk_ProductRegisterId]=@Fk_ProductRegisterId
           ,[Fk_RowMaterialId]=@Fk_RowMaterialId
           ,[Fk_InwardId]=@Fk_InwardId
           ,[Quantity]=@Quantity
           ,[Unit]=@Unit
           ,Remarks=@Remarks
           where Pk_InwardDetailId=@Pk_InwardDetailId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Tbl_InvoiceMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Tbl_InvoiceMaster]
@Fk_AddressId bigint,
         
           @InvoiceNo nvarchar(50),
           @Transporter nvarchar(100),
           @RRLRNo nvarchar(50),
           @BasicAmount decimal(18,2),
           @Discount numeric(18,2),
           @Total numeric(18,2),
           @CSTPerc int,
           @SubTotal numeric(18,2),
           @Advance numeric(18,2),
           @NetAmount numeric(18,2),
           @BankDetail nvarchar(100),
            @InvoiceDate datetime,
            @CSTNo nvarchar(50),
           @GSTNo nvarchar(50)
           AS
begin

Update [Tbl_InvoiceMaster]
set 
         
          
           [InvoiceNo]=@InvoiceNo
           ,[Transporter]=@Transporter
           ,[RRLRNo]=@RRLRNo
           ,[BasicAmount]=@BasicAmount
           ,[Discount]=@Discount
           ,[Total]=@Total
           ,[CSTPerc]=@CSTPerc
           ,[SubTotal]=@SubTotal
           ,[Advance]=@Advance
           ,[NetAmount]=@NetAmount
           ,[BankDetail]=@BankDetail
		   ,InvoiceDate=@InvoiceDate
		   ,CSTNo=@CSTNo
 ,GSTNo=@GSTNo
         where Fk_AddressId=@Fk_AddressId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Service_Address_Info]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Service_Address_Info]  
   @Pk_SerAddressId bigint  
   ,@fk_AddressId bigint  
           ,@Name varchar(50)  
           ,@Post varchar(50)  
           ,@COntactNo varchar(12)  
           ,@EmailId varchar(50)  
             
       as   
       begin  
             
UPDATE [Service_Address_Info]  
SET             
           [fk_AddressId]=@fk_AddressId   
           ,[Name]=@Name  
           ,[Post]=@Post  
           ,[ContactNumber]=@COntactNo  
           ,[EmailId]=@EmailId  
             
           Where Pk_SerAddressId=@Pk_SerAddressId 
             
           end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_RowMaterialMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_RowMaterialMaster]

@RowMaterialName nvarchar(200),
           @Unit nvarchar(30),
           @ReOrder bigint,
           @Fk_ProductRegisterId bigint,
           @Status bit,
           @Pk_RowMaterialId bigint
           
           AS
begin

update [Tbl_RowMaterialMaster]
set [RowMaterialName]=@RowMaterialName
           ,[Unit]=@Unit
           ,[ReOrder]=@ReOrder
           ,[Fk_ProductRegisterId]=@Fk_ProductRegisterId
           ,[Status]=@Status
    
           
           where Pk_RowMaterialId=@Pk_RowMaterialId

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_ProductRegisterMaster]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_ProductRegisterMaster]
@CategoryName nvarchar(200),
@Status bit,
@Pk_ProductRegisterId bigint
AS


begin

update [Tbl_ProductRegisterMaster]
set [CategoryName]=@CategoryName
,[Status]=@Status
where Pk_ProductRegisterId=@Pk_ProductRegisterId


	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Party_Master]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Party_Master]  
@EntryNo nvarchar(50),  
@OrderDate datetime,  
@PONo nvarchar(50),  
@OType nvarchar(100),  
@Fk_AddressId bigint,  
@PlantName nvarchar(150),  
@Capacity nvarchar(50),  
@DispatchDate datetime,  
@ExecutiveName nvarchar(80),  
@OrderStatus nvarchar(50),  
@Remarks nvarchar(max),  
@BreakSrNo numeric(18,0),  
@PDCReminder nvarchar(max),  
@Pk_PartyId bigint  
  
AS  
Begin  
  
update [dbo].[Party_Master]  
set [EntryNo]=@EntryNo  
           ,[OrderDate]=@OrderDate  
           ,[PONo]=@PONo  
           ,[OType]=@OType  
           ,[Fk_AddressId]=@Fk_AddressId  
           ,[PlantName]=@PlantName  
           ,[Capacity]=@Capacity  
           ,[DispatchDate]=@DispatchDate  
           ,[ExecutiveName]=@ExecutiveName  
           ,[OrderStatus]=@OrderStatus  
           ,[Remarks]=@Remarks  
           ,[BreakSrNo]=@BreakSrNo  
           ,[PDCReminder]=@PDCReminder  
         
where Fk_AddressId=@Fk_AddressId  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Party_Debit]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Party_Debit]  
@Fk_AddressId bigint,  
@TotalDebit numeric(18,2),  
@Discount numeric(18,2),  
@NetDebit numeric(20,2),  
@Pk_Party_DebitId bigint  
  
AS  
Begin  
update [dbo].[Party_Debit]  
Set [Fk_AddressId]=@Fk_AddressId 
           ,[TotalDebit]=@TotalDebit  
           ,[Discount]=@Discount  
           ,[NetDebit]=@NetDebit  
    where Pk_PartyDebitId=@Pk_Party_DebitId  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Party_Credit]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Update_Party_Credit]  
@Fk_AddressID bigint,  
           @TotalCredit numeric(18,2),  
           @Kasar numeric(18,2),  
           @Outstanding numeric(20,2),  
           @Advance numeric(18,2),  
           @Pk_PartyCreditId bigint  
AS  
Begin  
update [dbo].[Party_Credit]  
  set  [Fk_AddressID]=@Fk_AddressID  
           ,[TotalCredit]=@TotalCredit  
           ,[Kasar]=@Kasar  
           ,[Outstanding]=@Outstanding  
           ,[Advance]=@Advance  
    where Pk_PartyCreditId=@Pk_PartyCreditId  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_test]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[sp_test]
@user varchar(200)
as

begin

select * from User_Master
where UserName in (SELECT value FROM dbo.fn_Split(@user, ','))
end

--sp_test 'RK,PP'
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_UserAllotmentDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_UserAllotmentDetail_Update]
	@Fk_AddressId bigint,
	@Fk_UserId int,
	@Fk_DesignationId int,

	@Pk_AllotId bigint
As
BEGIN
	UPDATE	Tbl_UserAllotmentDetail
	SET	Fk_AddressId=@Fk_AddressId,
			Fk_UserId=@Fk_UserId,
			Fk_DesignationId=@Fk_DesignationId
	WHERE	Pk_AllotId=@Pk_AllotId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_UserAllotmentDetail_SelectByTeamAndUser]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_UserAllotmentDetail_SelectByTeamAndUser]
@TeamId int,
@User int	

As
BEGIN
if(@User=0)
begin
SELECT ua.*,a.EnqNo,u.UserName,t.TeamName,t.Department FROM Tbl_UserAllotmentDetail ua  (nolock)
inner join Address_Master a (nolock)
on ua.Fk_AddressId=a.Pk_AddressID
inner join User_Master u (nolock)
on u.Pk_UserId=ua.Fk_UserId
inner join Tbl_TeamMaster t (nolock)
on t.Pk_TeamId=ua.Fk_DesignationId
	WHERE	ua.Fk_DesignationId=@TeamId
end
else
begin 
SELECT ua.*,a.EnqNo,u.UserName,t.TeamName,t.Department FROM Tbl_UserAllotmentDetail ua (nolock)
inner join Address_Master a (nolock)
on ua.Fk_AddressId=a.Pk_AddressID
inner join User_Master u (nolock)
on u.Pk_UserId=ua.Fk_UserId
inner join Tbl_TeamMaster t (nolock)
on t.Pk_TeamId=ua.Fk_DesignationId
WHERE	ua.Fk_UserId=@User and ua.Fk_DesignationId=@TeamId

end

	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_UserAllotmentDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_UserAllotmentDetail_Select]
	@Pk_AllotId bigint
As
BEGIN
	SELECT * FROM Tbl_UserAllotmentDetail (nolock)
	WHERE	Pk_AllotId=@Pk_AllotId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_UserAllotmentDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_UserAllotmentDetail_Insert]
	@Fk_AddressId bigint,
	@Fk_UserId int,
	@Fk_DesignationId int
As
BEGIN
declare @team as int;
declare @dept as varchar(50);
set @team=(select Fk_TeamId from User_Master where Pk_UserId=@Fk_UserId)
set @dept=(select Department from Tbl_TeamMaster where Pk_TeamId=@team)


if(@dept='Order')
begin

if not exists (select Fk_AddressId from Tbl_UserAllotmentDetail where Fk_AddressId=@Fk_AddressId and Fk_DesignationId=@Fk_DesignationId and Fk_UserId=@Fk_UserId)
	begin
	 INSERT INTO Tbl_UserAllotmentDetail
		(
		Fk_AddressId,
		Fk_UserId,
		Fk_DesignationId
		)
	 VALUES
		(
		@Fk_AddressId,
		@Fk_UserId,
		@Fk_DesignationId
		)
		return Scope_identity();
	end
	else
	begin
	
	
	return -1;
	end

end

else
begin
	if not exists (select Fk_AddressId from Tbl_UserAllotmentDetail where Fk_AddressId=@Fk_AddressId)
	begin
	 INSERT INTO Tbl_UserAllotmentDetail
		(
		Fk_AddressId,
		Fk_UserId,
		Fk_DesignationId
		)
	 VALUES
		(
		@Fk_AddressId,
		@Fk_UserId,
		@Fk_DesignationId
		)
		return Scope_identity();
	end
	else
	begin 



		delete from Tbl_UserAllotmentDetail
		where Fk_AddressId=@Fk_AddressId 
		
if not exists (select Fk_AddressId from Tbl_UserAllotmentDetail where Fk_AddressId=@Fk_AddressId)
	begin
	 INSERT INTO Tbl_UserAllotmentDetail
		(
		Fk_AddressId,
		Fk_UserId,
		Fk_DesignationId
		)
	 VALUES
		(
		@Fk_AddressId,
		@Fk_UserId,
		@Fk_DesignationId
		)
		return Scope_identity();
	end
			

	end
end	
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_UserAllotmentDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_UserAllotmentDetail_Delete]
	@Pk_AllotId bigint
As
BEGIN
	DELETE FROM Tbl_UserAllotmentDetail
	WHERE	Pk_AllotId=@Pk_AllotId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_TeamMaster_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_TeamMaster_Update]
	@TeamName nvarchar(50),

	@Department nvarchar(50),

	@Pk_TeamId int
As
BEGIN

	UPDATE	Tbl_TeamMaster
	SET	TeamName=@TeamName,
 			Department=@Department
	WHERE	Pk_TeamId=@Pk_TeamId
	

	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_TeamMaster_SelectAll]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_TeamMaster_SelectAll]

As
BEGIN
	SELECT Pk_TeamId,TeamName+'-'+Department As TeamName,Department FROM Tbl_TeamMaster (nolock)
	where Status=1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_TeamMaster_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_TeamMaster_Select]
	@Pk_TeamId int
As
BEGIN
	SELECT Pk_TeamId,TeamName,Department FROM Tbl_TeamMaster (nolock)
	WHERE	Pk_TeamId=@Pk_TeamId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_TeamMaster_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_TeamMaster_Insert]
	@TeamName nvarchar(50),

	@Department nvarchar(50)
As
BEGIN

if not exists (select TeamName from Tbl_TeamMaster where TeamName=@TeamName)
begin

	 INSERT INTO Tbl_TeamMaster
		(
		TeamName,
	
		Department
		)
	 VALUES
		(
		@TeamName,
		
		@Department
		)
		return scope_identity();
end		
else
begin
return -1;
end


END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_TeamMaster_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_TeamMaster_Delete]
	@Pk_TeamId int
As
BEGIN
	Update  Tbl_TeamMaster
	set Status=0
	WHERE	Pk_TeamId=@Pk_TeamId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StockReportLogDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StockReportLogDetail_Update]
	@Fk_UserId int,
	@LogDate datetime,
	@Remarks nvarchar(300),
	@Status bit,

	@Pk_StockLogId bigint
As
BEGIN
	UPDATE	Tbl_StockReportLogDetail
	SET	Fk_UserId=@Fk_UserId,
			LogDate=@LogDate,
			Remarks=@Remarks,
			Status=@Status
	WHERE	Pk_StockLogId=@Pk_StockLogId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StockReportLogDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StockReportLogDetail_Select]
	@Fk_UserId int
As
BEGIN
	SELECT * FROM Tbl_StockReportLogDetail
	WHERE Fk_UserId=@Fk_UserId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StockReportLogDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StockReportLogDetail_Insert]
	@Fk_UserId int,
	@LogDate datetime,
	@Remarks nvarchar(300),
	@Status bit
As
BEGIN
	 INSERT INTO Tbl_StockReportLogDetail
		(
		Fk_UserId,
		LogDate,
		Remarks,
		Status
		)
	 VALUES
		(
		@Fk_UserId,
		@LogDate,
		@Remarks,
		@Status
		)
		return Scope_identity();
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StockReportLogDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StockReportLogDetail_Delete]
	@Pk_StockLogId bigint
As
BEGIN
	DELETE FROM Tbl_StockReportLogDetail
	WHERE	Pk_StockLogId=@Pk_StockLogId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Stock_ProductRegister_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Stock_ProductRegister_Update]
	@Fk_CategoryId bigint,
	@Fk_RawMaterialId bigint,
	

	@RemainingStock int,
	@Unit nvarchar(50),
	@OpeningStock int,
	@Pk_ProductRegisterID bigint,
	@EntryDate datetime
As
BEGIN
	UPDATE	Tbl_Stock_ProductRegister
	SET	Fk_CategoryId=@Fk_CategoryId,
			Fk_RawMaterialId=@Fk_RawMaterialId,
			RemainingStock=@RemainingStock,
			Unit=@Unit,
			OpeningStock=@OpeningStock,
			EntryDate=@EntryDate
	WHERE	Pk_ProductRegisterID=@Pk_ProductRegisterID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Stock_ProductRegister_SelectByCategory]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Stock_ProductRegister_SelectByCategory]  
@CategoryId bigint
AS  
begin  


/****** Script for SelectTopNRows command from SSMS  ******/
SELECT sp.Pk_ProductRegisterId, [Fk_CategoryId],
      [Fk_RawMaterialId],ROW_NUMBER() OVER(ORDER BY sp.Pk_ProductRegisterId)as 'No'
      ,cm.CategoryName,rm.RowMaterialName,OpeningStock,
      isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0) + isnull(InwardStock,0) AS InwardStock,
	isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) + isnull(OutwardStock,0) AS OutwardStock,
	(isnull((select sum(Quantity)from Tbl_InwardStockMaster im (nolock) inner join Tbl_InwardDetail ID (nolock) on im.Pk_InwardId=ID.Fk_InwardId  where ID.Fk_RowMaterialId=sp.Fk_RawMaterialId and ID.Fk_ProductRegisterId=sp.Fk_CategoryId and im.[Status]=1),0)+ isnull(InwardStock,0) + OpeningStock) - 
	(isnull((select sum(Quantity)from Tbl_OutwardMaster om (nolock) inner join Tbl_OutwardDetail od (nolock) on om.Pk_OutwardId=od.Fk_OutwardId where OD.Fk_RowMaterialId=sp.Fk_RawMaterialId and OD.Fk_ProductRegisterId=sp.Fk_CategoryId and om.[Status]=1),0) + isnull(OutwardStock,0))as RemainingStock
       ,isnull((select sum(Quantity)from Tbl_ReInwardMaster rm (nolock) inner join Tbl_RIInwardDetail RD (nolock) on rm.Pk_ReInwardId=RD.Fk_ReInwardId  where rD.Fk_RowMaterialId=sp.Fk_RawMaterialId and RD.Fk_ProductRegisterId=sp.Fk_CategoryId and rm.[Status]=1),0) + isnull(ReorderStock,0) AS ReInwardStock 
           ,sp.[Unit]
      ,[EntryDate]
      ,rm.ReOrder
  FROM [Tbl_Stock_ProductRegister] sp (nolock)
  inner join Tbl_ProductRegisterMaster cm (nolock)
  on sp.Fk_CategoryId=cm.Pk_ProductRegisterId
   inner join Tbl_RowMaterialMaster rm (nolock)
        on rm.Pk_RowMaterialId=sp.Fk_RawMaterialId  
        where Fk_CategoryId=@CategoryId
 
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Stock_ProductRegister_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Stock_ProductRegister_Select]
	@Pk_ProductRegisterID bigint
As
BEGIN
	SELECT * FROM Tbl_Stock_ProductRegister (nolock)
	WHERE	Pk_ProductRegisterID=@Pk_ProductRegisterID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Stock_ProductRegister_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Stock_ProductRegister_Insert]
	@Fk_CategoryId bigint,
	@Fk_RawMaterialId bigint,
	@InwardStock int,
	@OutwardStock int,
	@ReorderStock int,
	@RemainingStock int,
	@Unit nvarchar(50),
	@OpeningStock int,
	@EntryDate datetime
	
As
BEGIN
if not exists (select Pk_ProductRegisterID from Tbl_Stock_ProductRegister where Fk_CategoryId=@Fk_CategoryId and Fk_RawMaterialId=@Fk_RawMaterialId )
begin

	 INSERT INTO Tbl_Stock_ProductRegister
		(
		Fk_CategoryId,
		Fk_RawMaterialId,
	
		InwardStock,
		OutwardStock,
		ReorderStock,
		RemainingStock,
		Unit,
		OpeningStock,
		EntryDate
		)
	 VALUES
		(
		@Fk_CategoryId,
		@Fk_RawMaterialId,
	
		@InwardStock,
		@OutwardStock,
		@ReorderStock,
		@RemainingStock,
		@Unit,
		@OpeningStock,
		@EntryDate
		)
		return scope_identity();
		end
		else
		begin
		return -1;
		end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Stock_ProductRegister_DeleteByCategory]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Stock_ProductRegister_DeleteByCategory]
	@Fk_CategoryId bigint
As
BEGIN
	DELETE FROM Tbl_Stock_ProductRegister
	WHERE	Fk_CategoryId=@Fk_CategoryId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Stock_ProductRegister_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Stock_ProductRegister_Delete]
	@Pk_ProductRegisterID bigint
As
BEGIN
	DELETE FROM Tbl_Stock_ProductRegister
	WHERE	Pk_ProductRegisterID=@Pk_ProductRegisterID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StateTeamAllotment_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StateTeamAllotment_Update]
	@StateName nvarchar(50),
	@Fk_TeamId int,
	@Fk_UserId int,
	@Pk_AllotStateId int
As
BEGIN

	UPDATE	Tbl_StateTeamAllotment
	SET	StateName=@StateName,
			Fk_TeamId=@Fk_TeamId,
			Fk_UserId=@Fk_UserId
	WHERE	Pk_AllotStateId=@Pk_AllotStateId
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StateTeamAllotment_SelectAll]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StateTeamAllotment_SelectAll]
	
As
BEGIN
	SELECT Pk_AllotStateId,StateName,isnull(tm.TeamName,'') 'TeamName',ISNULL(um.UserName,'') 'User' FROM Tbl_StateTeamAllotment st (nolock)
	left join Tbl_TeamMaster tm (nolock) on tm.Pk_TeamId=st.Fk_TeamId 
	left join User_Master um (nolock) on um.Pk_UserId=st.Fk_UserId
	

END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StateTeamAllotment_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StateTeamAllotment_Select]
	@Pk_AllotStateId int
As
BEGIN
	SELECT * FROM Tbl_StateTeamAllotment (nolock)
	WHERE	Pk_AllotStateId=@Pk_AllotStateId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StateTeamAllotment_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StateTeamAllotment_Insert]
	@StateName nvarchar(50),
	@Fk_TeamId int,
	@Fk_UserId int
As
BEGIN
if not exists(Select StateName from Tbl_StateTeamAllotment where StateName=@StateName)
begin

	 INSERT INTO Tbl_StateTeamAllotment
		(
		StateName,
		Fk_TeamId,
		Fk_UserId
		)
	 VALUES
		(
		@StateName,
		@Fk_TeamId,
		@Fk_UserId
		)
		return scope_identity();
		
		end
		else
		begin
		return  -1;
		
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_StateTeamAllotment_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_StateTeamAllotment_Delete]
	@Pk_AllotStateId int
As
BEGIN
	DELETE FROM Tbl_StateTeamAllotment
	WHERE	Pk_AllotStateId=@Pk_AllotStateId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareMaster_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareMaster_Update]
	@Name nvarchar(50),
	@Status bit,

	@Pk_SoftwareId bigint
As
BEGIN
	UPDATE	Tbl_SoftwareMaster
	SET	Name=@Name,
			Status=@Status
	WHERE	Pk_SoftwareId=@Pk_SoftwareId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareMaster_Select_All]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareMaster_Select_All]
	--@Pk_SoftwareId bigint
As
BEGIN
	SELECT * FROM Tbl_SoftwareMaster  (nolock)
	where Status=1
	--WHERE	Pk_SoftwareId=@Pk_SoftwareId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareMaster_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareMaster_Select]
	@Pk_SoftwareId bigint
As
BEGIN
	SELECT * FROM Tbl_SoftwareMaster (nolock)
	WHERE	Pk_SoftwareId=@Pk_SoftwareId and Status=1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareMaster_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareMaster_Insert]
	@Name nvarchar(50),
	@Status bit
As
BEGIN
	 INSERT INTO Tbl_SoftwareMaster
		(
		Name,
		Status
		)
	 VALUES
		(
		@Name,
		@Status
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareMaster_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareMaster_Delete]
	@Pk_SoftwareId bigint
As
BEGIN
	DELETE FROM Tbl_SoftwareMaster
	WHERE	Pk_SoftwareId=@Pk_SoftwareId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareDetail_Update]
	@DetailName nvarchar(50),
	@Status bit,
	@Fk_SoftwareId bigint,

	@Pk_SoftwareDetail bigint
As
BEGIN
	UPDATE	Tbl_SoftwareDetail
	SET	DetailName=@DetailName,
			Status=@Status,
			Fk_SoftwareId=@Fk_SoftwareId
	WHERE	Pk_SoftwareDetail=@Pk_SoftwareDetail
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareDetail_Select]
	@SoftwareName nvarchar(50)
As
BEGIN
	SELECT Pk_SoftwareDetail,DetailName FROM Tbl_SoftwareDetail sd (nolock)
	inner join Tbl_SoftwareMaster sm (nolock)
	on sm.Pk_SoftwareId=sd.Fk_SoftwareId
	WHERE	sm.Name like @SoftwareName and sm.Status=1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareDetail_Insert]
	@DetailName nvarchar(50),
	@Status bit,
	@Fk_SoftwareId bigint
As
BEGIN
	 INSERT INTO Tbl_SoftwareDetail
		(
		DetailName,
		Status,
		Fk_SoftwareId
		)
	 VALUES
		(
		@DetailName,
		@Status,
		@Fk_SoftwareId
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SoftwareDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SoftwareDetail_Delete]
	@Pk_SoftwareDetail bigint
As
BEGIN
	DELETE FROM Tbl_SoftwareDetail
	WHERE	Pk_SoftwareDetail=@Pk_SoftwareDetail
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Update]
	@Fk_AddressId bigint,
	@EngineerName nvarchar(50),
	@ReportBy nvarchar(50),
	@PerformanceCategory nvarchar(30),
	@Remarks nvarchar(max)
As
BEGIN
	UPDATE	Tbl_Service_TechPerformanceRepDetail
	SET	
			EngineerName=@EngineerName,
			ReportBy=@ReportBy,
			PerformanceCategory=@PerformanceCategory,
			Remarks=@Remarks
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_TechPerformanceRepDetail (nolock)
	WHERE Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Insert]
	@Fk_AddressId bigint,
	@EngineerName nvarchar(50),
	@ReportBy nvarchar(50),
	@PerformanceCategory nvarchar(30),
	@Remarks nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_TechPerformanceRepDetail
		(
		Fk_AddressId,
		EngineerName,
		ReportBy,
		PerformanceCategory,
		Remarks
		)
	 VALUES
		(
		@Fk_AddressId,
		@EngineerName,
		@ReportBy,
		@PerformanceCategory,
		@Remarks
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_TechPerformanceRepDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_TechPerformanceRepDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareMaster_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareMaster_Update]
	@Fk_Addressid bigint,
	@SpareOrderDate datetime,
	@RecieveBy nvarchar(50),
	@DispatchVia nvarchar(50),
	@DispatchBy nvarchar(50),
	@Remarks nvarchar(max)
As
BEGIN
	UPDATE	Tbl_Service_SpareMaster
	SET
			SpareOrderDate=@SpareOrderDate,
			RecieveBy=@RecieveBy,
			DispatchVia=@DispatchVia,
			DispatchBy=@DispatchBy,
			Remarks=@Remarks
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareMaster_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_SpareMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareMaster_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareMaster_Insert]
	@Fk_Addressid bigint,
	@SpareOrderDate datetime,
	@RecieveBy nvarchar(50),
	@DispatchVia nvarchar(50),
	@DispatchBy nvarchar(50),
	@Remarks nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_SpareMaster
		(
		Fk_Addressid,
		SpareOrderDate,
		RecieveBy,
		DispatchVia,
		DispatchBy,
		Remarks
		)
	 VALUES
		(
		@Fk_Addressid,
		@SpareOrderDate,
		@RecieveBy,
		@DispatchVia,
		@DispatchBy,
		@Remarks
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareMaster_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_SpareMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareDetail_Update]
	@Fk_AddressId bigint,
	@ItemName nvarchar(50),
	@Qty int,
	@Price numeric(18,2),
	@Amount numeric(18,2)
As
BEGIN
	UPDATE	Tbl_Service_SpareDetail
	SET	
			ItemName=@ItemName,
			Qty=@Qty,
			Price=@Price,
			Amount=@Amount
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_SpareDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareDetail_Insert]
	@Fk_AddressId bigint,
	@ItemName nvarchar(50),
	@Qty int,
	@Price numeric(18,2),
	@Amount numeric(18,2)
As
BEGIN
	 INSERT INTO Tbl_Service_SpareDetail
		(
		Fk_AddressId,
		ItemName,
		Qty,
		Price,
		Amount
		)
	 VALUES
		(
		@Fk_AddressId,
		@ItemName,
		@Qty,
		@Price,
		@Amount
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_SpareDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_SpareDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_SpareDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceLogMaster_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceLogMaster_Update]
	@Fk_AddressId bigint,
	@ServiceType nvarchar(50),
	@ServiceDate datetime,
	@Status nvarchar(50),
	@ServiceDoneDate datetime

As
BEGIN
	UPDATE	Tbl_Service_ServiceLogMaster
	SET
			ServiceType=@ServiceType,
			ServiceDate=@ServiceDate,
			Status=@Status,
			ServiceDoneDate=@ServiceDoneDate
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceLogMaster_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceLogMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ServiceLogMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceLogMaster_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceLogMaster_Insert]
	@Fk_AddressId bigint,
	@ServiceType nvarchar(50),
	@ServiceDate datetime,
	@Status nvarchar(50),
	@ServiceDoneDate datetime
As
BEGIN
	 INSERT INTO Tbl_Service_ServiceLogMaster
		(
		Fk_AddressId,
		ServiceType,
		ServiceDate,
		Status,
		ServiceDoneDate
		)
	 VALUES
		(
		@Fk_AddressId,
		@ServiceType,
		@ServiceDate,
		@Status,
		@ServiceDoneDate
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceLogMaster_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceLogMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ServiceLogMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceDetail_Update]
	@Fk_AddressId bigint,
	@DelServiceDate datetime,
	@EngineerName nvarchar(50),
	@Remarks nvarchar(max),
	@I_TDS nvarchar(20),
	@I_TH nvarchar(20),
	@I_PH nvarchar(20),
	@I_Remarks nvarchar(max),
	@O_TDS nvarchar(20),
	@O_TH nvarchar(20),
	@O_PH nvarchar(20),
	@O_Remarks nvarchar(max),
	@ServiceType nvarchar(50),
	@isDone bit,
	@DoneDate datetime
As
BEGIN
	UPDATE	Tbl_Service_ServiceDetail
	SET	
			DelServiceDate=@DelServiceDate,
			EngineerName=@EngineerName,
			Remarks=@Remarks,
			I_TDS=@I_TDS,
			I_TH=@I_TH,
			I_PH=@I_PH,
			I_Remarks=@I_Remarks,
			O_TDS=@O_TDS,
			O_TH=@O_TH,
			O_PH=@O_PH,
			O_Remarks=@O_Remarks,
			ServiceType=@ServiceType,
			isDone=@isDone,
			DoneDate=@DoneDate
	WHERE	Fk_AddressId=@Fk_AddressId
	
	if(@isDone=1)
	begin
	
	update Tbl_Service_ServiceLogMaster
	set ServiceDoneDate=@DoneDate,
	Status='Done'
	where ServiceType=@ServiceType and Fk_AddressId=@Fk_AddressId
	
	if(@ServiceType='Service1')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service2' and Fk_AddressId=@Fk_AddressId
	
	end
	
	if(@ServiceType='Service2')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service3' and Fk_AddressId=@Fk_AddressId
	end
	 if(@ServiceType='Service3')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service4' and Fk_AddressId=@Fk_AddressId
	end
	 if(@ServiceType='Service4')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service5' and Fk_AddressId=@Fk_AddressId
	end
	
	end
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ServiceDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceDetail_Insert]
	@Fk_AddressId bigint,
	@DelServiceDate datetime,
	@EngineerName nvarchar(50),
	@Remarks nvarchar(max),
	@I_TDS nvarchar(20),
	@I_TH nvarchar(20),
	@I_PH nvarchar(20),
	@I_Remarks nvarchar(max),
	@O_TDS nvarchar(20),
	@O_TH nvarchar(20),
	@O_PH nvarchar(20),
	@O_Remarks nvarchar(max),
	@ServiceType nvarchar(50),
	@isDone bit,
	@DoneDate datetime
As
BEGIN
	 INSERT INTO Tbl_Service_ServiceDetail
		(
		Fk_AddressId,
		DelServiceDate,
		EngineerName,
		Remarks,
		I_TDS,
		I_TH,
		I_PH,
		I_Remarks,
		O_TDS,
		O_TH,
		O_PH,
		O_Remarks,
		ServiceType,
		isDone,
		DoneDate
		)
	 VALUES
		(
		@Fk_AddressId,
		@DelServiceDate,
		@EngineerName,
		@Remarks,
		@I_TDS,
		@I_TH,
		@I_PH,
		@I_Remarks,
		@O_TDS,
		@O_TH,
		@O_PH,
		@O_Remarks,
		@ServiceType,
		@isDone,
		@DoneDate
		)
		
	if(@isDone=1)
	begin
	
	update Tbl_Service_ServiceLogMaster
	set ServiceDoneDate=@DoneDate,
	Status='Done'
	where ServiceType=@ServiceType and Fk_AddressId=@Fk_AddressId
	
	if(@ServiceType='Service1')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service2' and Fk_AddressId=@Fk_AddressId
	
	end
	
	if(@ServiceType='Service2')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service3' and Fk_AddressId=@Fk_AddressId
	end
	 if(@ServiceType='Service3')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service4' and Fk_AddressId=@Fk_AddressId
	end
	 if(@ServiceType='Service4')
	begin
	update Tbl_Service_ServiceLogMaster
	set ServiceDate=DATEADD (MONTH , 3 , @DoneDate )
	Where ServiceType='Service5' and Fk_AddressId=@Fk_AddressId
	end
	
	end

END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ServiceDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ServiceDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ServiceDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ProjectDetail_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ProjectDetail_Update]
	@Fk_AddressId bigint,
	@MOC nvarchar(50),
	@PlantType nvarchar(30),
	@InspectionBy nvarchar(50),
	@InspectionDate datetime
As
BEGIN
	UPDATE	Tbl_Service_ProjectDetail
	SET	Fk_AddressId=@Fk_AddressId,
			MOC=@MOC,
			PlantType=@PlantType,
			InspectionBy=@InspectionBy,
			InspectionDate=@InspectionDate
	WHERE	Fk_AddressId=@Fk_AddressId
	return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ProjectDetail_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ProjectDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ProjectDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ProjectDetail_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ProjectDetail_Insert]
	@Fk_AddressId bigint,
	@MOC nvarchar(50),
	@PlantType nvarchar(30),
	@InspectionBy nvarchar(50),
	@InspectionDate datetime
As
BEGIN
	 INSERT INTO Tbl_Service_ProjectDetail
		(
		Fk_AddressId,
		MOC,
		PlantType,
		InspectionBy,
		InspectionDate
		)
	 VALUES
		(
		@Fk_AddressId,
		@MOC,
		@PlantType,
		@InspectionBy,
		@InspectionDate
		)
		return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ProjectDetail_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ProjectDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ProjectDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Update]
	@Fk_AddressId bigint,
	@BottleConvStatus bit,
	@BottleConvRemarks nvarchar(200),
	@BottleCompStatus bit,
	@BottleCompRemarks nvarchar(200),
	@BlowCompStatus bit,
	@BlowCompRemarks nvarchar(200),
	@BlowAirStatus bit,
	@BlowAirRemarks nvarchar(200),
	@LabPack1Status bit,
	@LabPack1Remarks nvarchar(200),
	@LabPack2Status bit,
	@LabPack2Remarks nvarchar(200),
	@Other1Status bit,
	@Other1Remarks nvarchar(500),
	@Other2Status bit,
	@Other2Remarks nvarchar(500),
	@PendingMaterials nvarchar(max)

	
As
BEGIN
	UPDATE	Tbl_Service_PhysicalVerificationDayOneMaster
	SET	Fk_AddressId=@Fk_AddressId,
			BottleConvStatus=@BottleConvStatus,
			BottleConvRemarks=@BottleConvRemarks,
			BottleCompStatus=@BottleCompStatus,
			BottleCompRemarks=@BottleCompRemarks,
			BlowCompStatus=@BlowCompStatus,
			BlowCompRemarks=@BlowCompRemarks,
			BlowAirStatus=@BlowAirStatus,
			BlowAirRemarks=@BlowAirRemarks,
			LabPack1Status=@LabPack1Status,
			LabPack1Remarks=@LabPack1Remarks,
			LabPack2Status=@LabPack2Status,
			LabPack2Remarks=@LabPack2Remarks,
			Other1Status=@Other1Status,
			Other1Remarks=@Other1Remarks,
			Other2Status=@Other2Status,
			Other2Remarks=@Other2Remarks,
			PendingMaterials=@PendingMaterials
	WHERE	Fk_AddressId=@Fk_AddressId
	return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_PhysicalVerificationDayOneMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Insert]
	@Fk_AddressId bigint,
	@BottleConvStatus bit,
	@BottleConvRemarks nvarchar(200),
	@BottleCompStatus bit,
	@BottleCompRemarks nvarchar(200),
	@BlowCompStatus bit,
	@BlowCompRemarks nvarchar(200),
	@BlowAirStatus bit,
	@BlowAirRemarks nvarchar(200),
	@LabPack1Status bit,
	@LabPack1Remarks nvarchar(200),
	@LabPack2Status bit,
	@LabPack2Remarks nvarchar(200),
	@Other1Status bit,
	@Other1Remarks nvarchar(500),
	@Other2Status bit,
	@Other2Remarks nvarchar(500),
	@PendingMaterials nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_PhysicalVerificationDayOneMaster
		(
		Fk_AddressId,
		BottleConvStatus,
		BottleConvRemarks,
		BottleCompStatus,
		BottleCompRemarks,
		BlowCompStatus,
		BlowCompRemarks,
		BlowAirStatus,
		BlowAirRemarks,
		LabPack1Status,
		LabPack1Remarks,
		LabPack2Status,
		LabPack2Remarks,
		Other1Status,
		Other1Remarks,
		Other2Status,
		Other2Remarks,
		PendingMaterials
		)
	 VALUES
		(
		@Fk_AddressId,
		@BottleConvStatus,
		@BottleConvRemarks,
		@BottleCompStatus,
		@BottleCompRemarks,
		@BlowCompStatus,
		@BlowCompRemarks,
		@BlowAirStatus,
		@BlowAirRemarks,
		@LabPack1Status,
		@LabPack1Remarks,
		@LabPack2Status,
		@LabPack2Remarks,
		@Other1Status,
		@Other1Remarks,
		@Other2Status,
		@Other2Remarks,
		@PendingMaterials
		)
		
		return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Delete]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PhysicalVerificationDayOneMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_PhysicalVerificationDayOneMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Update]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Update]
	@Fk_AddressID bigint,
	@WaterType nvarchar(50),
	@Status bit,
	@Remarks nvarchar(max)

	
As
BEGIN
	UPDATE	Tbl_Service_Physical_WaterTreatment_Master
	SET	Fk_AddressID=@Fk_AddressID,
			WaterType=@WaterType,
			Status=@Status,
			Remarks=@Remarks
	WHERE	Fk_AddressId=@Fk_AddressId
	return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Select]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_Physical_WaterTreatment_Master (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Insert]    Script Date: 12/20/2013 14:35:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Insert]
	@Fk_AddressID bigint,
	@WaterType nvarchar(50),
	@Status bit,
	@Remarks nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_Physical_WaterTreatment_Master
		(
		Fk_AddressID,
		WaterType,
		Status,
		Remarks
		)
	 VALUES
		(
		@Fk_AddressID,
		@WaterType,
		@Status,
		@Remarks
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_Physical_WaterTreatment_Master_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_Physical_WaterTreatment_Master
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyRemarksMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyRemarksMaster_Update]
	@Fk_AddressId bigint,
	@Document1 nvarchar(max),
	@Document2 nvarchar(max),
	@Document3 nvarchar(max)
As
BEGIN
	UPDATE	Tbl_Service_PartyRemarksMaster
	SET	Fk_AddressId=@Fk_AddressId,
			Document1=@Document1,
			Document2=@Document2,
			Document3=@Document3
	WHERE	Fk_AddressId=@Fk_AddressId
	return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyRemarksMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyRemarksMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_PartyRemarksMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyRemarksMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyRemarksMaster_Insert]
	@Fk_AddressId bigint,
	@Document1 nvarchar(max),
	@Document2 nvarchar(max),
	@Document3 nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_PartyRemarksMaster
		(
		Fk_AddressId,
		Document1,
		Document2,
		Document3
		)
	 VALUES
		(
		@Fk_AddressId,
		@Document1,
		@Document2,
		@Document3
		)
		
		return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyRemarksMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyRemarksMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_PartyRemarksMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessMaster_Update]
	@Fk_AddressId bigint,
	@QualityReportPath nvarchar(max)
	
As
BEGIN
	UPDATE	Tbl_Service_PartyReadynessMaster
	SET	Fk_AddressId=@Fk_AddressId,
			QualityReportPath=@QualityReportPath
	WHERE	Fk_AddressId=@Fk_AddressId
	return Scope_identity();
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_PartyReadynessMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessMaster_Insert]
	@Fk_AddressId bigint,
	@QualityReportPath nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_PartyReadynessMaster
		(
		Fk_AddressId,
		QualityReportPath
		)
	 VALUES
		(
		@Fk_AddressId,
		@QualityReportPath
		)
		return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_PartyReadynessMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessDetail_Update]
	@Fk_AddressId bigint,
	@HeaderName nvarchar(50),
	@Status nvarchar(20),
	@CompletionDate datetime,
	@Remarks nvarchar(max)

	
As
BEGIN
	UPDATE	Tbl_Service_PartyReadynessDetail
	SET	Fk_AddressId=@Fk_AddressId,
			HeaderName=@HeaderName,
			Status=@Status,
			CompletionDate=@CompletionDate,
			Remarks=@Remarks
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_PartyReadynessDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessDetail_Insert]
	@Fk_AddressId bigint,
	@HeaderName nvarchar(50),
	@Status nvarchar(20),
	@CompletionDate datetime,
	@Remarks nvarchar(max),
	@PartyReadyDate datetime
As
BEGIN
	 INSERT INTO Tbl_Service_PartyReadynessDetail
		(
		Fk_AddressId,
		HeaderName,
		Status,
		CompletionDate,
		Remarks,
		PartyReadyDate
		)
	 VALUES
		(
		@Fk_AddressId,
		@HeaderName,
		@Status,
		@CompletionDate,
		@Remarks,
		@PartyReadyDate
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_PartyReadynessDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_PartyReadynessDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_PartyReadynessDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceMaster_Update]
	@GSDate datetime,
	@ServiceType nvarchar(50),
	@ComplainNo nvarchar(30),
	@AttendDate datetime,
	@AttendBy nvarchar(50),
	@Engineer nvarchar(50),
	
	@Fk_AddressId bigint

As
BEGIN
	UPDATE	Tbl_Service_GeneralServiceMaster
	SET	GSDate=@GSDate,
			ServiceType=@ServiceType,
			ComplainNo=@ComplainNo,
			AttendDate=@AttendDate,
			AttendBy=@AttendBy,
			Engineer=@Engineer
		
			
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_GeneralServiceMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceMaster_Insert]
	@GSDate datetime,
	@ServiceType nvarchar(50),
	@ComplainNo nvarchar(30),
	@AttendDate datetime,
	@AttendBy nvarchar(50),
	@Engineer nvarchar(50),
	
	@Fk_AddressId bigint
As
BEGIN
	 INSERT INTO Tbl_Service_GeneralServiceMaster
		(
		GSDate,
		ServiceType,
		ComplainNo,
		AttendDate,
		AttendBy,
		Engineer,
	
		Fk_AddressId
		)
	 VALUES
		(
		@GSDate,
		@ServiceType,
		@ComplainNo,
		@AttendDate,
		@AttendBy,
		@Engineer,
		
		@Fk_AddressId
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_GeneralServiceMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceDetail_Update]
	@Fk_AddressId bigint,
	@FollowUp nvarchar(100),
	@TentativeADate datetime,
	@Status nvarchar(50),
	@Remarks nvarchar(300)

As
BEGIN
	UPDATE	Tbl_Service_GeneralServiceDetail
	SET
			FollowUp=@FollowUp,
			TentativeADate=@TentativeADate,
			Status=@Status,
			Remarks=@Remarks
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_GeneralServiceDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceDetail_Insert]
	@Fk_AddressId bigint,
	@FollowUp nvarchar(100),
	@TentativeADate datetime,
	@Status nvarchar(50),
	@Remarks nvarchar(300)
As
BEGIN
	 INSERT INTO Tbl_Service_GeneralServiceDetail
		(
		Fk_AddressId,
		FollowUp,
		TentativeADate,
		Status,
		Remarks
		)
	 VALUES
		(
		@Fk_AddressId,
		@FollowUp,
		@TentativeADate,
		@Status,
		@Remarks
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_GeneralServiceDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_GeneralServiceDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_GeneralServiceDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommMaster_Update]
	@Fk_AddressID bigint,
	@Engg_Title1	nvarchar(50),
	@Engg_Name1	nvarchar(50),
@Engg_Title2	nvarchar(50),
@Engg_Name2	nvarchar(50),
@Engg_Title3	nvarchar(50),
@Engg_Name3	nvarchar(50),
@Engg_Title4	nvarchar(50),
@Engg_Name4	nvarchar(50),
@Engg_Title5	nvarchar(50),
@Engg_Name5	nvarchar(50),
	@Documentupload nvarchar(max),
	@ECDate datetime,
	@ECRemarks nvarchar(max)
As
BEGIN
	UPDATE	Tbl_Service_ErrectionCommMaster
	SET	  Fk_AddressID=@Fk_AddressID,
			Engg_Title1=@Engg_Title1,
		Engg_Name1=@Engg_Name1,
		Engg_Title2=@Engg_Title2,
		Engg_Name2=@Engg_Name2,
		Engg_Title3=@Engg_Title3,
		Engg_Name3=@Engg_Name3,
		Engg_Title4=@Engg_Title4,
		Engg_Name4=@Engg_Name4,
		Engg_Title5=@Engg_Title5,
		Engg_Name5=@Engg_Name5,
			Documentupload=@Documentupload,
			ECDate=@ECDate,
			ECRemarks=@ECRemarks
	WHERE	Fk_AddressId=@Fk_AddressId
	return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ErrectionCommMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommMaster_Insert]
	@Fk_AddressID bigint,
	
	@Engg_Title1	nvarchar(50),
	@Engg_Name1	nvarchar(50),
@Engg_Title2	nvarchar(50),
@Engg_Name2	nvarchar(50),
@Engg_Title3	nvarchar(50),
@Engg_Name3	nvarchar(50),
@Engg_Title4	nvarchar(50),
@Engg_Name4	nvarchar(50),
@Engg_Title5	nvarchar(50),
@Engg_Name5	nvarchar(50),
	@Documentupload nvarchar(max),
	@ECDate datetime,
	@ECRemarks nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_ErrectionCommMaster
		(
		Fk_AddressID,
		Engg_Title1,
		Engg_Name1,
		Engg_Title2,
		Engg_Name2,
		Engg_Title3,
		Engg_Name3,
		Engg_Title4,
		Engg_Name4,
		Engg_Title5,
		Engg_Name5,
		Documentupload,
		ECDate,
		ECRemarks
		)
	 VALUES
		(
		@Fk_AddressID,
		@Engg_Title1,
		@Engg_Name1,
		@Engg_Title2,
		@Engg_Name2,
		@Engg_Title3,
		@Engg_Name3,
		@Engg_Title4,
		@Engg_Name4,
		@Engg_Title5,
		@Engg_Name5,
		@Documentupload,
		@ECDate,
		@ECRemarks
		)
		
		return Scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ErrectionCommMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommDetails_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommDetails_Update]
	@Fk_AddressID bigint,
	@RawWaterValue nvarchar(50),
	@Parameter nvarchar(50),
	@TreatedWaterValue nvarchar(50)
As
BEGIN
	UPDATE	Tbl_Service_ErrectionCommDetails
	SET	
			RawWaterValue=@RawWaterValue,
			Parameter=@Parameter,
			TreatedWaterValue=@TreatedWaterValue
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommDetails_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommDetails_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ErrectionCommDetails (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommDetails_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommDetails_Insert]
	@Fk_AddressID bigint,
	@RawWaterValue nvarchar(50),
	@Parameter nvarchar(50),
	@TreatedWaterValue nvarchar(50)
As
BEGIN
	 INSERT INTO Tbl_Service_ErrectionCommDetails
		(
		Fk_AddressID,
		RawWaterValue,
		Parameter,
		TreatedWaterValue
		)
	 VALUES
		(
		@Fk_AddressID,
		@RawWaterValue,
		@Parameter,
		@TreatedWaterValue
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ErrectionCommDetails_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ErrectionCommDetails_Delete]  
 @Fk_AddressID bigint  
As  
BEGIN  
 DELETE FROM Tbl_Service_ErrectionCommDetails  
 WHERE Fk_AddressID =@Fk_AddressID  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngRepMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngRepMaster_Update]
	@Fk_AddressId bigint,
	@IsDone bit,
	@ECDoneDate datetime

	
As
BEGIN
	UPDATE	Tbl_Service_ECEngRepMaster
	SET	
			IsDone=@IsDone,
			ECDoneDate=@ECDoneDate
	WHERE	Fk_AddressId=@Fk_AddressId
	
	return @@rowcount
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngRepMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngRepMaster_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ECEngRepMaster (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngRepMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngRepMaster_Insert]
	@Fk_AddressId bigint,
	@IsDone bit,
	@ECDoneDate datetime
As
BEGIN
	 INSERT INTO Tbl_Service_ECEngRepMaster
		(
		Fk_AddressId,
		IsDone,
		ECDoneDate
		)
	 VALUES
		(
		@Fk_AddressId,
		@IsDone,
		@ECDoneDate
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngRepMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngRepMaster_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ECEngRepMaster
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Update]
	@Fk_AddressId bigint,
	@ECRepDate datetime,
	@ECStartTime nvarchar(20),
	@ECEndTime nvarchar(20),
	@ECWorkDone nvarchar(max),
	@ECIsWorkDelay bit,
	@ECWorkDelayRemarks nvarchar(max),
	@ECInstallationFor nvarchar(50),
	@ECRemarks nvarchar(max)

	
As
BEGIN
	UPDATE	Tbl_Service_ECEngineerReportDetail
	SET
			ECRepDate=@ECRepDate,
			ECStartTime=@ECStartTime,
			ECEndTime=@ECEndTime,
			ECWorkDone=@ECWorkDone,
			ECIsWorkDelay=@ECIsWorkDelay,
			ECWorkDelayRemarks=@ECWorkDelayRemarks,
			ECInstallationFor=@ECInstallationFor,
			ECRemarks=@ECRemarks
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ECEngineerReportDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Insert]
	@Fk_AddressId bigint,
	@ECRepDate datetime,
	@ECStartTime nvarchar(20),
	@ECEndTime nvarchar(20),
	@ECWorkDone nvarchar(max),
	@ECIsWorkDelay bit,
	@ECWorkDelayRemarks nvarchar(max),
	@ECInstallationFor nvarchar(50),
	@ECRemarks nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_ECEngineerReportDetail
		(
		Fk_AddressId,
		ECRepDate,
		ECStartTime,
		ECEndTime,
		ECWorkDone,
		ECIsWorkDelay,
		ECWorkDelayRemarks,
		ECInstallationFor,
		ECRemarks
		)
	 VALUES
		(
		@Fk_AddressId,
		@ECRepDate,
		@ECStartTime,
		@ECEndTime,
		@ECWorkDone,
		@ECIsWorkDelay,
		@ECWorkDelayRemarks,
		@ECInstallationFor,
		@ECRemarks
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECEngineerReportDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ECEngineerReportDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECDoneDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECDoneDetail_Update]
	@ECInstallationType nvarchar(50),
	@isDone bit,
	@ECDate datetime,
	@Fk_AddressId bigint
As
BEGIN
	UPDATE	Tbl_Service_ECDoneDetail
	SET	ECInstallationType=@ECInstallationType,
			isDone=@isDone,
			ECDate=@ECDate
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECDoneDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECDoneDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ECDoneDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECDoneDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECDoneDetail_Insert]
	@ECInstallationType nvarchar(50),
	@isDone bit,
	@ECDate datetime,
	@Fk_AddressId bigint
As
BEGIN
	 INSERT INTO Tbl_Service_ECDoneDetail
		(
		ECInstallationType,
		isDone,
		ECDate,
		Fk_AddressId
		)
	 VALUES
		(
		@ECInstallationType,
		@isDone,
		@ECDate,
		@Fk_AddressId
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ECDoneDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ECDoneDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ECDoneDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ContactInfo_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ContactInfo_Update]
	@Fk_AddressId bigint,
	@Name nvarchar(50),
	@Post nvarchar(50),
	@ContactNo nvarchar(50),
	@EmailId nvarchar(200)

As
BEGIN
	UPDATE	Tbl_Service_ContactInfo
	SET	
			Name=@Name,
			Post=@Post,
			ContactNo=@ContactNo,
			EmailId=@EmailId
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ContactInfo_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ContactInfo_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_ContactInfo (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ContactInfo_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ContactInfo_Insert]
	@Fk_AddressId bigint,
	@Name nvarchar(50),
	@Post nvarchar(50),
	@ContactNo nvarchar(50),
	@EmailId nvarchar(200)
As
BEGIN
	 INSERT INTO Tbl_Service_ContactInfo
		(
		Fk_AddressId,
		Name,
		Post,
		ContactNo,
		EmailId
		)
	 VALUES
		(
		@Fk_AddressId,
		@Name,
		@Post,
		@ContactNo,
		@EmailId
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_ContactInfo_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_ContactInfo_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_ContactInfo
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_CheckLogDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_CheckLogDetail_Update]
	@Fk_AddressId bigint,
	@ChkLogTitle nvarchar(200),
	@isDone bit,
	@LogDate datetime,
	@Remarks nvarchar(max)
As
BEGIN
	UPDATE	Tbl_Service_CheckLogDetail
	SET
			ChkLogTitle=@ChkLogTitle,
			isDone=@isDone,
			LogDate=@LogDate,
			Remarks=@Remarks
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_CheckLogDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_CheckLogDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_Service_CheckLogDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_CheckLogDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_CheckLogDetail_Insert]
	@Fk_AddressId bigint,
	@ChkLogTitle nvarchar(200),
	@isDone bit,
	@LogDate datetime,
	@Remarks nvarchar(max)
As
BEGIN
	 INSERT INTO Tbl_Service_CheckLogDetail
		(
		Fk_AddressId,
		ChkLogTitle,
		isDone,
		LogDate,
		Remarks
		)
	 VALUES
		(
		@Fk_AddressId,
		@ChkLogTitle,
		@isDone,
		@LogDate,
		@Remarks
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_Service_CheckLogDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_Service_CheckLogDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_Service_CheckLogDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SecondAllotmentDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SecondAllotmentDetail_Update]
	@Fk_AddressId bigint,
	@Fk_UserId int,

	@Pk_SecondAllotId bigint
As
BEGIN
if not exists (select * from Tbl_SecondAllotmentDetail where Fk_AddressId=@Fk_AddressId and Fk_UserId=@Fk_UserId)
begin

	UPDATE	Tbl_SecondAllotmentDetail
	SET	Fk_AddressId=@Fk_AddressId,
			Fk_UserId=@Fk_UserId
	WHERE	Pk_SecondAllotId=@Pk_SecondAllotId
	return 1;
	end
	else
	begin
	return -1;
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SecondAllotmentDetail_SelectByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SecondAllotmentDetail_SelectByUser]
@Fk_UserId bigint
As
BEGIN
	SELECT sa.Pk_SecondAllotId,um.UserName,am.EnqNo,sa.Fk_UserId,sa.Fk_AddressId FROM Tbl_SecondAllotmentDetail sa (nolock) 
	inner join User_Master um (nolock)
	on um.Pk_UserId=sa.Fk_UserId
	inner join Address_Master am (nolock)
	on am.Pk_AddressID=sa.Fk_AddressId
	
	
	
	WHERE	Fk_UserId=@Fk_UserId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SecondAllotmentDetail_SelectAll]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SecondAllotmentDetail_SelectAll]
	--@Pk_SecondAllotId bigint
As
BEGIN
	SELECT * FROM Tbl_SecondAllotmentDetail (nolock)
	--WHERE	Pk_SecondAllotId=@Pk_SecondAllotId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SecondAllotmentDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SecondAllotmentDetail_Select]
	@Pk_SecondAllotId bigint
As
BEGIN
	SELECT * FROM Tbl_SecondAllotmentDetail (nolock)
	WHERE	Pk_SecondAllotId=@Pk_SecondAllotId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SecondAllotmentDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SecondAllotmentDetail_Insert]
	@Fk_AddressId bigint,
	@Fk_UserId int
As
BEGIN

if not exists (select * from Tbl_SecondAllotmentDetail where Fk_AddressId=@Fk_AddressId and Fk_UserId=@Fk_UserId)
begin
	 INSERT INTO Tbl_SecondAllotmentDetail
		(
		Fk_AddressId,
		Fk_UserId
		)
	 VALUES
		(
		@Fk_AddressId,
		@Fk_UserId
		)
		return scope_identity();
		
end
else
begin
	return -1;
end		
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_SecondAllotmentDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_SecondAllotmentDetail_Delete]
	@Fk_AddressId bigint,
	@Fk_UserId int
As
BEGIN
	DELETE FROM Tbl_SecondAllotmentDetail
	WHERE	Fk_AddressId=@Fk_AddressId and
	Fk_UserId= @Fk_UserId 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_ProjectLetterLog_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_ProjectLetterLog_Update]
	@LetterTitle nvarchar(100),
	@MailBy nvarchar(50),
	@MailDate datetime,
	@CourierBy nvarchar(50),
	@CourierDate datetime,
	@RecMailBy nvarchar(50),
	@RecMailDate datetime,
	@RecCourierBy nvarchar(50),
	@RecCourierDate datetime,
	@Fk_AddressId bigint

	
As
BEGIN
	UPDATE	Tbl_ProjectLetterLog
	SET	LetterTitle=@LetterTitle,
			MailBy=@MailBy,
			MailDate=@MailDate,
			CourierBy=@CourierBy,
			CourierDate=@CourierDate,
			RecMailBy=@RecMailBy,
			RecMailDate=@RecMailDate,
			RecCourierBy=@RecCourierBy,
			RecCourierDate=@RecCourierDate
	
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_ProjectLetterLog_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_ProjectLetterLog_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_ProjectLetterLog (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_ProjectLetterLog_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_ProjectLetterLog_Insert]
	@LetterTitle nvarchar(100),
	@MailBy nvarchar(50),
	@MailDate datetime,
	@CourierBy nvarchar(50),
	@CourierDate datetime,
	@RecMailBy nvarchar(50),
	@RecMailDate datetime,
	@RecCourierBy nvarchar(50),
	@RecCourierDate datetime,
	@Fk_AddressId bigint
As
BEGIN
	 INSERT INTO Tbl_ProjectLetterLog
		(
		LetterTitle,
		MailBy,
		MailDate,
		CourierBy,
		CourierDate,
		RecMailBy,
		RecMailDate,
		RecCourierBy,
		RecCourierDate,
		Fk_AddressId
		)
	 VALUES
		(
		@LetterTitle,
		@MailBy,
		@MailDate,
		@CourierBy,
		@CourierDate,
		@RecMailBy,
		@RecMailDate,
		@RecCourierBy,
		@RecCourierDate,
		@Fk_AddressId
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_ProjectLetterLog_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_ProjectLetterLog_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_ProjectLetterLog
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_PermissionMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_PermissionMaster_Update]
	@Fk_SoftwareID bigint,
	@Fk_SoftwareDetailId bigint,	
	@Fk_UserId bigint,
	@Type int,
	@IsAdd bit,
	@IsUpdate bit,
	@IsDelete bit,
	@IsPrint bit,
	@CreationDate datetime, 
	@Pk_PermissionId bigint
As
BEGIN
	UPDATE	Tbl_PermissionMaster
	SET	
			FK_SoftwareID = @Fk_SoftwareID,
			Fk_SoftwareDetailId=@Fk_SoftwareDetailId,
			Fk_UserId=@Fk_UserId,
			Type = @Type,
			IsAdd=@IsAdd,
			IsUpdate=@IsUpdate,
			IsDelete=@IsDelete,
			IsPrint=@IsPrint,
			CreationDate = @CreationDate
	WHERE	Pk_PermissionId=@Pk_PermissionId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_PermissionMaster_SelectByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_PermissionMaster_SelectByUser]
	@UserId bigint
As
BEGIN
	SELECT pm.*,Pk_SoftwareId,DetailName,Name FROM Tbl_PermissionMaster pm (nolock)
	inner join Tbl_SoftwareMaster sm (nolock)
	on sm.Pk_SoftwareId=pm.Fk_SoftwareId
	Left outer join Tbl_SoftwareDetail sd (nolock) 
	on sd.Pk_SoftwareDetail=pm.Fk_SoftwareDetailId
	
	WHERE	Fk_UserId=@UserId
	
	order by sm.Pk_SoftwareId ,sd.DetailName 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_PermissionMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_PermissionMaster_Select]
	@Pk_PermissionId bigint
As
BEGIN
	SELECT * FROM Tbl_PermissionMaster (nolock)
	WHERE	Pk_PermissionId=@Pk_PermissionId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_PermissionMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_PermissionMaster_Insert]
	@Fk_SoftwareId bigInt,
	@Fk_SoftwareDetailId bigint,
	@Fk_UserId bigint,
	@Type int,
	@IsAdd bit,
	@IsUpdate bit,
	@IsDelete bit,
	@IsPrint bit,
	@CreationDate datetime
As
BEGIN
	 INSERT INTO Tbl_PermissionMaster
		(
		FK_SoftwareID ,
		Fk_SoftwareDetailId,
		Fk_UserId,
		Type , 
		IsAdd,
		IsUpdate,
		IsDelete,
		IsPrint,
		CreationDate 
		)
	 VALUES
		(
		@FK_SoftwareID,
		@Fk_SoftwareDetailId,
		@Fk_UserId,
		@Type,
		@IsAdd,
		@IsUpdate,
		@IsDelete,
		@IsPrint,
		@CreationDate 
		)
		
		return scope_identity();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_PermissionMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_PermissionMaster_Delete]
	@Fk_UserID bigint
As
BEGIN
	DELETE FROM Tbl_PermissionMaster
	WHERE Tbl_PermissionMaster.Fk_UserId = @Fk_UserID 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderRawMaterialDetail_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderRawMaterialDetail_Update]
	@ItemName nvarchar(50),
	@Qty int,
	@Rate numeric(18,2),
	@Amount numeric(18,2),
	@IsOrderConfirm bit,
	@IsPaymentReceived bit,
	@Fk_AddressId bigint
As
BEGIN
	UPDATE	Tbl_OrderRawMaterialDetail
	SET	ItemName=@ItemName,
		Qty=@Qty,
		Rate=@Rate,
		Amount=@Amount,
		IsOrderConfirm=@IsOrderConfirm,
		IsPaymentReceived=@IsPaymentReceived
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderRawMaterialDetail_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderRawMaterialDetail_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_OrderRawMaterialDetail (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderRawMaterialDetail_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderRawMaterialDetail_Insert]
	@Fk_AddressId bigint,
	@ItemName nvarchar(50),
	@Qty int,
	@Rate numeric(18,2),
	@Amount numeric(18,2),
	@IsOrderConfirm bit,
	@IsPaymentReceived bit,
	@OrderDate Datetime ,
	@DisDate DateTime
As
BEGIN
	
	if @OrderDate ='01-01-1900' 
		set @OrderDate = null
		
	if @DisDate ='01-01-1900' 
		set @DisDate = null
		
	
	
	
	
	INSERT INTO Tbl_OrderRawMaterialDetail
		(
		Fk_AddressId,
		ItemName,
		Qty,
		Rate,
		Amount,
		IsOrderConfirm,
		IsPaymentReceived,
		OrderDate,
		disDate
		)
	 VALUES
		(
		@Fk_AddressId,
		@ItemName,
		@Qty,
		@Rate,
		@Amount,
		@IsOrderConfirm,
		@IsPaymentReceived,
		@OrderDate ,
		@DisDate 
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderRawMaterialDetail_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderRawMaterialDetail_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_OrderRawMaterialDetail
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderPlanDrawing_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderPlanDrawing_Update]
	@Draw1Val1 nvarchar(50),
	@Draw1Val2 nvarchar(200),
	@Draw1Image1 nvarchar(max),
	@Draw2Val1 nvarchar(50),
	@Draw2Val2 nvarchar(200),
	@Draw2Image2 nvarchar(max),
	@Draw3Val1 nvarchar(50),
	@Draw3Val2 nvarchar(200),
	@Draw3Image3 nvarchar(max),
	@Fk_AddressId bigint

	
As
BEGIN
	UPDATE	Tbl_OrderPlanDrawing
	SET	Draw1Val1=@Draw1Val1,
			Draw1Val2=@Draw1Val2,
			Draw1Image1=@Draw1Image1,
			Draw2Val1=@Draw2Val1,
			Draw2Val2=@Draw2Val2,
			Draw2Image2=@Draw2Image2,
			Draw3Val1=@Draw3Val1,
			Draw3Val2=@Draw3Val2,
			Draw3Image3=@Draw3Image3
		
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderPlanDrawing_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderPlanDrawing_Select]
	@Fk_AddressId bigint
As
BEGIN
	SELECT * FROM Tbl_OrderPlanDrawing  (nolock)
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderPlanDrawing_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderPlanDrawing_Insert]
	@Draw1Val1 nvarchar(50),
	@Draw1Val2 nvarchar(200),
	@Draw1Image1 nvarchar(max),
	@Draw2Val1 nvarchar(50),
	@Draw2Val2 nvarchar(200),
	@Draw2Image2 nvarchar(max),
	@Draw3Val1 nvarchar(50),
	@Draw3Val2 nvarchar(200),
	@Draw3Image3 nvarchar(max),
	@Fk_AddressId bigint
As
BEGIN

if not exists(select Fk_AddressId from Tbl_OrderPlanDrawing where Fk_AddressId=@Fk_AddressId)
begin

	 INSERT INTO Tbl_OrderPlanDrawing
		(
		Draw1Val1,
		Draw1Val2,
		Draw1Image1,
		Draw2Val1,
		Draw2Val2,
		Draw2Image2,
		Draw3Val1,
		Draw3Val2,
		Draw3Image3,
		Fk_AddressId
		)
	 VALUES
		(
		@Draw1Val1,
		@Draw1Val2,
		@Draw1Image1,
		@Draw2Val1,
		@Draw2Val2,
		@Draw2Image2,
		@Draw3Val1,
		@Draw3Val2,
		@Draw3Image3,
		@Fk_AddressId
		)
		return scope_Identity();
		end
		else
		begin		
		return -1;
		end
		
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_OrderPlanDrawing_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_OrderPlanDrawing_Delete]
	@Fk_AddressId bigint
As
BEGIN
	DELETE FROM Tbl_OrderPlanDrawing
	WHERE	Fk_AddressId=@Fk_AddressId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_DesignationMaster_Update]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_DesignationMaster_Update]
	@DesignationTitle nvarchar(50),
	@Status bit,

	@Pk_DesignationMaster int
As
BEGIN
	UPDATE	Tbl_DesignationMaster
	SET	DesignationTitle=@DesignationTitle,
			Status=@Status
	WHERE	Pk_DesignationMaster=@Pk_DesignationMaster
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_DesignationMaster_SelectAll]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_DesignationMaster_SelectAll]
--	@Pk_DesignationMaster int
As
BEGIN
	SELECT * FROM Tbl_DesignationMaster  (nolock)
	--WHERE	Pk_DesignationMaster=@Pk_DesignationMaster
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_DesignationMaster_Select]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_DesignationMaster_Select]
	@Pk_DesignationMaster int
As
BEGIN
	SELECT * FROM Tbl_DesignationMaster  (nolock)
	WHERE	Pk_DesignationMaster=@Pk_DesignationMaster
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_DesignationMaster_Insert]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_DesignationMaster_Insert]
	@DesignationTitle nvarchar(50),
	@Status bit
As
BEGIN
	 INSERT INTO Tbl_DesignationMaster
		(
		DesignationTitle,
		Status
		)
	 VALUES
		(
		@DesignationTitle,
		@Status
		)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Tbl_DesignationMaster_Delete]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Tbl_DesignationMaster_Delete]
	@Pk_DesignationMaster int
As
BEGIN
	DELETE FROM Tbl_DesignationMaster
	WHERE	Pk_DesignationMaster=@Pk_DesignationMaster
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_User_Permission]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_User_Permission]

           
AS
Begin

	SELECT Pk_PermissionId
      ,[Fk_UserID]
      ,[Fk_SoftwareID]
      ,[Fk_SoftwareDetailID]
      ,[Type]
      ,IsAdd
      ,IsUpdate
      ,IsDelete
      ,IsPrint
      ,[CreationDate]
  FROM [Tbl_PermissionMaster]  (nolock)


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ServiceMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ServiceMaster]        
 @Address bigint  =0      
AS        
Begin        
        
  if @Address =0       
 set @Address =null       
       
select * from [dbo].Tbl_ServiceMaster  (nolock)    s    
left outer join Address_Master v on s.Fk_Address =v.Pk_AddressID     
where   s.Fk_Address= ISNULL(@Address ,s.Fk_Address )       
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ReInwardMaster_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ReInwardMaster_ById]
@Pk_REInwardId bigint
           
           AS
begin

select * from [Tbl_ReInwardMaster] m (nolock)

where Pk_ReInwardId=@Pk_REInwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ReInwardDetail_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ReInwardDetail_ById]
 @Pk_RIInwardDetailId bigint
           AS
begin

select * from [Tbl_RIInwardDetail] (nolock)
where Pk_RIInwardDetailId=@Pk_RIInwardDetailId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ProjectInformationMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ProjectInformationMaster_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_ProjectInformationMaster_Two  (nolock) where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ProjectInformationMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ProjectInformationMaster]

           @Fk_AddressId bigint
           
AS
Begin
Select * from [Tbl_ProjectInformationMaster] (nolock)
         where [Fk_AddressId]=@Fk_AddressId
     
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ProjectDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ProjectDetail]
@Fk_AddressId bigint
          
AS
Begin
select * from [Tbl_ProjectDetail] (nolock)

 where Fk_AddressId=@Fk_AddressId
        

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ProductInstallationMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ProductInstallationMaster_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_ProductInstallationMaster_Two  (nolock) where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_PartyPDCDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_PartyPDCDetail]
@Fk_AddressId bigint
         
AS
Begin
Select * From [Tbl_PartyPDCDetail] (nolock)
where [Fk_AddressId]=@Fk_AddressId
          


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_PartyCFormDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_PartyCFormDetail]
@Fk_AddressId bigint
        
AS
Begin

select * from [Tbl_PartyCFormDetail] (nolock)
           where [Fk_AddressId]=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_PackagingMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_PackagingMaster]

           @Fk_AddressId bigint
          

AS
Begin

select * from [Tbl_PackagingDetail] (nolock)

           where [Fk_AddressId]=@Fk_AddressId
  
           



End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OutwardMaster_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OutwardMaster_ById]
 @Pk_OutwardId bigint
        
           AS
begin
select outward.* from [Tbl_OutwardMaster] outward (nolock)

where outward.Pk_OutwardId=@Pk_OutwardId
         
      
     

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OutwardDetail_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OutwardDetail_ById]
  
   
@Pk_OutwardDetailId bigint

AS
begin
select * from [Tbl_OutwardDetail] (nolock)
where Pk_OutwardDetailId=@Pk_OutwardDetailId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderVisitorDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderVisitorDetail]

           @Fk_AddressId bigint
AS
Begin
select * from [Tbl_OrderVisitorDetail] (nolock)

           where Fk_AddressId=@Fk_AddressId
    


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderServiceFollowUpDetailByOrder]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderServiceFollowUpDetailByOrder]
           @Fk_AddressId bigint
As
begin
select * from [Tbl_OrderServiceFollowUpDetail] (nolock)

       where [Fk_AddressId]=@Fk_AddressId
  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderPartyMasterById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderPartyMasterById]
@Fk_AddressId bigint
AS
begin

select * from Party_Master(nolock)
 where Fk_AddressId=@Fk_AddressId
           
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderParty_Master_ByAddressId]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderParty_Master_ByAddressId]

@Address bigint

AS
Begin

select * from [dbo].Tbl_OrderPartyMaster (nolock)
where Fk_AddressId=@Address


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderOutstandingDetailByOrder]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderOutstandingDetailByOrder]
@Fk_AddressId bigint
        
AS
begin

select * from [Tbl_OrderOutstandingDetail] (nolock)
           where [Fk_AddressId]=@Fk_AddressId
    
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderMaster_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_OrderMaster_Two (nolock) where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_OrderFollowupMasterByOrder]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_OrderFollowupMasterByOrder]
--@ProjectDetail nvarchar(200),
@Fk_AddressId bigint
AS
begin

select * from [Tbl_OrderFollowupMaster] (nolock)
--set [ProjectDetail]=@ProjectDetail
where Fk_AddressId=@Fk_AddressId
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_LetterMailComMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_LetterMailComMaster_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_LetterMailComMaster_Two (nolock)
 where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_LetterMailComMaster_Detail_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_LetterMailComMaster_Detail_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_LetterMailComMaster_Detail_Two (nolock)
 where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ISIProcessMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ISIProcessMaster_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_ISIProcessMaster_Two (nolock)
where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_ISIProcess_DetailMaster_Two]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_ISIProcess_DetailMaster_Two]
@Fk_AddressId bigint
AS
Begin
select * from Tbl_ISIProcess_DetailMaster_Two (nolock) where Fk_AddressId=@Fk_AddressId
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_InwardStockMaster_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_InwardStockMaster_ById]


          @Pk_InwardId bigint
           
           AS
begin

select * from [Tbl_InwardStockMaster] i (nolock)

where Pk_InwardId=@Pk_InwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_InwardDetail_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_InwardDetail_ById]
 @Pk_InwardDetailId bigint
           AS
begin

select * from [Tbl_InwardDetail] (nolock)
where Pk_InwardDetailId=@Pk_InwardDetailId
     
           
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_InvoiceMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_InvoiceMaster]
@Fk_AddressId bigint
 AS
begin
Select * from [Tbl_InvoiceMaster] (nolock)
where Fk_AddressId=@Fk_AddressId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_DocumentMasterByDocAndAddress]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_DocumentMasterByDocAndAddress]

@Fk_AddressId bigint,
 @DocId int      
AS
begin

select * from  [Tbl_DocumentMaster]
where [Fk_AddressId]=@Fk_AddressId and Fk_DocId=@DocId
          
         
      
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_DocumentMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_DocumentMaster]

@Fk_AddressId bigint
       
AS
begin

select * from  [Tbl_DocumentMaster] (nolock)
where [Fk_AddressId]=@Fk_AddressId
         
      
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_DocumentLog]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_DocumentLog]
@Fk_DocumentId bigint

      
           AS
begin

select * from [Tbl_DocumentLog] (nolock)
 where [Fk_DocumentId]=@Fk_DocumentId
        
           
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_DocMasterById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_DocMasterById]
@DocId int
  AS
begin

select * from  [Tbl_DocMaster] (nolock)
 where Pk_DocId=@DocId
         
           
        
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Tbl_DocMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Tbl_DocMaster]

           AS
begin

select * from  [Tbl_DocMaster]  (nolock)
         
           
        
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_RowMaterialMaster_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_RowMaterialMaster_ById]

@Pk_RowMaterialId bigint
As
begin

Select * From [Tbl_RowMaterialMaster]  (nolock)
where Pk_RowMaterialId=@Pk_RowMaterialId and Status=1

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_ProductRegisterMasterById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_ProductRegisterMasterById]
@Pk_ProductRegisterId bigint
AS


begin

select * from [Tbl_ProductRegisterMaster]  (nolock)
where Pk_ProductRegisterId=@Pk_ProductRegisterId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_Master_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_Master_ById]
@Pk_PartyId bigint

AS
Begin

select * from [dbo].[Party_Master]  (nolock)
where Status=1 and Pk_PartyId=@Pk_PartyId



End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_Master_ByAddressId]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_Master_ByAddressId]  
  
@Address bigint  
  
AS  
Begin  
  
select * from [dbo].[Party_Master]  (nolock)  
where   Fk_AddressId=@Address  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_Detail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
Alter PROCEDURE [dbo].[SP_Select_Party_Detail]  
@addressId bigint  
AS  
Begin  
  
  
select * from Party_Master P (nolock)  
inner join Address_Master A  (nolock) on P.Fk_AddressId=A.Pk_AddressID  
left join Party_Credit C (nolock) on C.Fk_AddressID=P.Fk_AddressID  
left join Party_Debit D (nolock) on D.Fk_AddressID=P.Fk_AddressID  
left join [Tbl_ProjectInformationMaster] I (nolock) on I.Fk_AddressID=P.Fk_AddressID  
where A.Pk_AddressID=@addressId  
  
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_DebitDetailById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_DebitDetailById]  
@Fk_AddressID bigint  
            
  
AS  
Begin  
select * from [dbo].[Party_Debit_Detail]  
   where [Fk_AddressID]=@Fk_AddressID  
           
  
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_DebitDetail_ByPartyId]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_DebitDetail_ByPartyId]  
@Fk_AddressID bigint  
  
AS  
Begin  
select * from [dbo].[Party_Debit] (nolock) as Party_M 
right join Party_Debit_Detail (nolock)  as Party_D
on Party_M.Fk_AddressId=Party_D.Fk_AddressID  
where Party_D.Fk_AddressID  =@Fk_AddressID  
             
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_Debit_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_Debit_ById]

@Pk_Party_DebitId bigint

AS
Begin
select * From [dbo].[Party_Debit]

    where Pk_PartyDebitId=@Pk_Party_DebitId


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_CreditDetail_ByPartyId]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_CreditDetail_ByPartyId]  
@Fk_AddressID   bigint  
  
AS  
Begin  
select * from [dbo].[Party_Credit] (nolock)as   Party_CM
right join Party_CreditDetail (nolock)  as Party_CD
on Party_CM.Fk_AddressID=Party_CD.Fk_AddressID  
        where Party_CD.Fk_AddressID=@Fk_AddressID  
             
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_CreditDetail_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_CreditDetail_ById]  
@Fk_AddressID bigint  
  
AS  
Begin  
select * from [dbo].[Party_CreditDetail]  
        where [Fk_AddressID]=@Fk_AddressID 
             
  
End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_Party_Credit_ById]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_Party_Credit_ById]

           @Pk_PartyCreditId bigint
AS
Begin
select * from  [dbo].[Party_Credit] (nolock)
 
    where Pk_PartyCreditId=@Pk_PartyCreditId


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_OrderParty_Detail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
Alter PROCEDURE [dbo].[SP_Select_OrderParty_Detail]  
@addressId bigint  
AS  
Begin  
  
  
select * from Party_Master P (nolock)  
inner join Address_Master A (nolock) on P.Fk_AddressId=A.Pk_AddressID  
inner join Party_Debit D (nolock) on D.Fk_AddressId=P.Fk_AddressId  
inner join Party_Master PM (nolock) on PM.Fk_AddressId = A.Pk_AddressID   
inner join Tbl_InvoiceMaster I (nolock) on I.Fk_AddressId = A.Pk_AddressID    
where A.Pk_AddressID=@addressId  
  
  
  
  
End  
-- [SP_Select_OrderParty_Detail] 3
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_FollowUp_Daily]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_FollowUp_Daily]


           AS
begin

	SELECT  
		
		ProductModel,
		OfferTime,
		CourierBy,
		CommValue,
		Convert(varchar(10),F_Date,103) FollowUp,
		Followup,
		 Convert(varchar(10),N_F_FollowpDate,103) NextFollowUp,
		Status,
		ByWhom,
		EnqType,
		Remarks,fm.Fk_AddressID
	FROM dbo.Enq_FollowMaster AS FM (nolock) INNER JOIN dbo.Enq_FollowDetails AS FD  (nolock)
	ON FM.Fk_AddressID = FD.Fk_AddressID 
   where Convert(varchar(10),N_F_FollowpDate,103) = CONVERT(varchar(10),getDate(),103) 
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_RIInwardDetailByRIInward]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_RIInwardDetailByRIInward]
@Fk_ReInwardId as bigint
AS
begin

select i.Pk_RIInwardDetailId,i.Fk_ProductRegisterId,i.Fk_RowMaterialId,i.Fk_ReInwardId, ROW_NUMBER() over  (order by i.Pk_RIInwardDetailId) as 'SrNo',p.CategoryName,r.RowMaterialName,i.Unit,i.Quantity,i.Remarks 
from [Tbl_RIInwardDetail] i (nolock)
inner join Tbl_RowMaterialMaster r (nolock) on r.Pk_RowMaterialId=i.Fk_RowMaterialId
inner join Tbl_ProductRegisterMaster p (nolock) on p.Pk_ProductRegisterId=i.Fk_ProductRegisterId 
where Fk_ReInwardId=@Fk_ReInwardId       
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_RIInwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_RIInwardDetail]
AS
begin

select i.Pk_RIInwardDetailId,i.Fk_ProductRegisterId,i.Fk_RowMaterialId,i.Fk_ReInwardId, ROW_NUMBER() over  (order by i.Pk_RIInwardDetailId) as 'SrNo',p.CategoryName,r.RowMaterialName,i.Unit,i.Quantity,i.Remarks 
from [Tbl_RIInwardDetail] i (nolock)
inner join Tbl_RowMaterialMaster r (nolock) on r.Pk_RowMaterialId=i.Fk_RowMaterialId
inner join Tbl_ProductRegisterMaster p (nolock) on p.Pk_ProductRegisterId=i.Fk_ProductRegisterId  
           
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_ReInwardMasterByOutward]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_ReInwardMasterByOutward]
@Fk_OutwardId bigint
AS
begin

select RI.* from [Tbl_ReInwardMaster] RI (nolock)

where Fk_OutwardId=@Fk_OutwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_OutwardMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_OutwardMaster]
 
        
           AS
begin
select o.* from [Tbl_OutwardMaster] o (nolock)


     

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_OutwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_OutwardDetail]
@Fk_outwardid bigint
AS
begin
select o.Pk_OutwardDetailId	,o.Fk_ProductRegisterId,o.Fk_RowMaterialId,o.Fk_OutwardId, ROW_NUMBER() over  (order by o.Pk_OutwardDetailId) as 'SrNo',p.CategoryName,r.RowMaterialName,o.Unit,o.Quantity,o.Remarks from [Tbl_OutwardDetail] o (nolock)
inner join Tbl_RowMaterialMaster r (nolock) on r.Pk_RowMaterialId=o.Fk_RowMaterialId
inner join Tbl_ProductRegisterMaster p (nolock) on p.Pk_ProductRegisterId=o.Fk_ProductRegisterId  
where o.Fk_OutwardId=@Fk_outwardid
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_OrderPartyMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_OrderPartyMaster]

     @Fk_AddressId bigint

AS
begin

select * from [Tbl_OrderPartyMaster]
 where Fk_AddressId=@Fk_AddressId
           
           end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_OrderOneMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_OrderOneMaster]
@Fk_AddressId bigint
AS
Begin

select * from [Tbl_OrderOneMaster]
where [Fk_AddressId]=@Fk_AddressId
 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_OrderFollowupDetailByFollowUp]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_OrderFollowupDetailByFollowUp]
@Fk_AddressId bigint
        
AS
begin

select * from Tbl_OrderFollowupDetail
where Fk_AddressId=@Fk_AddressId
          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_OrderFollowupDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_OrderFollowupDetail]

        
AS
begin

select * from Tbl_OrderFollowupDetail

          
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_InwardStockMasterByOutward]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_InwardStockMasterByOutward]
@Fk_OutwardId bigint
AS
begin

select RI.* from [Tbl_ReInwardMaster] RI (nolock)

where Fk_OutwardId=@Fk_OutwardId
          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_InwardStockMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_InwardStockMaster]


       
           
           AS
begin

select i.* from [Tbl_InwardStockMaster] i (nolock)

          
         
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_InwardDetailByInward]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_InwardDetailByInward]
@InwardId as bigint
AS
begin

select i.Pk_InwardDetailId,i.Fk_ProductRegisterId,i.Fk_RowMaterialId,i.Fk_InwardId, ROW_NUMBER() over  (order by i.Pk_InwardDetailId) as 'SrNo',p.CategoryName,r.RowMaterialName,i.Unit,i.Quantity,i.Remarks from [Tbl_InwardDetail] i (nolock)
inner join Tbl_RowMaterialMaster r (nolock) on r.Pk_RowMaterialId=i.Fk_RowMaterialId
inner join Tbl_ProductRegisterMaster p (nolock) on p.Pk_ProductRegisterId=i.Fk_ProductRegisterId  
where Fk_InwardId=@InwardId       
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_InwardDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_InwardDetail]
AS
begin

select i.Pk_InwardDetailId,i.Fk_ProductRegisterId,i.Fk_RowMaterialId,i.Fk_InwardId, ROW_NUMBER() over  (order by i.Pk_InwardDetailId) as 'SrNo',p.CategoryName,r.RowMaterialName,i.Unit,i.Quantity,i.Remarks from [Tbl_InwardDetail] i (nolock)
inner join Tbl_RowMaterialMaster r (nolock) on r.Pk_RowMaterialId=i.Fk_RowMaterialId
inner join Tbl_ProductRegisterMaster p (nolock) on p.Pk_ProductRegisterId=i.Fk_ProductRegisterId  
           
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Tbl_InvoiceMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Tbl_InvoiceMaster]

 AS
begin
Select * from [Tbl_InvoiceMaster] (nolock)

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_RowMaterialMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_RowMaterialMaster]
@Categoty bigint
As
begin

Select ROW_NUMBER() OVER(ORDER BY r.Pk_RowMaterialId) as 'SrNo' , r.*,p.CategoryName From [Tbl_RowMaterialMaster] r (nolock)
inner join Tbl_ProductRegisterMaster p (nolock)
on p.Pk_ProductRegisterId=r.Fk_ProductRegisterId
where p.Status=1 and r.Fk_ProductRegisterId=@Categoty
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_RawMatrialByOutward]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_RawMatrialByOutward]
@Categoty bigint
AS


begin

select distinct rm.Pk_RowMaterialId,rm.RowMaterialName from Tbl_RowMaterialMaster rm (nolock)

inner join Tbl_ProductRegisterMaster p (nolock)
on p.Pk_ProductRegisterId=rm.Fk_ProductRegisterId
inner join Tbl_OutwardDetail od (nolock)
on rm.Pk_RowMaterialId=od.Fk_RowMaterialId
where p.Status=1 and rm.Fk_ProductRegisterId=@Categoty
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_RawMaterialByStockRegister]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_RawMaterialByStockRegister]
@Fk_ProductId bigint
AS


begin

select distinct rw.Pk_RowMaterialId,rw.RowMaterialName from Tbl_RowMaterialMaster rw (nolock)
inner join Tbl_Stock_ProductRegister sp
on sp.Fk_RawMaterialId=rw.Pk_RowMaterialId
where rw.Fk_ProductRegisterId=@Fk_ProductId
Order by rw.RowMaterialName
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_ProductRegisterMaster]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_ProductRegisterMaster]

AS


begin

select Pk_ProductRegisterId,ROW_NUMBER() OVER(ORDER BY Pk_ProductRegisterId asc) AS SRNO,CategoryName  from [Tbl_ProductRegisterMaster] (nolock)

end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_ProductRegisterByStockRegister]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_ProductRegisterByStockRegister]

AS


begin

select distinct pr.Pk_ProductRegisterId,pr.CategoryName from [Tbl_ProductRegisterMaster] pr (nolock)
inner join Tbl_Stock_ProductRegister sp
on sp.Fk_CategoryId=pr.Pk_ProductRegisterId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_ProductRegisterByOutward]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_ProductRegisterByOutward]

AS


begin

select distinct pr.Pk_ProductRegisterId,pr.CategoryName from [Tbl_ProductRegisterMaster] pr (nolock)
inner join Tbl_OutwardDetail od
on od.Fk_ProductRegisterId=pr.Pk_ProductRegisterId
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Party_Master]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Party_Master]

AS
Begin

select * from [dbo].[Party_Master] (nolock)
where Status=1

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Party_DebitDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Party_DebitDetail]

          

AS
Begin
select * from [dbo].[Party_Debit_Detail] (nolock)

         


End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Party_Debit]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Party_Debit]



AS
Begin
select * From [dbo].[Party_Debit] (nolock)

 

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Party_CreditDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Party_CreditDetail]

AS
Begin
select * from [dbo].[Party_CreditDetail] (nolock)
        --where [Fk_PartyCreditId]=@Fk_PartyCreditId
           

End
GO
/****** Object:  StoredProcedure [dbo].[SP_Select_All_Party_Credit]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_Select_All_Party_Credit]


AS
Begin
select * from  [dbo].[Party_Credit] (nolock)
 
  


End
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllVisitorReportDetailCountByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllVisitorReportDetailCountByUser]      
--exec [SP_SearchAllVisitorReportDetailCount] '''RK''','','','','20120506','20140506'    
 @user varchar(1000),    
 @criteria varchar(max),   
 @start datetime,      
@end datetime      
AS      
begin      
DECLARE @QRY VARCHAR(max);        
SET @QRY='';        
      

if (@criteria <> '')      
  BEGIN  
      
   SET @QRY='select vd.E_Type ''EnqType'',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd 
  on vm.FK_AddressID=vd.Fk_AddressID   
where am.EnqStatus=1 and am.EnqDate >='''+ Convert(varchar,@start,112) +''' and am.EnqDate<='''+ Convert(varchar,@end,112) +''''+ @criteria+' 
group by vd.E_Type';          

END    

   IF(@QRY='')    
  begin    
if(@user='All' or @user='')
begin  
      select vd.E_Type 'EnqType',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm      
 on am.Pk_AddressID=vm.FK_AddressID      
 inner join Enq_VisitorDetail vd      
 on vm.FK_AddressID=vd.Fk_AddressID     
where am.EnqStatus=1 and vd.V_Date >=Convert(varchar,@start,112) and vd.V_Date<=Convert(varchar,@end,112)    
     group by vd.E_Type
   end  
   else   
   begin  
       select vd.E_Type 'EnqType',COUNT(vd.E_Type) as Total from Address_Master am      
  inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >=Convert(varchar,@start,112) and vd.V_Date<=Convert(varchar,@end,112)    
and vd.FollowBy in (SELECT value FROM dbo.fn_Split(@user,','))
   group by vd.E_Type
  
   end  
   end    
  else    
  begin    
      
if(@User='All' or @User='')  
begin  
 exec (@QRY);      
 end  
 else  
 begin   
-- print @QRY;  
 set @QRY = @QRY  + ' and vd.FollowBy in (SELECT value FROM dbo.fn_Split('''+@User+''','',''))
   group by vd.E_Type';   
  --print @QRY;  
 exec (@QRY);  
end    
end  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllVisitorReportDetailCount]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllVisitorReportDetailCount]      
--exec [SP_SearchAllVisitorReportDetailCount] '''RK''','','','','20120506','20140506'    
 @user varchar(1000),      
 @salesexec  varchar(1000),      
 @status varchar(1000),      
 @enquiry varchar(1000),    
 @start datetime,      
@end datetime      
AS      
begin      
DECLARE @QRY VARCHAR(max);        
SET @QRY='';        
      
if (@user <> '')        
  BEGIN        
   SET @QRY='select vd.E_Type ''EnqType'',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm      
 on am.Pk_AddressID=vm.FK_AddressID      
 inner join Enq_VisitorDetail vd      
 on vm.FK_AddressID=vd.Fk_AddressID      
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and vd.FollowBy in (' + @user + ') group by vd.E_Type'              
END      
    
    
IF(@salesexec<>'')          
  BEGIN        
   IF(@QRY<>'')        
    BEGIN        
     SET @QRY=@QRY+ ' INTERSECT '        
    END        
   SET @QRY=@QRY+'select vd.E_Type ''EnqType'',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm      
 on am.Pk_AddressID=vm.FK_AddressID      
 inner join Enq_VisitorDetail vd      
 on vm.FK_AddressID=vd.Fk_AddressID      
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and  vd.SalesExc in (' + @salesexec +') group by vd.E_Type'              
  END        
IF(@status<>'')          
  BEGIN        
   IF(@QRY<>'')        
    BEGIN        
     SET @QRY=@QRY+ ' INTERSECT '      
    END        
   SET @QRY=@QRY+'select vd.E_Type ''EnqType'',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm      
 on am.Pk_AddressID=vm.FK_AddressID      
 inner join Enq_VisitorDetail vd      
 on vm.FK_AddressID=vd.Fk_AddressID      
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and  vd.V_Status in (' + @status + ') group by vd.E_Type'              
  END        
    
    
IF(@enquiry<>'')          
  BEGIN        
   IF(@QRY<>'')        
    BEGIN        
     SET @QRY=@QRY+ ' INTERSECT '      
    END        
   SET @QRY=@QRY+'select vd.E_Type ''EnqType'',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm      
 on am.Pk_AddressID=vm.FK_AddressID      
 inner join Enq_VisitorDetail vd      
 on vm.FK_AddressID=vd.Fk_AddressID      
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and  vd.E_Type in (' + @enquiry + ') group by vd.E_Type'       
  END        
    
IF(@QRY='')      
  begin      
     select vd.E_Type 'EnqType',COUNT(vd.E_Type) as Total from Address_Master am      
 inner join Enq_VisitorMaster vm      
 on am.Pk_AddressID=vm.FK_AddressID      
 inner join Enq_VisitorDetail vd      
 on vm.FK_AddressID=vd.Fk_AddressID      
where am.EnqStatus=1 and vd.V_Date >=Convert(varchar,@start,112) and vd.V_Date<=Convert(varchar,@end,112) 
group by vd.E_Type
      
   end      
  else      
  begin      
     
 EXEC (@QRY);        
end      
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllVisitorReportDetailByUser]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllVisitorReportDetailByUser]    
--exec [SP_SearchAllVisitorReportDetail] '''RK''','','','','20120506','20140506'  
 @user varchar(1000),    
 @criteria varchar(max),
 @start datetime,    
@end datetime    
AS    
begin    
DECLARE @QRY VARCHAR(max);      
SET @QRY='';      
    
if (@criteria <> '')      
  BEGIN  
      
   SET @QRY='select DISTINCT vd.VFNo,am.EnqNo,am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.SalesExc ''SalesExecutive'',vd.V_Status ''Status'',vd.FollowBy ''FollowBy'',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd  
    on vm.FK_AddressID=vd.Fk_AddressID
where am.EnqStatus=1 and am.EnqDate >='''+ Convert(varchar,@start,112) +''' and am.EnqDate<='''+ Convert(varchar,@end,112) +''''+ @criteria;          

END    

   IF(@QRY='')    
  begin    
if(@user='All' or @user='')
begin  
      select DISTINCT vd.VFNo,am.EnqNo,am.Name,am.Area 'Station',vd.V_Date 'Date',vd.E_Type 'EnqType',vd.SalesExc 'SalesExecutive',vd.V_Status 'Status',vd.FollowBy 'FollowBy',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >=Convert(varchar,@start,112) and vd.V_Date<=Convert(varchar,@end,112)    
    
   end  
   else   
   begin  
     select DISTINCT vd.VFNo,am.EnqNo, am.Name,am.Area 'Station',vd.V_Date 'Date',vd.E_Type 'EnqType',vd.SalesExc 'SalesExecutive',vd.V_Status 'Status',vd.FollowBy 'FollowBy',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >=Convert(varchar,@start,112) and vd.V_Date<=Convert(varchar,@end,112)    
  and vd.FollowBy in (SELECT value FROM dbo.fn_Split(@user,','))
  
   end  
   end    
  else    
  begin    
      
if(@User='All' or @User='')  
begin  
 exec (@QRY);      
 end  
 else  
 begin   
-- print @QRY;  
 set @QRY = @QRY + ' and vd.FollowBy in (SELECT value FROM dbo.fn_Split('''+@User+''','',''))';      
  --print @QRY;  
 exec (@QRY);  
end    
end  
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchAllVisitorReportDetail]    Script Date: 12/20/2013 14:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter PROCEDURE [dbo].[SP_SearchAllVisitorReportDetail]    
--exec [SP_SearchAllVisitorReportDetail] '''RK''','','','','20120506','20140506'  
 @user varchar(1000),    
 @salesexec  varchar(1000),    
 @status varchar(1000),    
 @enquiry varchar(1000),  
 @start datetime,    
@end datetime    
AS    
begin    
DECLARE @QRY VARCHAR(max);      
SET @QRY='';      
    
if (@user <> '')      
  BEGIN      
   SET @QRY='select DISTINCT am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.SalesExc ''SalesExecutive'',vd.V_Status ''Status'',vd.FollowBy ''FollowBy'',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and vd.FollowBy in (' + @user + ')'     
END    
  
  
IF(@salesexec<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' INTERSECT '      
    END      
   SET @QRY=@QRY+'select DISTINCT am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.SalesExc ''SalesExecutive'',vd.V_Status ''Status'',vd.FollowBy ''FollowBy'',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and  vd.SalesExc in (' + @salesexec +')'     
  END      
IF(@status<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' INTERSECT '    
    END      
   SET @QRY=@QRY+'select DISTINCT am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.SalesExc ''SalesExecutive'',vd.V_Status ''Status'',vd.FollowBy ''FollowBy'',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and  vd.V_Status in (' + @status + ')'     
  END      
  
  
IF(@enquiry<>'')        
  BEGIN      
   IF(@QRY<>'')      
    BEGIN      
     SET @QRY=@QRY+ ' INTERSECT '    
    END      
   SET @QRY=@QRY+'select DISTINCT am.Name,am.Area ''Station'',vd.V_Date ''Date'',vd.E_Type ''EnqType'',vd.SalesExc ''SalesExecutive'',vd.V_Status ''Status'',vd.FollowBy ''FollowBy'',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >='''+ Convert(varchar,@start,112) +''' and vd.V_Date<='''+ Convert(varchar,@end,112)+''' and  vd.E_Type in (' + @enquiry + ')'     
  END      
  
IF(@QRY='')    
  begin    
     select DISTINCT am.Name,am.Area 'Station',vd.V_Date 'Date',vd.E_Type 'EnqType',vd.SalesExc 'SalesExecutive',vd.V_Status 'Status',vd.FollowBy 'FollowBy',vd.Remarks,am.Pk_AddressID from Address_Master am    
 inner join Enq_VisitorMaster vm    
 on am.Pk_AddressID=vm.FK_AddressID    
 inner join Enq_VisitorDetail vd    
 on vm.FK_AddressID=vd.Fk_AddressID    
where am.EnqStatus=1 and vd.V_Date >=Convert(varchar,@start,112) and vd.V_Date<=Convert(varchar,@end,112)    
     EXEC (@QRY);      
   end    
  else    
  begin    
      
      
 EXEC (@QRY);      
end    
end
GO
