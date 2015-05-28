INSERT INTO [CodeBox].[dbo].[Roles]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('Administrators'
		   ,'Administrators can manage users, roles and languages')
		   INSERT INTO [CodeBox].[dbo].[Roles]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('Users'
		   ,'Regular users with creation rights')
		   INSERT INTO [CodeBox].[dbo].[Users]
		   ([Name]
		   ,[Surname]
		   ,[Username]
		   ,[Password]
		   ,[LastSeen]
		   ,[Mail]
		   ,[passwordQuestion]
		   ,[Comment]
		   ,[Approved]
		   ,[LockedOut]
		   ,[CreationDate]
		   ,[lastLoginDate]
		   ,[LastPasswordChangeDate]
		   ,[LastLockOutDate]
		   ,[ProviderName]
		   ,[LastActivityDate])
	 VALUES
		   ('Christophe'
		   ,'De Troyer'
		   ,'administrator'
		   ,'c1d639ca74ccb43c845879499c30d444582519b1d0c010e35722c46db96d95b3'
		   ,null
		   ,'christophe.dt@skynet.be'
		   ,null
		   ,null
		   ,1
		   ,0
		   ,'2003-04-09'
		   ,'2003-04-09'
		   ,'2003-04-09'
		   ,'2003-04-09'
		   ,'query'
		   ,'2003-04-09')
		   INSERT INTO [CodeBox].[dbo].[UserRole]
		   ([Users_UserId]
		   ,[Roles_RoleId])
	 VALUES
		   (1
		   ,1)
		   INSERT INTO [CodeBox].[dbo].[Users]
		   ([Name]
		   ,[Surname]
		   ,[Username]
		   ,[Password]
		   ,[LastSeen]
		   ,[Mail]
		   ,[passwordQuestion]
		   ,[Comment]
		   ,[Approved]
		   ,[LockedOut]
		   ,[CreationDate]
		   ,[lastLoginDate]
		   ,[LastPasswordChangeDate]
		   ,[LastLockOutDate]
		   ,[ProviderName]
		   ,[LastActivityDate])
	 VALUES
		   ('random'
		   ,'person'
		   ,'randomperson007'
		   ,'e517bc0d87b72f38ebcd3b087de6d447acdaa89b542041fd4d72bd14f5223eda'
		   ,null
		   ,'random@person.com'
		   ,null
		   ,null
		   ,1
		   ,0
		   ,'2015-05-15'
		   ,'2015-05-15'
		   ,'2015-05-15'
		   ,'2015-05-15'
		   ,'CustomMembershipProvider'
		   ,'2015-05-15')
		   INSERT INTO [CodeBox].[dbo].[UserRole]
		   ([Users_UserId]
		   ,[Roles_RoleId])
	 VALUES
		   (2
		   ,2)
		   INSERT INTO [CodeBox].[dbo].[Users]
		   ([Name]
		   ,[Surname]
		   ,[Username]
		   ,[Password]
		   ,[LastSeen]
		   ,[Mail]
		   ,[passwordQuestion]
		   ,[Comment]
		   ,[Approved]
		   ,[LockedOut]
		   ,[CreationDate]
		   ,[lastLoginDate]
		   ,[LastPasswordChangeDate]
		   ,[LastLockOutDate]
		   ,[ProviderName]
		   ,[LastActivityDate])
	 VALUES
		   ('random2'
		   ,'person2'
		   ,'randomperson008'
		   ,'e517bc0d87b72f38ebcd3b087de6d447acdaa89b542041fd4d72bd14f5223eda'
		   ,null
		   ,'random2@person.com'
		   ,null
		   ,null
		   ,1
		   ,0
		   ,'2015-05-16'
		   ,'2015-05-16'
		   ,'2015-05-16'
		   ,'2015-05-16'
		   ,'CustomMembershipProvider'
		   ,'2015-05-16')
		   INSERT INTO [CodeBox].[dbo].[UserRole]
		   ([Users_UserId]
		   ,[Roles_RoleId])
	 VALUES
		   (3
		   ,2)
		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('None'
		   ,'No language selected')

		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('C#'
		   ,'C sharp')

		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('C'
		   ,'Objective C')

		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('Java'
		   ,'Good Old Java')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('Python'
		   ,'Python')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('Bash'
		   ,'Bash')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('SQL'
		   ,'Structured Query Language')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('HTML'
		   ,'HTML')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('XML'
		   ,'XML')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('CSS'
		   ,'Cascade Style Sheets')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('JavaScript'
		   ,'JavaScript')
		   		   		   INSERT INTO [CodeBox].[dbo].[Languages]
		   ([Name]
		   ,[Description])
	 VALUES
		   ('MakeFiles'
		   ,'Makefiles')


GO


