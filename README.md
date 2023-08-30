# BlogPostManagement

Deployed here: http://blog-post-management.polandcentral.cloudapp.azure.com/swagger/index.html

- I ussualy use PATCH /blog-posts/{blogPostId} instead of PUT /blog-posts/{blogPostId}. Because partial update covers more cases than full replace

- It sends 2 events via Azure Service Bus: BlogPostPublishedEvent and BlogPostDeletedEvent. CommentManagementService is subscribed to these evetns.

DB Schema
```
CREATE TABLE [dbo].[BlogPost]
(
  	[Id]					UNIQUEIDENTIFIER NOT NULL,
  	[AuthorId]				VARCHAR(128) NOT NULL,
  	[FeedbackEmailAddress]	NVARCHAR(256) NOT NULL 		CONSTRAINT [DF_BlogPost_FeedbackEmailAddress] DEFAULT '',
  	[Title]					NVARCHAR(1024) NOT NULL		CONSTRAINT [DF_BlogPost_Title] DEFAULT '',
  	[Content]				NVARCHAR(MAX) NOT NULL		CONSTRAINT [DF_BlogPost_Content] DEFAULT '',
  	[PublishStatus]			NVARCHAR(24) NOT NULL		CONSTRAINT [DF_BlogPost_PublishStatus] DEFAULT '',
  	[PublishDateTime]		DATETIME2 NULL,
  	[IsDeleted]				BIT NOT NULL				CONSTRAINT [DF_BlogPost_IsDeleted] DEFAULT 0,
  	[CreatedAt]				DATETIME2 NOT NULL			CONSTRAINT [DF_BlogPost_CreatedAt] DEFAULT GETDATE(),
  	[UpdatedAt]				DATETIME2 NOT NULL			CONSTRAINT [DF_BlogPost_UpdatedAt] DEFAULT GETDATE(),

	CONSTRAINT [PK_BlogPost_Id] PRIMARY KEY (Id)
);

CREATE TABLE [dbo].[EmbeddedResource]
(
	[Id]			INT IDENTITY(1,1) NOT NULL,
	[BlogPostId]	UNIQUEIDENTIFIER NOT NULL,
	[Url]			NVARCHAR(2048) NOT NULL		CONSTRAINT [DF_EmbeddedResource_Url] DEFAULT '',
	[Caption]		NVARCHAR(MAX) NOT NULL		CONSTRAINT [DF_EmbeddedResource_Caption] DEFAULT '',

	CONSTRAINT [PK_EmbeddedResource_Id] PRIMARY KEY (Id),
	CONSTRAINT [FK_EmbeddedResource_BlogPostId_BlogPost_Id] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost]([Id])
);
```
