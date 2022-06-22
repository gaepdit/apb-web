/*******************************************************************************

Author:     Doug Waldron
Overview:   Initial facility migration

Tables written to:
  air.Facilities

Tables accessed:
  dbo.APBFACILITYINFORMATION
  dbo.APBHEADERDATA
  dbo.APBMASTERAIRS
  dbo.EPDUSERPROFILES

Modification History:
When        Who                 What
----------  ------------------  ------------------------------------------------
WIP         DWaldron            Initial version

*******************************************************************************/

-- insert into air.Facilities
select concat_ws('-', substring(f.STRAIRSNUMBER, 5, 3), right(f.STRAIRSNUMBER, 5)),
       f.STRFACILITYNAME,
       iif(h.STRPLANTDESCRIPTION is null, '', h.STRPLANTDESCRIPTION),
       f.STRFACILITYSTREET1,
       IIF(f.STRFACILITYSTREET2 = 'N/A', null, f.STRFACILITYSTREET2),
       f.STRFACILITYCITY,
       f.STRFACILITYSTATE,
       f.STRFACILITYZIPCODE,
       f.NUMFACILITYLATITUDE,
       f.NUMFACILITYLONGITUDE,
       a.DATMODIFINGDATE at time zone 'Eastern Standard Time',
       concat_ws(' | ', a.STRMODIFINGPERSON, concat_ws(', ', u_a.STRLASTNAME, u_a.STRFIRSTNAME)),
       f.DATMODIFINGDATE at time zone 'Eastern Standard Time',
       concat_ws(' | ', f.STRMODIFINGPERSON, concat_ws(', ', u_f.STRLASTNAME, u_f.STRFIRSTNAME),
                 case
                     when f.STRMODIFINGLOCATION = 1
                         then 'Permitting Action'
                     when f.STRMODIFINGLOCATION = 2
                         then 'Facility Header Editor'
                     when f.STRMODIFINGLOCATION = 4
                         then 'IAIP Facility Creation Tool'
                 end)
from dbo.APBFACILITYINFORMATION f
    inner join dbo.APBHEADERDATA h
    on h.STRAIRSNUMBER = f.STRAIRSNUMBER
    inner join dbo.APBMASTERAIRS a
    on a.STRAIRSNUMBER = f.STRAIRSNUMBER
    left join dbo.EPDUSERPROFILES u_a
    on u_a.NUMUSERID = a.STRMODIFINGPERSON
    left join dbo.EPDUSERPROFILES u_f
    on u_f.NUMUSERID = f.STRMODIFINGPERSON
where concat_ws('-', substring(f.STRAIRSNUMBER, 5, 3), right(f.STRAIRSNUMBER, 5))
          not in (select Id from air.Facilities);
