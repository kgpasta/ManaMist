using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Utility
{
    [Serializable]
    public class Coordinate
    {
        [SerializeField]
        private int m_X;
        public int x { get { return m_X; } set { m_X = value; } }

        [SerializeField]
        private int m_Y;
        public int y { get { return m_Y; } set { m_Y = value; } }

        public Coordinate()
        {
            m_X = 0;
            m_Y = 0;
        }

        public Coordinate(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        public override bool Equals(object obj)
        {
            Coordinate coordinate = obj as Coordinate;

            return coordinate.x == m_X && coordinate.y == m_Y;
        }

        public override int GetHashCode()
        {
            return m_X ^ m_Y;
        }

        public override string ToString()
        {
            return string.Format("({0} , {1})", m_X, m_Y);
        }

        public int Distance(Coordinate coord)
        {
            return Math.Abs(m_X - coord.x) + Math.Abs(m_Y - coord.y);
        }

        public bool IsNextTo(Coordinate coord)
        {
            return Distance(coord) == 1;
        }

        public bool IsDiagonal(Coordinate coord)
        {
            return Math.Abs(m_X - coord.x) == 1 && Math.Abs(m_Y - coord.y) == 1;
        }

        public bool IsAdjacent(Coordinate coord)
        {
            return IsNextTo(coord) || IsDiagonal(coord);
        }

        public List<Coordinate> GetNeighbors()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            coordinates.Add(new Coordinate(m_X, m_Y + 1));
            coordinates.Add(new Coordinate(m_X - 1, m_Y));
            coordinates.Add(new Coordinate(m_X + 1, m_Y));
            coordinates.Add(new Coordinate(m_X, m_Y - 1));
            return coordinates;
        }
    }
}