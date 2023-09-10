# BlogPostManagement

Deployed here: http://blog-post-management.polandcentral.cloudapp.azure.com/swagger/index.html

Architecture details: https://github.com/Alexander-Shein/EmpCore

There're endpoints:

FYI: Put version `1` everywhere when you test

- `GET /v{version}/blog-posts` - searches for blog posts. It returns a paged result.
- `POST /v{version}/blog-posts` - creates a new draft blog post
- `GET /v{version}/blog-posts/{blogPostId}` - gets a blog post by its id
- `PATCH /v{version}/blog-posts/{blogPostId}` - partial update for a blog post. I ussualy use PATCH instead of PUT for update. Because partial update covers more cases than full replace
- `DELETE /v{version}/blog-posts/{blogPostId}` - deletes a blog post by id. This endpoint sends DeletedBlogPostEvent. The CommentManagement service is subscribed to this event. Soft delete is used (IsDeleted flag)
- `PUT /v{version}/published-blog-posts/{blogPostId}` - it updates a blog post status from draft to published. It sends an integration event 'BlogPostPublishedEvent'. The Commentmanagement service is subscribed to this event and it enables comments once a blog post is published.

All the entities and Aggregate Roots are incapsulated and they are always in the correct state. Because of incapsulation it is not possible to create an entity or change it to an invalid state.

All the business rules are in the Domain project.

Persistence project uses EntityFrameworkCore, Migrations, Repository patterns to load/update Aggregate Roots.

For QueryStack fast dapper is used.

- It sends 2 events via Azure Service Bus: BlogPostPublishedEvent and BlogPostDeletedEvent. CommentManagementService is subscribed to these evetns.

DB Schema
``` SQL
CREATE TABLE [dbo].[BlogPost]
(
  	[Id]			UNIQUEIDENTIFIER NOT NULL,
  	[AuthorId]		VARCHAR(128) NOT NULL,
  	[FeedbackEmailAddress]	NVARCHAR(256) NOT NULL 		CONSTRAINT [DF_BlogPost_FeedbackEmailAddress] DEFAULT '',
  	[Title]			NVARCHAR(1024) NOT NULL		CONSTRAINT [DF_BlogPost_Title] DEFAULT '',
  	[Content]		NVARCHAR(MAX) NOT NULL		CONSTRAINT [DF_BlogPost_Content] DEFAULT '',
  	[PublishStatus]		NVARCHAR(24) NOT NULL		CONSTRAINT [DF_BlogPost_PublishStatus] DEFAULT '',
  	[PublishDateTime]	DATETIME2 NULL,
  	[IsDeleted]		BIT NOT NULL			CONSTRAINT [DF_BlogPost_IsDeleted] DEFAULT 0,
  	[CreatedAt]		DATETIME2 NOT NULL		CONSTRAINT [DF_BlogPost_CreatedAt] DEFAULT GETDATE(),
  	[UpdatedAt]		DATETIME2 NOT NULL		CONSTRAINT [DF_BlogPost_UpdatedAt] DEFAULT GETDATE(),

	CONSTRAINT [PK_BlogPost_Id] PRIMARY KEY (Id)
);

CREATE TABLE [dbo].[EmbeddedResource]
(
	[Id]		INT IDENTITY(1,1) NOT NULL,
	[BlogPostId]	UNIQUEIDENTIFIER NOT NULL,
	[Url]		NVARCHAR(2048) NOT NULL		CONSTRAINT [DF_EmbeddedResource_Url] DEFAULT '',
	[Caption]	NVARCHAR(MAX) NOT NULL		CONSTRAINT [DF_EmbeddedResource_Caption] DEFAULT '',

	CONSTRAINT [PK_EmbeddedResource_Id] PRIMARY KEY (Id),
	CONSTRAINT [FK_EmbeddedResource_BlogPostId_BlogPost_Id] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost]([Id])
);
```
