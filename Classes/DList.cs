using System.Collections;
using System.Collections.Generic;

namespace elemental.Classes{
    public class DList<T> : IEnumerable<T>{
        List<T> data = new List<T>(){};
        public T def;
        public int Count => data.Count;
        public T this[int index]{
            get{
                return index<Count?data[index]:def;
            }
        }
        public DList(){def = default(T);}
        public DList(T d){def = d;}
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)data).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)data).GetEnumerator();
        }
        public void Add(T val){
            data.Add(val);
        }
        public void Remove(T val){
            data.Remove(val);
        }
        public void RemoveAt(int val){
            data.RemoveAt(val);
        }
        public static implicit operator List<T>(DList<T> t){
            return t.data;
        }
    }
    public class NList<T> : IEnumerable<TrueNullable<T>>{
        List<TrueNullable<T>> data = new List<TrueNullable<T>>(){};
        public TrueNullable<T> def;
        public int Count => data.Count;
        public TrueNullable<T> this[int index]{
            get{
                if(index<Count)return data[index];
                for(; Count <= index; Add(def)){}
                return def;
            }
            set{
                for(; Count <= index; Add(def)){}
                data[index] = value;
            }
        }
        public NList(){def = null;}
        public NList(T d){def = d;}
        public IEnumerator<TrueNullable<T>> GetEnumerator()
        {
            return ((IEnumerable<TrueNullable<T>>)data).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TrueNullable<T>>)data).GetEnumerator();
        }
        public void Add(T val){
            data.Add(val);
        }
        public void Remove(TrueNullable<T> val){
            data.Remove(val);
        }
        public void RemoveAt(int val){
            data.RemoveAt(val);
        }
    }
    public class TrueNullable<T> {
        public T value;
        public TrueNullable(T value){
            this.value=value;
        }
        public static implicit operator T(TrueNullable<T> input){
            return input.value;
        }
        public static implicit operator TrueNullable<T>(T input){
            return new TrueNullable<T>(input);
        }
        public override string ToString(){
            return value.ToString()+"?";
        }
    }
}