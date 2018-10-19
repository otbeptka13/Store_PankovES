SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE view [lk].[UsersView]
as
SELECT	u.id
      , u.email
      , u.emailConfirmed
      , u.phoneNumber
      , u.dateRegistration
      , u.generateToken
      , u.dateConfirmation
	  , u.name
	  , r.name [role]
from lk.Users u 
left join lk.UserRoles ur on ur.userId = u.id
left join lk.Roles r on r.id = ur.roleId

GO
