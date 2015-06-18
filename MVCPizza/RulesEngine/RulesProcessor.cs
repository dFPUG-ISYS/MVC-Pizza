using System;
using System.Collections.Generic;

namespace MVCPizza.RulesEngine
{
    public class RulesProcessor
    {
        private List<String> _ValidationResults;
        public List<String> ValidationResults
        {
            get 
            {
                if (_ValidationResults == null)
                    _ValidationResults = new List<String>();
                return _ValidationResults; 
            }
        }
    }
    public static class RulesProcessorExtensions
    {
        // bei der Erstellung der Regeln ist zu beachten: 
        // die Fehlermeldung wird dann produziert,
        // wenn der Regelfall NICHT eintritt
        public static RulesProcessor EmptyRule(this RulesProcessor rp, string value, string msg)
        {
            if (!String.IsNullOrWhiteSpace(value))
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor NotEmptyRule(this RulesProcessor rp, string value, string msg)
        {
            if (String.IsNullOrWhiteSpace(value))
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor NotContainRule(this RulesProcessor rp, string value, string stringpart, string msg)
        {
            if (!String.IsNullOrWhiteSpace(value) && value.Contains(stringpart))
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor ContainRule(this RulesProcessor rp, string value, string stringpart, string msg)
        {
            if (String.IsNullOrWhiteSpace(value) || !value.Contains(stringpart))
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor LessThanRule(this RulesProcessor rp, decimal? value, decimal compare, string msg)
        {
            if ((value ?? 0) >= compare)
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor NotLessThanRule(this RulesProcessor rp, decimal? value, decimal compare, string msg)
        {
            if ((value ?? 0) < compare)
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor GreaterThanRule(this RulesProcessor rp, decimal? value, decimal compare, string msg)
        {
            if ((value ?? 0) <= compare)
                rp.ValidationResults.Add(msg);
            return rp;
        }
        public static RulesProcessor NotGreaterThanRule(this RulesProcessor rp, decimal? value, decimal compare, string msg)
        {
            if ((value ?? 0) > compare)
                rp.ValidationResults.Add(msg);
            return rp;
        }
    }
}