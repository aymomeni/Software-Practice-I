// Skeleton written by Joe Zachary for CS 3500, January 2014
/*
 * Ali Momeni - CS 3500 - Assignment 1 - January 25, 2015
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;


namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public class Formula
    {
        // the string array tokens holds all of the tokens for the formula class's methods to use
        List<String> tokens = new List<String>();

        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using standard C# syntax for double/int literals), 
        /// variable symbols (one or more letters followed by one or more digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// An example of a valid parameter to this constructor is "2.5e9 + x5 / 17".
        /// Examples of invalid parameters are "x", "-5.3", and "2 5 + 3";
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula)
        {
            // running the formula string through the getTokens method
            IEnumerable<string>tempTokens = GetTokens(formula);


            // parenthesis counters
            int leftParenthesis = 0;
            int rightParenthesis = 0;

            // first element boolean that helps check if the first element is valid
            bool firstElement = true;

            // string that represents the last token added
            string lastToken = "";


            // varify that there aren't any invalid tokens
            foreach(string s in tempTokens){

                // Removing empty spaces (including repetitive spaces) in the string array
                s.Trim();
                if (s == "" || s == " ")
                    continue;
                
                // checking if the token is valid
                if (s.Equals("+") | s.Equals("-") | s.Equals("*") | s.Equals("/") | s.Equals("(") | s.Equals(")") | isVariable(s) | isValidNumericalValue(s))
                {
                    //checking if the first element is a number, a variable, or an opening parenthesis.
                    if(firstElement == true){
                        if(s.Equals(isValidNumericalValue(s)) | isVariable(s) | s.Equals("(")){
                            firstElement = false; 
                        } else { throw new FormulaFormatException("The first element was not a number, a variable, or an opening parenthesis"); }
                    }

                    // keeping track of the parenthesis count
                    if (s.Equals("(")) { leftParenthesis++; } 
                    if (s.Equals(")")) { rightParenthesis++; }
                    if (rightParenthesis > leftParenthesis) { throw new FormulaFormatException("There were more closing parenthesis then opening parenthesis"); }

                    // checking token that immediately follows an opening parenthesis or an operator must be either a number, a variable, or an opening parenthesis
                    if(lastToken.Equals("(") | lastToken.Equals("+") | lastToken.Equals("-") | lastToken.Equals("*") | lastToken.Equals("/")){
                        if(s.Equals("(") | isVariable(s) | isValidNumericalValue(s)){ }
                        else throw new FormulaFormatException("A Token that immediately followed an opening parenthesis or an operator was not a number, a variable, or an opening parenthesis");
                    }

                    // checking token that immediately follows a number, a variable, or a closing parenthesis must be either an operator or a closing parenthesis
                    if(isValidNumericalValue(lastToken) | isVariable(lastToken) | lastToken.Equals(")")){
                        if(s.Equals("+") | s.Equals("-") | s.Equals("*") | s.Equals("/") | s.Equals(")")){ }
                        else throw new FormulaFormatException("A Token that immediately followed a number, a variable, or a closing parenthesis was not an operator or a closing parenthesis");
                    }
                    
                    tokens.Add(s);
                    lastToken = s;

                    continue;
                } 
                else { throw new FormulaFormatException("Invalid token was used."); } // if s is is invalid we throw an exception with an explanatory message
            }

            // varifying if there is at least one token
            if (tokens.Count == 0)
            {
                throw new FormulaFormatException("There wasn't at least one token.");
            }

            // the last token of an expression must be a number, a variable, or a closing parenthesis.
            if(isValidNumericalValue(lastToken) | isVariable(lastToken) | lastToken.Equals(")")){}
            else throw new FormulaFormatException("The last token of the expression was not a number, variable, or a closing parenthesis");
        }



        /// <summary>
        /// A Lookup function is one that maps some strings to double values.  Given a string,
        /// such a function can either return a double (meaning that the string maps to the
        /// double) or throw an ArgumentException (meaning that the string is unmapped.
        /// Exactly how a Lookup function decides which strings map to doubles and which
        /// don't is up to the implementation of that function.
        /// </summary>
        public delegate double Lookup(string s);

        /// <summary>
        /// Evaluates this Formula, using lookup to determine the values of variables.  
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, throw a FormulaEvaluationException  
        /// with an explanatory Message.
        /// </summary>
        public double Evaluate(Lookup lookup)
        {
            Stack<String> Operator = new Stack<string>();
            Stack<String> Value = new Stack<string>();
            
            
            return 0;
        }

        /// <summary>
        /// Checks if the parameter string is a valid numerical value (validity described in the class description)
        /// </summary>
        /// <returns>True if it is a valid numerical value</returns>
        private Boolean isValidNumericalValue(string numericalValue)
        {
            //if numericalValue is an integer          
            int intValue = 0;
            // checking if t is an integer
            if (int.TryParse(numericalValue, out intValue))
            {
                return true;
            }

            //if numericalValue is a double
            double doubleValue = 0;
            // checking if t is a double
            if (double.TryParse(numericalValue, out doubleValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the input string is a variable
        /// </summary>
        /// <returns>True if the input string is a variable, false otherwise</returns>
        private Boolean isVariable(string variable)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+[0-9]+$"); // any repeated lowercase or uppercase letters with any amout of numbers concatinated
            Match match = regex.Match(variable);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This is a helper method that performs arithmetic addition and subtraction
        /// </summary>
        /// <param name="x"> left operand </param> 
        /// <param name="y"> right operand </param> 
        /// <param name="operation"> char that represents a - or + operation </param>
        /// <returns></returns>
        private static int additionAndSubtraction(int x, int y, string operation)
        {
            if (operation.Equals('+'))
            {
                x += y;
            }
            else if (operation.Equals('-'))
            {
                x -= y;
            }
            return x;
        }


        /// <summary>
        /// This is a helper method that performs arithmetic multiplication and division
        /// </summary>
        /// <param name="x"> left operand </param>
        /// <param name="y"> right operand </param>
        /// <param name="operation"> arithmetic operation * or / </param>
        /// <returns></returns>
        private static int multiplicationAndDivision(int x, int y, string operation)
        {
            if (operation.Equals('/'))
            {
                if (y == 0)
                    throw new ArgumentException("Division by 0 is unsupported.");

                x = x / y;
            }
            else if (operation.Equals('*'))
                x = x * y;

            return x;
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of one or more
        /// letters followed by one or more digits, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z]+\d+";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message)
            : base(message)
        {
        }
    }
}
