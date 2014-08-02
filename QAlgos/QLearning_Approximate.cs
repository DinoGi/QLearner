﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLearner.QStates;

namespace QLearner.QAlgos
{
    /*
     * Approximate Q-Learning is an attempt to solve the slow-learning and memory-heavy aspect of Q-Learning by generalizing states seen to a handful of features (or characteristics).  New states can then be estimated based on similarity in feature set.
     * This particular approach to approximation allows for estimation of the features that would result from an action.  It adds another layer of uncertainty, but in some cases, it is not possible to know the exact outcomes of ones actions.
     * */
    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = false)]
    public class QLearning_Approximate:QLearning
    {
        protected Dictionary<string, decimal> QWeights;
        private Dictionary<QStateActionPair, Dictionary<string, decimal>> featureCache;

        public override void Initialize()
        {
            QWeights = new Dictionary<string, decimal>();
            random = new Random();
            RenameLearningTable("#", "Feature", "Weight", "Last");
            featureCache = new Dictionary<QStateActionPair, Dictionary<string, decimal>>();
        }

        // Return the value of performing an action based on the features expected from the action.
        protected override decimal GetQValue(QStateActionPair p)
        {
            decimal qv = 0;

            Dictionary<string, decimal> features;
            //features = p.state.GetFeaturesEstimate(p.action);
            if (featureCache.ContainsKey(p)) features = featureCache[p];
            else
            {
                features = p.state.GetFeatures(p.action);
                featureCache[p] = features;
            }
            foreach (KeyValuePair<string, decimal> feature in features)
            {
                if (!QWeights.ContainsKey(feature.Key)) QWeights[feature.Key] = 0;
                qv += QWeights[feature.Key] * feature.Value;
            }

            return qv;
        }

        // Update the weights of each feature based on their contribution to the reward
        protected override void QUpdate(int n, QState currentState, string action, QState newState, decimal reward)
        {
            decimal maxQ = GetMaxValue(newState);
            QStateActionPair p = new QStateActionPair(currentState, action);
            Dictionary<string, decimal> features;
            if (featureCache.ContainsKey(p)) features = featureCache[p];
            else
            {
                features = currentState.GetFeatures(action);
                featureCache[p] = features;
            }
            decimal currentQ = GetQValue(p);
            decimal difference = reward + discount * maxQ - currentQ;
            foreach (KeyValuePair<string, decimal> feature in features)
            {
                try
                {
                    if (!QWeights.ContainsKey(feature.Key)) QWeights[feature.Key] = 0;
                    decimal oldWeight = QWeights[feature.Key];
                    decimal newWeight = oldWeight + learn * difference * feature.Value;
                    if (Math.Abs(newWeight) <= 1000000)
                        QWeights[feature.Key] = newWeight;
                    else WriteOutput("Warning: Weights diverging. Check that your features are valid and measured consistently with everything else.", true);
                }
                catch (Exception e)
                {
                    WriteOutput("Exception: " + e + "\n*Check that your features are valid and measured consistently with everything else.*", true);
                    Abort();
                    break;
                }
            }
            
            // Output
            foreach (KeyValuePair<string, decimal> feature in QWeights)
            {
                UpdateLearningTable(-1, feature.Key, feature.Value.ToString(), features.ContainsKey(feature.Key) ? features[feature.Key] : 0);
            }

        }

        public override void Open(object o, QState initialState)
        {
            QWeights = (Dictionary<string, decimal>)o;
            ClearLearningTable();
            int i = 1;
            foreach (KeyValuePair<string, decimal> kv in QWeights)
                UpdateLearningTable(i++, kv.Key, kv.Value.ToString(), 0);
        }

        public override object Save()
        {
            return QWeights;
        }
    }
}
