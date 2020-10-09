CREATE VIEW [dbo].[viewProjectTree]
AS
WITH [CollectionHierarchy] AS
(
	SELECT 0 AS [Type],
	       [Name],
		   [ProjectCollectionId],
	       NULL AS [ProjectId],
		   NULL AS [ProjectVersionId],
		   NULL AS [ParentProjectId],
		   NULL AS [ParentProjectVersionId]
	FROM ProjectCollection
)
,[ProjectHierarchy] AS
(
	SELECT 1 AS [Type],
		   x.[Name],
		   x.[ProjectCollectionId],
	       x.[ProjectId],
		   NULL AS [ProjectVersionId],
		   x.[ParentProjectId],
		   NULL AS [ParentProjectVersionId]
    FROM [dbo].[Project] AS x
	WHERE [ParentProjectId] IS NULL
	UNION ALL
	SELECT 1 AS [Type],
		   y.[Name],
		   y.[ProjectCollectionId],
	       y.[ProjectId],
		   NULL AS [ProjectVersionId],
		   y.[ParentProjectId],
		   NULL AS [ParentProjectVersionId]
    FROM [dbo].[Project] y
		INNER JOIN [ProjectHierarchy] c ON y.[ParentProjectId] = c.[ProjectId]
),
[ProjectVersionHierarchy] AS
(
	SELECT 2 AS [Type],
		   z.[Name],
		   p.[ProjectCollectionId] AS [ProjectCollectionId],
	       z.[ProjectId],
	       z.[ProjectVersionId],
		   NULL AS [ParentProjectId],
		   z.[ParentProjectVersionId]
	FROM [dbo].[ProjectVersion] z
		INNER JOIN [Project] p ON z.[ProjectId] = p.[ProjectId]
	WHERE z.[ParentProjectVersionId] IS NULL
	UNION ALL
	SELECT 2 AS [Type],
		   a.[Name],
		   p.[ProjectCollectionId] AS [ProjectCollectionId],
	       a.[ProjectId],
	       a.[ProjectVersionId],
		   NULL AS [ParentProjectId],
		   a.[ParentProjectVersionId]
	FROM [dbo].[ProjectVersion] a
		INNER JOIN [Project] p ON a.[ProjectId] = p.[ProjectId]
		INNER JOIN [ProjectVersionHierarchy] d ON a.[ParentProjectVersionId] = d.[ProjectVersionId]
)
SELECT * FROM [CollectionHierarchy]
UNION ALL
SELECT * FROM [ProjectHierarchy]
UNION ALL
SELECT * FROM [ProjectVersionHierarchy]