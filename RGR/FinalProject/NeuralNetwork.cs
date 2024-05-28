using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class NeuralNetwork
    {
        public Dictionary<Tuple<string, string>, double> wordToHidden;
        public Dictionary<Tuple<string, string>, double> hiddenToUrl;
        public List<string> wordIds;
        public List<string> hiddenIds;
        public List<string> urlIds;
        double[] ai;
        double[] ah;
        double[] ao;
        double[,] wi;
        double[,] wo;
        double[] output_deltas;
        double[] hidden_deltas;
        double error;
        public NeuralNetwork()
        {
            hiddenIds = new List<string>();
            wordToHidden = new Dictionary<Tuple<string, string>, double>();
            hiddenToUrl = new Dictionary<Tuple<string, string>, double>();
            wordIds = new List<string>();
            urlIds = new List<string>();
        }
        public double GetStrength(string fromId, string toId, int layer)
        {
            Dictionary<Tuple<string, string>, double> table;
            if (layer == 0)
            {
                table = wordToHidden;
            }
            else
            {
                table = hiddenToUrl;
            }
            if (table.ContainsKey(new Tuple<string, string>(fromId, toId)))
            {
                return table[new Tuple<string, string>(fromId, toId)];
            }
            else
            {
                if (layer == 0)
                {
                    return -0.2;
                }
                else
                {
                    return 0;
                }
            }
        }
        public void SetStrength(string fromId, string toId, int layer, double strength)
        {
            Dictionary<Tuple<string, string>, double> table;
            if (layer == 0)
            {
                table = wordToHidden;
            }
            else
            {
                table = hiddenToUrl;
            }
            if (table.ContainsKey(new Tuple<string, string>(fromId, toId)))
            {
                if (layer == 0)
                {
                    wordToHidden[new Tuple<string, string>(fromId, toId)] = strength;
                }
                else
                {
                    hiddenToUrl[new Tuple<string, string>(fromId, toId)] = strength;
                }
            }
            else
            {
                if (layer == 0)
                {
                    wordToHidden.Add(new Tuple<string, string>(fromId, toId), strength);
                }
                else
                {
                    hiddenToUrl.Add(new Tuple<string, string>(fromId, toId), strength);
                }
            }
        }
        public void GenerateHiddenNode(string[] wordIds, string[] urlIds)
        {
            if (wordIds.Length > 3 || wordIds.Length < 0)
            {
                return;
            }
            else
            {
                string hiddenId = "";
                foreach (var wi in wordIds)
                {
                    hiddenId += wi;
                }
                if (!hiddenIds.Contains(hiddenId))
                {
                    hiddenIds.Add(hiddenId);
                    foreach (var wordId in wordIds)
                    {
                        SetStrength(wordId, hiddenId, 0, 1.0 / wordIds.Length);
                    }
                    foreach (var url in urlIds)
                    {
                        SetStrength(hiddenId, url, 1, 0.1);
                    }
                }
            }
        }
        public List<string> GetAllHiddenIds(string[] worIds, string[] urlIds)
        {
            List<string> l1 = new List<string>();
            foreach (var wordId in worIds)
            {
                foreach (KeyValuePair<Tuple<string, string>, double> node in wordToHidden)
                {
                    if (node.Key.Item1 == wordId && !l1.Contains(node.Key.Item2))
                    {
                        l1.Add(node.Key.Item2);
                    }
                }
            }
            foreach (var urlId in urlIds)
            {
                foreach (KeyValuePair<Tuple<string, string>, double> node in hiddenToUrl)
                {
                    if (node.Key.Item2 == urlId && !l1.Contains(node.Key.Item1))
                    {
                        l1.Add(node.Key.Item1);
                    }
                }
            }
            return l1;
        }
        public void SetupNetwork(string[] wordIds, string[] urlIds)
        {
            this.wordIds = wordIds.ToList();
            this.urlIds = urlIds.ToList();
            this.hiddenIds = GetAllHiddenIds(wordIds, urlIds);
            ai = new double[this.wordIds.Count()];
            ah = new double[this.hiddenIds.Count()];
            ao = new double[this.urlIds.Count()];
            wi = new double[this.wordIds.Count(), this.hiddenIds.Count()];
            wo = new double[this.hiddenIds.Count(), this.urlIds.Count()];
            for (int i = 0; i < this.wordIds.Count(); i++)
            {
                for (int j = 0; j < this.hiddenIds.Count(); j++)
                {
                    wi[i, j] = GetStrength(this.wordIds[i], this.hiddenIds[j], 0);
                }
            }
            for (int i = 0; i < this.hiddenIds.Count(); i++)
            {
                for (int j = 0; j < this.urlIds.Count(); j++)
                {
                    wo[i, j] = GetStrength(this.hiddenIds[i], this.urlIds[j], 1);
                }
            }
        }
        double[] FeedForward()
        {
            for (int i = 0; i < wordIds.Count(); i++)
            {
                ai[i] = 1.0;
            }

            for (int j = 0; j < hiddenIds.Count(); j++)
            {
                double sum = 0.0;
                for (int i = 0; i < wordIds.Count(); i++)
                {
                    sum += (ai[i] * wi[i, j]);
                }
                ah[j] = 1.0 * Math.Tanh(sum);
            }

            for (int k = 0; k < urlIds.Count(); k++)
            {
                double sum = 0.0;
                for (int j = 0; j < hiddenIds.Count(); j++)
                {
                    sum += (ah[j] * wo[j, k]);
                }
                ao[k] = 1.0 * Math.Tanh(sum);
            }
            return ao;
        }
        public double[] GetResult(string[] wordIds, string[] urlIds)
        {
            SetupNetwork(wordIds, urlIds);
            return FeedForward();
        }
        double dTanh(double x)
        {
            return 1 - x * x;
        }
        void BackPropagate(double[] targets, double N)
        {
            double change;
            output_deltas = new double[urlIds.Count()];
            for (int k = 0; k < urlIds.Count(); k++)
            {
                error = targets[k] - ao[k];
                output_deltas[k] = error * dTanh(ao[k]);
            }
            hidden_deltas = new double[hiddenIds.Count()];
            for (int j = 0; j < hiddenIds.Count(); j++)
            {
                error = 0.0;
                for (int k = 0; k < urlIds.Count(); k++)
                {
                    error += (output_deltas[k] * wo[j, k]);
                }
                hidden_deltas[j] = error * dTanh(ah[j]);
            }
            for (int j = 0; j < hiddenIds.Count(); j++)
            {
                for (int k = 0; k < urlIds.Count(); k++)
                {
                    change = output_deltas[k] * ah[j];
                    wo[j, k] += (N * change);
                }
            }
            for (int i = 0; i < wordIds.Count(); i++)
            {
                for (int j = 0; j < hiddenIds.Count(); j++)
                {
                    change = hidden_deltas[j] * ai[i];
                    wi[i, j] += (N * change);
                }
            }
        }
        void UpdateDataBase()
        {
            for (int i = 0; i < wordIds.Count(); i++)
            {
                for (int j = 0; j < hiddenIds.Count(); j++)
                {
                    SetStrength(wordIds[i], hiddenIds[j], 0, wi[i, j]);
                }
            }
            for (int j = 0; j < hiddenIds.Count(); j++)
            {
                for (int k = 0; k < urlIds.Count(); k++)
                {
                    SetStrength(hiddenIds[j], urlIds[k], 1, wo[j, k]);
                }
            }
        }
        public void TrainQery(string[] wordIds, string[] urlIds, string[] selectedUrls, double[] marks)
        {
            GenerateHiddenNode(wordIds, urlIds);
            SetupNetwork(wordIds, urlIds);
            FeedForward();
            double[] targets = new double[urlIds.Length];
            for (int i = 0; i < selectedUrls.Length; i++)
            {
                targets[this.urlIds.IndexOf(selectedUrls[i])] = (marks[i] - 1.0) / 4.0;
            }
            BackPropagate(targets, 0.5);
            UpdateDataBase();
        }
    }
}
