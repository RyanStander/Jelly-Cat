using UnityEngine;

/// <summary>
/// Collection of reusable methods that can be modified across projects.
/// </summary>
public static class ExtensionMethods
{
    #region Collider Helper Methods
    /// <summary>
    /// Checks if a given mesh is convex.
    /// </summary>
    /// <typeparam name="Collider"> Generic class type for all colliders. </typeparam>
    /// <param name="givenCollider"> The collider being checked. </param>
    /// <returns> Returns true if the mesh is convex. </returns>
    public static bool IsConvexMesh<Collider>(this Collider givenCollider)
    {
        if (givenCollider is MeshCollider meshCollider)
        {
            return meshCollider.convex;
        }
        return false;
    }
    #endregion
}
