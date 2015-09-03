﻿using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveMvvm.Models
{
    public class ModelSwitch<TModel, TId> : IObserver<IObservable<TModel>>
        where TModel : Model<TId>
        where TId : IEquatable<TId>
    {
        public ModelSwitch(TId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Stream = Stream<TModel, TId>.Get(id);
        }

        public Stream<TModel, TId> Stream { get; }

        public TId Id => Stream.Id;

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IObserver<IObservable<TModel>>.OnCompleted()
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IObserver<IObservable<TModel>>.OnError(Exception error)
        {
            throw new NotSupportedException();
        }

        public async void OnNext(IObservable<TModel> observable)
        {
            if (observable == null)
            {
                throw new ArgumentNullException(nameof(observable));
            }

            Stream.OnNext(await observable);
        }

        public async void OnNext(Task<TModel> task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            Stream.OnNext(await task);
        }

        public void OnNext(TModel value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Stream.OnNext(value);
        }
    }
}