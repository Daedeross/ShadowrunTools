using ReactiveUI;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShadowrunTools.Characters.Tests
{
    public class OaphTests
    {
        public interface IOaphTrait : IReactiveObject
        {
            int IntProperty1 { get; set; }

            int IntProperty2 { get; set; }

            int IntProperty3 { get; set; }

            int DependentProperty1 { get; }

            int DependentProperty2 { get; }
        }

        public class OaphTrait : ReactiveObject, IOaphTrait
        {
            private int m_IntProperty1;
            public int IntProperty1
            {
                get => m_IntProperty1;
                set => this.RaiseAndSetIfChanged(ref m_IntProperty1, value);
            }

            private int m_IntProperty2;
            public int IntProperty2
            {
                get => m_IntProperty2;
                set => this.RaiseAndSetIfChanged(ref m_IntProperty2, value);
            }

            private int m_IntProperty3;
            public int IntProperty3
            {
                get => m_IntProperty3;
                set => this.RaiseAndSetIfChanged(ref m_IntProperty3, value);
            }

            private readonly ObservableAsPropertyHelper<int> m_DependentProperty1;
            public int DependentProperty1 => m_DependentProperty1.Value;

            private readonly ObservableAsPropertyHelper<int> m_DependentProperty2;
            public int DependentProperty2 => m_DependentProperty2.Value;

            public OaphTrait()
            {
                m_DependentProperty1 = this.WhenAnyValue(x => x.IntProperty1, y => y.IntProperty2, (xv, yv) => xv + yv)
                    .ToProperty(this, x => x.DependentProperty1);


                m_DependentProperty2 = this.WhenAnyValue(x => x.DependentProperty1, y => y.IntProperty3, (xv, yv) => xv - yv)
                    .ToProperty(this, x => x.DependentProperty2);
            }
        }

        public class OaphTraitViewModel : ReactiveObject, IOaphTrait
        {
            private readonly IOaphTrait _model;

            public int IntProperty1 { get => _model.IntProperty1; set => _model.IntProperty1 = value; }
            public int IntProperty2 { get => _model.IntProperty2; set => _model.IntProperty2 = value; }
            public int IntProperty3 { get => _model.IntProperty3; set => _model.IntProperty3 = value; }

            public int DependentProperty1 => _model.DependentProperty1;

            public int DependentProperty2 => _model.DependentProperty2;

            public OaphTraitViewModel(IOaphTrait model)
            {
                _model = model;

                _model.PropertyChanged += this.ModelChanged;
                _model.PropertyChanging += this.ModelChanging;
            }

            private void ModelChanging(object sender, PropertyChangingEventArgs e)
            {
                this.RaisePropertyChanging(e.PropertyName);
            }

            private void ModelChanged(object sender, PropertyChangedEventArgs e)
            {
                this.RaisePropertyChanged(e.PropertyName);
            }
        }

        [Fact]
        public void TestDependentChangeNotificationObservable()
        {
            /// Setup
            var subject = new OaphTrait
            {
                IntProperty1 = 1,
                IntProperty2 = 2,
                IntProperty3 = 1,
            };

            // Ensure dependent properties start correct
            Assert.Equal(3, subject.DependentProperty1);
            Assert.Equal(2, subject.DependentProperty2);

            List<IReactivePropertyChangedEventArgs<IReactiveObject>> changing = new ();
            List<IReactivePropertyChangedEventArgs<IReactiveObject>> changed = new ();

            using var s1 = subject.Changing.Subscribe(changing.Add);
            using var s2 = subject.Changed.Subscribe(changed.Add);

            /// Execute
            subject.IntProperty3 += 1;

            /// Assert

            // Ensure DPs have correct value
            Assert.Equal(3, subject.DependentProperty1);
            Assert.Equal(1, subject.DependentProperty2);

            // Assert observables were fired
            Assert.Contains(changing, args => Equals(nameof(IOaphTrait.DependentProperty2), args.PropertyName));
            Assert.DoesNotContain(changing, args => Equals(nameof(IOaphTrait.DependentProperty1), args.PropertyName));
        }


        [Fact]
        public void TestDependentChangeNotificationEvent()
        {
            /// Setup
            var subject = new OaphTrait
            {
                IntProperty1 = 1,
                IntProperty2 = 2,
                IntProperty3 = 1,
            };

            // Ensure dependent properties start correct
            Assert.Equal(3, subject.DependentProperty1);
            Assert.Equal(2, subject.DependentProperty2);

            List<PropertyChangingEventArgs> changing = new();
            List<PropertyChangedEventArgs> changed = new();

            subject.PropertyChanging += (sender, e) => changing.Add(e);
            subject.PropertyChanged += (sender, e) => changed.Add(e);

            /// Execute
            subject.IntProperty3 += 1;

            /// Assert

            // Ensure DPs have correct value
            Assert.Equal(3, subject.DependentProperty1);
            Assert.Equal(1, subject.DependentProperty2);

            // Assert observables were fired
            Assert.Contains(changing, args => Equals(nameof(IOaphTrait.DependentProperty2), args.PropertyName));
            Assert.DoesNotContain(changing, args => Equals(nameof(IOaphTrait.DependentProperty1), args.PropertyName));
        }
    }
}
