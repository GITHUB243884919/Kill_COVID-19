using System;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace _24PointGame
{
    public class CardOperator
    {
        private  List<int> _cards = new List<int>();
        const double Threadhold = 0.0000001F;

        public List<string> showResult = new List<string>();

        public void InitCards(int num1, int num2, int num3, int num4)
        {
            _cards.Clear();
            _cards.Add(num1);
            _cards.Add(num2);
            _cards.Add(num3);
            _cards.Add(num4);
        }

        /// <summary>
        /// 对数组a所有可能的排列进行组合运算并返回运算的结果
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public string Operate(double checkResult)
        {
            string result = "";
            List<string> results = new List<string>();
            for (int ai = 0; ai < _cards.Count; ai++)
            {
                int a1 = _cards[ai];
                for (int bi = 0; bi < _cards.Count; bi++)
                {
                    if (bi != ai)
                    {
                        int a2 = _cards[bi];
                        for (int ci = 0; ci < _cards.Count; ci++)
                        {
                            if (ci != ai && ci != bi)
                            {
                                int a3 = _cards[ci];
                                for (int di = 0; di < _cards.Count; di++)
                                {
                                    if (di != ai && di != bi && di != ci)
                                    {
                                        int a4 = _cards[di];
                                        if (OperateTwoTwo(new int[] { a1, a2, a3, a4 }, checkResult, out result))
                                        {
                                            if (!results.Contains(result))
                                            {
                                                //Console.WriteLine(result);
                                                results.Add(result);
                                            }
                                            //return result;
                                        }
                                        if (OperateTreeOne(new int[] { a1, a2, a3, a4 }, checkResult, out result))
                                        {
                                            if (!results.Contains(result))
                                            {
                                                //Console.WriteLine(result);
                                                results.Add(result);
                                            }
                                            // return result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            showResult.Clear();
            showResult.AddRange(results);
            return $"计算完成，共有 {results.Count} 种运算方式。" ;
        }

        /// <summary>
        /// (a1{Exp}a2){Exp}(a3{Exp}a4)
        /// </summary>
        /// <returns></returns>
        private bool OperateTwoTwo(int[] cards, double checkResult, out string expression)
        {
            List<ExpressionOperator> a12 = Operates(cards[0], cards[1]);
            List<ExpressionOperator> a34 = Operates(cards[2], cards[3]);
            for (int ai = 0; ai < a12.Count; ai++)
            {
                ExpressionOperator a = a12[ai];
                for (int bi = 0; bi < a34.Count; bi++)
                {
                    ExpressionOperator b = a34[bi];
                    List<ExpressionOperator> ab = Operates(a.GetResult(), b.GetResult());
                    for (int abi = 0; abi < ab.Count; abi++)
                    {
                        double h = Math.Abs(ab[abi].GetResult() - checkResult);
                        if (h < Threadhold)
                        {
                            expression = ab[abi].GetExpressionString(a.GetExpressionString(), b.GetExpressionString())+$"={ab[abi].GetResult()}";
                            return true;
                        }
                    }
                }
            }
            expression = "";
            return false;
        }

        /// <summary>
        /// (a1{Exp}a2{Exp}a3){Exp}a4
        /// </summary>
        /// <returns></returns>
        private bool OperateTreeOne(int[] cards, double checkResult, out string expression)
        {
            List<ExpressionOperator> a12 = Operates(cards[0], cards[1]);//(a1{Exp}a2{Exp}a3){Exp}a4
            //double[] a34 = Operates(new double[] { a3, a4 });
            for (int ai = 0; ai < a12.Count; ai++)
            {
                ExpressionOperator a = a12[ai];
                List<ExpressionOperator> a123 = Operates(a.GetResult(), cards[2]);
                for (int bi = 0; bi < a123.Count; bi++)
                {
                    ExpressionOperator b = a123[bi];
                    List<ExpressionOperator> ab = Operates(b.GetResult(), cards[3]);
                    for (int abi = 0; abi < ab.Count; abi++)
                    {
                        if (Math.Abs(ab[abi].GetResult() - checkResult) < Threadhold)
                        {
                            expression = ab[abi].GetExpressionString(b.GetExpressionString(a.GetExpressionString(), b.Num_b), cards[3]) + $"={ab[abi].GetResult()}";
                            return true;
                        }
                    }
                }
            }
            expression = "";
            return false;
        }

        /// <summary>
        /// 运算两个数的四则运算并将结果作为一个数组返回
        /// </summary>
        /// <param name="num_a">The number a.</param>
        /// <param name="num_b">The number b.</param>
        /// <returns></returns>
        private List<ExpressionOperator> Operates(double num_a, double num_b)
        {
            var expressionOperator = new List<ExpressionOperator>();
            //return new ExpressionOperator[]
            {
                expressionOperator.Add(new ExpressionOperator(num_a, num_b, Expressions.Addition));
                expressionOperator.Add(new ExpressionOperator(num_a, num_b, Expressions.Subtraction));
                expressionOperator.Add(new ExpressionOperator(num_a, num_b, Expressions.NSubtraction));
                expressionOperator.Add(new ExpressionOperator(num_a, num_b, Expressions.Multiplication));
                expressionOperator.Add(new ExpressionOperator(num_a, num_b, Expressions.Division));
                expressionOperator.Add(new ExpressionOperator(num_a, num_b, Expressions.NDivision));
            };

            return expressionOperator;
        }

    }
}
