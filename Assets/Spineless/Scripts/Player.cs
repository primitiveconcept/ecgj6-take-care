namespace Spineless
{
    using System.Collections.Generic;
    using UnityEngine;


    public class Player : MonoBehaviour
    {
        private static List<Player> players = new List<Player>();

        public static int Add(Player player)
        {
            if (players.Contains(player))
                return players.IndexOf(player);

            players.Add(player);
            return players.Count - 1;
        }


        public static Player Get(int playerIndex = 0)
        {
            if (players.Count > playerIndex)
                return players[playerIndex];

            return null;
        }


        public static Player GetClosest(Transform transform)
        {
            if (players.Count < 1)
                return null;

            if (players.Count == 1)
                return players[0];

            Player nearestPlayer = players[0];
            float nearestPlayerDistance = Vector2.Distance(transform.position, nearestPlayer.transform.position);
            for (int i = 1; i < players.Count; i++)
            {
                Player player = players[i];
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance >= nearestPlayerDistance)
                    continue;

                nearestPlayer = player;
                nearestPlayerDistance = distance;
            }

            return nearestPlayer;
        }


        public static int GetPlayerNumber(GameObject playerGameObject)
        {
            Player player = playerGameObject.GetComponent<Player>();

            return players.IndexOf(player);
        }


        public static void Remove(Player player)
        {
            if (players.Contains(player))
                players.Remove(player);
        }


        public void Awake()
        {
            Add(this);
        }
    }
}