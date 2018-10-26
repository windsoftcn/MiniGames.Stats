using System;

namespace MiniGames.Stats.Entities
{
    public class BaseEntity : BaseEntity<int>
    {

    }

    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}