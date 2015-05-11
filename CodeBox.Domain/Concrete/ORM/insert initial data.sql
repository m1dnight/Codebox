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
		   ,'c18f3e0599590d1f028ac69563d25c03f83f3a4981afab4a040a0137c4f9fb78'
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


