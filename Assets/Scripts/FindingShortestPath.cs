using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This class is responsible for finding the shortest path to next destination of hero.
/// </summary>
public class FindingShortestPath : MonoBehaviour
{


    private static Tilemap mountainTilemap;
    private static List<Vector3Int> validPositions;
    public float moveSpeed = 5f;

    private Vector3Int currentDestination;
    private List<Vector3Int> currentPath;
    private int currentPathIndex = 0;

    private bool isSelected = false;

    void Start()
    {
        mountainTilemap = GameObject.Find("Mountain").GetComponent<Tilemap>();
        validPositions = GetValidPositions(mountainTilemap);

        // Set the initial destination to the first valid position in the list
        currentDestination = mountainTilemap.WorldToCell(transform.position);
    }

    void Update()
    {
        // Check if the archer has reached the current destination
        if (transform.position == mountainTilemap.GetCellCenterWorld(currentDestination))
        {
            // If the archer has reached the current destination, set the next destination
            currentPathIndex = 0;

            // Check if the player is selected
            if (isSelected)
            {
                // Check if the mouse is clicked on a valid position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickedCellPosition = mountainTilemap.WorldToCell(mousePosition);
                if (validPositions.Contains(clickedCellPosition))
                {
                    // Set the new destination
                    currentDestination = clickedCellPosition;

                    // Find the shortest path to the new destination
                    currentPath = FindPath(transform.position, mountainTilemap.GetCellCenterWorld(currentDestination));
                }
            }
        }

        // Move the archer along the current path
        if (currentPath != null && currentPathIndex < currentPath.Count)
        {
            Vector3 nextPosition = mountainTilemap.GetCellCenterWorld(currentPath[currentPathIndex]);

            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == nextPosition)
            {
                currentPathIndex++;
            }
        }
        // Check if the mouse is clicked on the player
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Toggle the player selection
                isSelected = !isSelected;
            }
        }
    }
    //void Update()
    //{
    //    // check if click on player
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
    //        if (hit.collider != null && hit.collider.gameObject == gameObject)
    //        {
    //            selectedPlayer = this;
    //        }
    //        if (selectedPlayer == this)
    //        {
    //            // check if click on valid position
    //            Vector3Int cellPos = mountainTilemap.WorldToCell(mousePos);
    //            if (validPositions.Contains(cellPos))
    //            {
    //                destination = cellPos;
    //            }
    //        }
    //    }
    //}


    private List<Vector3Int> FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        // Create a new instance of the A* algorithm
        AStar<Vector3Int> aStar = new AStar<Vector3Int>();

        // Define the heuristic function (distance between two cells)
        aStar.HeuristicFunction = (a, b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

        // Define the cost function (distance between two adjacent cells)
        aStar.CostFunction = (a, b) => Vector3Int.Distance(a, b);

        // Define the start and end nodes
        Vector3Int startNode = mountainTilemap.WorldToCell(startPosition);
        Vector3Int endNode = mountainTilemap.WorldToCell(endPosition);

        // Find the shortest path using the A* algorithm
        List<Vector3Int> path = aStar.FindPath(startNode, endNode, validPositions);

        return path;
    }

    public class AStar<T>
    {
        // The heuristic function estimates the distance between two nodes
        public Func<T, T, float> HeuristicFunction { get; set; }

        // The cost function returns the cost to move from one node to an adjacent node
        public Func<T, T, float> CostFunction { get; set; }

        public List<T> FindPath(T start, T end, List<T> nodes)
        {
            // The set of nodes that have been visited
            HashSet<T> visited = new HashSet<T>();

            // The set of nodes that have been discovered but not yet visited
            HashSet<T> frontier = new HashSet<T>();

            // The dictionary of nodes and their parent nodes
            Dictionary<T, T> parents = new Dictionary<T, T>();

            // The dictionary of nodes and their path costs
            Dictionary<T, float> costs = new Dictionary<T, float>();

            // Add the start node to the frontier
            frontier.Add(start);

            // Set the path cost of the start node to zero
            costs[start] = 0f;

            // Continue searching until the frontier is empty
            while (frontier.Count > 0)
            {
                // Get the node in the frontier with the lowest cost
                T current = default(T);
                float lowestCost = Mathf.Infinity;
                foreach (T node in frontier)
                {
                    if (!costs.ContainsKey(node)) continue;
                    float cost = costs[node] + HeuristicFunction(node, end);
                    if (cost < lowestCost)
                    {
                        lowestCost = cost;
                        current = node;
                    }
                }

                // If the current node is the end node, the path has been found
                if (current.Equals(end))
                {
                    List<T> path = new List<T>();
                    while (!current.Equals(start))
                    {
                        path.Add(current);
                        current = parents[current];
                    }
                    path.Add(start);
                    path.Reverse();
                    return path;
                }

                // Remove the current node from the frontier
                frontier.Remove(current);

                // Add the current node to the visited set
                visited.Add(current);

                // Iterate over the adjacent nodes to the current node
                foreach (T adjacent in GetAdjacentNodes(current, nodes))
                {
                    // If the adjacent node has already been visited, skip it
                    if (visited.Contains(adjacent)) continue;

                    // Calculate the cost to move from the current node to the adjacent node
                    float costToAdjacent = CostFunction(current, adjacent);

                    // Calculate the total cost of the path to the adjacent node
                    float totalCost = costs[current] + costToAdjacent;

                    // If the adjacent node is not in the frontier, add it
                    if (!frontier.Contains(adjacent))
                    {
                        frontier.Add(adjacent);
                    }
                    // If the total cost to the adjacent node is greater than or equal to the
                    // current cost, skip it
                    else if (totalCost >= costs[adjacent])
                    {
                        continue;
                    }

                    // Update the parent of the adjacent node to the current node
                    parents[adjacent] = current;

                    // Update the cost of the path to the adjacent node
                    costs[adjacent] = totalCost;
                }
            }

            // If the frontier is empty and the end node has not been found, there is no path
            return null;
        }
        private IEnumerable<T> GetAdjacentNodes(T node, List<T> nodes)
        {
            // Define the offset vectors for the four adjacent nodes
            Vector2Int[] offsets = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

            // Convert the current node to a vector
            Vector2Int currentVector = new Vector2Int((int)(object)node.GetType().GetField("x").GetValue(node), (int)(object)node.GetType().GetField("y").GetValue(node));

            // Iterate over the adjacent nodes
            foreach (Vector2Int offset in offsets)
            {
                // Calculate the position of the adjacent node
                Vector2Int adjacentVector = currentVector + offset;

                // Convert the adjacent node vector to a generic type
                T adjacent = (T)(object)new Vector3Int(adjacentVector.x, adjacentVector.y, 0);

                // If the adjacent node is valid, add it to the list of adjacent nodes
                if (nodes.Contains(adjacent))
                {
                    yield return adjacent;
                }
            }
        }
    }




    private void MoveToDestination(Vector3Int destination, Tilemap mountainTilemap, List<Vector3Int> validPositions)
    {
        List<Vector3Int> path = FindShortestPath(transform.position, destination, mountainTilemap, validPositions);

        if (path != null)
        {
            StartCoroutine(MoveAlongPath(path));
        }
        else
        {
            Debug.LogError("No valid path found.");
        }
    }
    private IEnumerator MoveAlongPath(List<Vector3Int> path)
    {
        foreach (Vector3Int tilePosition in path)
        {
            Vector3 destination = mountainTilemap.GetCellCenterWorld(tilePosition);
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
    private List<Vector3Int> GetValidPositions(Tilemap tilemap)
    {
        List<Vector3Int> validPositions = new List<Vector3Int>();

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                validPositions.Add(position);
            }
        }

        return validPositions;
    }

    private List<Vector3Int> FindShortestPath(Vector3 currentPosition, Vector3Int destination, Tilemap tilemap, List<Vector3Int> validPositions)
    {
        List<Vector3Int> openList = new List<Vector3Int>();
        List<Vector3Int> closedList = new List<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> parentMap = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, int> gScore = new Dictionary<Vector3Int, int>();
        Dictionary<Vector3Int, int> fScore = new Dictionary<Vector3Int, int>();

        Vector3Int start = tilemap.WorldToCell(currentPosition);
        openList.Add(start);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, destination);

        while (openList.Count > 0)
        {
            Vector3Int current = GetLowestFScore(openList, fScore);
            if (current == destination)
            {
                return ReconstructPath(parentMap, current);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current, tilemap, validPositions))
            {
                if (closedList.Contains(neighbor))
                {
                    continue;
                }

                int tentativeGScore = gScore[current] + 1;
                if (!openList.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    parentMap[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, destination);

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private int Heuristic(Vector3Int position, Vector3Int destination)
    {
        return Mathf.Abs(position.x - destination.x) + Mathf.Abs(position.y - destination.y);
    }

    private List<Vector3Int> GetNeighbors(Vector3Int position, Tilemap tilemap, List<Vector3Int> validPositions)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        Vector3Int[] offsets = new Vector3Int[]
        {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left
        };

        foreach (Vector3Int offset in offsets)
        {
            Vector3Int neighborPosition = position + offset;
            if (validPositions.Contains(neighborPosition) && tilemap.HasTile(neighborPosition))
            {
                neighbors.Add(neighborPosition);
            }
        }

        return neighbors;
    }

    private Vector3Int GetLowestFScore(List<Vector3Int> list, Dictionary<Vector3Int, int> fScore)
    {
        int lowestScore = int.MaxValue;
        Vector3Int lowestPosition = list[0];

        foreach (Vector3Int position in list)
        {
            if (fScore.ContainsKey(position) && fScore[position] < lowestScore)
            {
                lowestScore = fScore[position];
                lowestPosition = position;
            }
        }

        return lowestPosition;
    }

    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> parentMap, Vector3Int position)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        while (parentMap.ContainsKey(position))
        {
            path.Add(position);
            position = parentMap[position];
        }

        // Add the starting point to the path
        path.Add(position);

        // Reverse the path to get it in the correct order
        path.Reverse();

        return path;
    }
}
