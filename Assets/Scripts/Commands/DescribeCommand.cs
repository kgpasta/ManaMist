using System;
using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Commands
{
    public class DescribeCommand : Command
    {
        public bool describeAll { get; set; } = false;
        public string id { get; set; }

        public Coordinate coordinate { get; set; }

        public DescribeCommand(int playerId, Coordinate coordinate) : base(playerId, CommandType.DESCRIBE)
        {
            this.coordinate = coordinate;
        }

        public DescribeCommand(int playerId, string id) : base(playerId, CommandType.DESCRIBE)
        {
            this.id = id;
        }

        public DescribeCommand(int playerId) : base(playerId, CommandType.DESCRIBE)
        {
            this.describeAll = true;
        }

        public bool Execute(MapController mapController, Entity entity)
        {
            if (coordinate != null)
            {
                MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);

                Console.WriteLine(mapTile.terrain.ToString());
                if (!mapTile.resource.Equals(Resource.NONE))
                {
                    Console.WriteLine(mapTile.resource.ToString());
                }

                foreach (Entity e in mapTile.entities)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (entity != null)
            {
                Coordinate coordinate = mapController.GetPositionOfEntity(id);
                if (coordinate != null)
                {
                    Console.WriteLine(coordinate.ToString());
                }
                Console.WriteLine(entity.ToString());
            }
            else
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            string value = describeAll ? "All" : id != null ? id : coordinate.ToString();
            return "Describing " + value;
        }
    }
}