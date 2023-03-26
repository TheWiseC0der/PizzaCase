﻿namespace DesignPatterns.ModelMothers
{
    public interface IComposable
    {
        string Name { get;}
        double Price {get; set;}
        public double GetTotalPrice();
        void Display();

        string GetString();
        public void Add(IComposable child);
        public IComposable GetChild(int child);
        public void Remove(IComposable child);
    }
}