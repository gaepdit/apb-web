USE [airbranch];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER TRIGGER dbo.UpdateAirFacilityTable
    ON dbo.APBFACILITYINFORMATION
    AFTER UPDATE AS

/*******************************************************************************

Author:     Doug Waldron
Overview:   Updates air.Facilities with changes from dbo.APBFACILITYINFORMATION

Tables written to:
  air.Facilities

Tables accessed:
  dbo.APBFACILITYINFORMATION
  dbo.EPDUSERPROFILES

Modification History:
When        Who                 What
----------  ------------------  ------------------------------------------------
WIP         DWaldron            Initial version

*******************************************************************************/

BEGIN
    SET NOCOUNT ON;

    IF UPDATE(STRFACILITYNAME)
        OR UPDATE(STRFACILITYSTREET1)
        OR UPDATE(STRFACILITYSTREET2)
        OR UPDATE(STRFACILITYCITY)
        OR UPDATE(STRFACILITYSTATE)
        OR UPDATE(STRFACILITYZIPCODE)

        update a
        set a.Name                       = i.STRFACILITYNAME,
            a.FacilityAddress_Street     = i.STRFACILITYSTREET1,
            a.FacilityAddress_Street2    = i.STRFACILITYSTREET2,
            a.FacilityAddress_City       = i.STRFACILITYCITY,
            a.FacilityAddress_State      = i.STRFACILITYSTATE,
            a.FacilityAddress_PostalCode = i.STRFACILITYZIPCODE,
            a.UpdatedAt                  = i.DATMODIFINGDATE at time zone 'Eastern Standard Time',
            a.UpdatedBy                  = concat_ws(' | ', i.STRMODIFINGPERSON,
                                                     concat_ws(', ', u.STRLASTNAME, u.STRFIRSTNAME),
                                                     case
                                                         when i.STRMODIFINGLOCATION = 1
                                                             then 'Permitting Action'
                                                         when i.STRMODIFINGLOCATION = 2
                                                             then 'Facility Header Editor'
                                                         when i.STRMODIFINGLOCATION = 4
                                                             then 'IAIP Facility Creation Tool'
                                                     end)
        from air.Facilities a
            inner join inserted i
            on replace(a.Id, '-', '') = right(i.STRAIRSNUMBER, 8)
            left join dbo.EPDUSERPROFILES u
            on u.NUMUSERID = i.STRMODIFINGPERSON;

END;
go
