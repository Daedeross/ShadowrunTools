namespace ShadowrunTools.Characters.Traits
{
    using ReactiveUI;
    using ShadowrunTools.Characters.Internal;
    using ShadowrunTools.Characters.Model;
    using System;

    public abstract class LeveledTraitWithRequirements : LeveledTrait, IHavePrerequisite, IHaveTaboo
    {
        private readonly IDslParser<ITrait> _parser;
        private readonly IScope<ITrait> _scope;

        public LeveledTraitWithRequirements(
            Guid id,
            int prototypeHash,
            string name,
            string category,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            IRules rules,
            IDslParser<ITrait> parser,
            LeveledTraitObservables inits = LeveledTraitObservables.All)
            : base(id, prototypeHash, name, category, container, root, rules, inits)
        {
            _parser = parser;

            _scope = new Scope<ITrait>(null, this, root);
        }

        public override TraitType TraitType => TraitType.None;

        public override bool Independant => throw new NotImplementedException();

        #region Requirements

        #region Needs

        private Func<bool> _needs = () => true;

        internal bool SafeNeeds()
        {
            try
            {
                return _needs();
            }
            catch (Exception)
            {
                return true;
            }
        }

        private string m_Needs;
        public string Needs
        {
            get => m_Needs;
            set
            {
                if (!string.Equals(m_Needs, value))
                {
                    m_Needs = value;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        _needs = () => true;
                    }

                    var parsed = _parser.ParseExpression<bool>(m_Needs, _scope);
                    if (parsed.HasValue)
                    {
                        _needs = parsed.Value.Scoped;
                    }
                    else
                    {
                        // TODO: show more/any information to user on parse failure.
                        _needs = () => true;
                    }

                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(Invalid));
                }
            }
        }

        #endregion

        #region Taboo
        
        private Func<bool> _taboo = () => false;

        internal bool SafeTaboo()
        {
            try
            {
                return _taboo();
            }
            catch (Exception)
            {
                return true;
            }
        }

        private string m_Taboo;
        public string Taboo
        {
            get => m_Taboo;
            set
            {
                if (!string.Equals(m_Taboo, value))
                {
                    m_Taboo = value;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        _taboo = () => false;
                        return;
                    }

                    var parsed = _parser.ParseExpression<bool>(m_Taboo, _scope);
                    if (parsed.HasValue)
                    {
                        _taboo = parsed.Value.Scoped;
                    }
                    else
                    {
                        _taboo = () => false;
                    }

                    this.RaisePropertyChanged(nameof(Invalid));
                }
            }
        }
        #endregion

        public virtual bool Invalid => SafeTaboo() && !SafeNeeds();

        public string InvalidMessage => string.Empty;

        #endregion
    }
}
