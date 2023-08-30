# BlogPostManagement

Deployed here: http://blog-post-management.polandcentral.cloudapp.azure.com/swagger/index.html

- I ussualy use PATCH /blog-posts/{blogPostId} instead of PUT /blog-posts/{blogPostId}. Because partial update covers more cases than full replace

- It sends 2 events via Azure Service Bus: BlogPostPublishedEvent and BlogPostDeletedEvent. CommentManagementService is subscribed to these evetns.
