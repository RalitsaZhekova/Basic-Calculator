using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicCalculator
{
    public partial class Calculator : Form
    {
        decimal result = 0m;
        string operatorSymbol = "";
        bool isOperatorClicked = false;

        decimal memoryStore = 0m;

        public Calculator()
        {
            InitializeComponent();
        }

        //in case of devision by zero
        private void ButtonDisable()
        {
            foreach (Button button in this.Controls.OfType<Button>())
            {
                if (button.Text == "C")
                {
                    continue;
                }
                button.Enabled = false;
            }
        }

        //restores default functionality; checks if memory store is empty
        private void ButtonEnable()
        {
            foreach (Button button in this.Controls.OfType<Button>())
            {
                if (button.Text == "")
                {
                    continue;
                }

                button.Enabled = true;

                if (memoryStore == 0)
                {
                    btnMemoryClear.Enabled = false;
                    btnMemoryRecall.Enabled = false;
                }
            }
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            Size = new Size(433, 633);
        }

        // stores all numerical values; checkes for multiple  full stops
        private void buttonValue_click(object sender, EventArgs e)
        {
            Button buttonValue = (Button)sender;

            if ((txtDisplayInput.Text == "0") || isOperatorClicked)
            {
                txtDisplayInput.Clear();
            }

            if (buttonValue.Text == ".")
            {
                if (!txtDisplayInput.Text.Contains("."))
                {
                    txtDisplayInput.Text += buttonValue.Text;
                }
            }
            else
            {
                txtDisplayInput.Text += buttonValue.Text;
            }

            isOperatorClicked = false;
        }

        //stores all the basic operators and shows the expression above the input area; assigns temp result value
        private void buttonOperator_click(object sender, EventArgs e)
        {
            Button buttonOperator = (Button)sender;

            if (result != 0)
            {
                btnEquals.PerformClick();
                operatorSymbol = buttonOperator.Text;
                result = decimal.Parse(txtDisplayInput.Text);
                isOperatorClicked = true;
                lblViewExpression.Text = result + " " + operatorSymbol;
            }
            else
            {
                operatorSymbol = buttonOperator.Text;
                result = decimal.Parse(txtDisplayInput.Text);
                isOperatorClicked = true;
                lblViewExpression.Text = result + " " + operatorSymbol;
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtDisplayInput.Text = "0";
            lblViewExpression.Text = "";
            result = 0m;
            ButtonEnable();
        }

        //shows the final result depending on the operator pressed
        private void btnEquals_Click(object sender, EventArgs e)
        {
            switch (operatorSymbol)
            {
                case "+":
                    txtDisplayInput.Text = (result + decimal.Parse(txtDisplayInput.Text)).ToString();
                    break;
                case "-":
                    txtDisplayInput.Text = (result - decimal.Parse(txtDisplayInput.Text)).ToString();
                    break;
                case "x":
                    txtDisplayInput.Text = (result * decimal.Parse(txtDisplayInput.Text)).ToString();
                    break;
                case "/":
                    if (decimal.Parse(txtDisplayInput.Text) == 0)
                    {
                        txtDisplayInput.Text = "You cannot devide by zero";
                        ButtonDisable();
                        return;
                    }
                    else
                    {
                        txtDisplayInput.Text = (result / decimal.Parse(txtDisplayInput.Text)).ToString();
                    }
                    break;
                case "x^2":
                    txtDisplayInput.Text = (result * result).ToString();
                    break;
                default:
                    break;
            }
            result = decimal.Parse(txtDisplayInput.Text);
            operatorSymbol = "";
            lblViewExpression.Text = "";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtDisplayInput.Text.Length > 0)
            {
                txtDisplayInput.Text = txtDisplayInput.Text.Remove(txtDisplayInput.Text.Length - 1, 1);
            }

            if (txtDisplayInput.Text == "")
            {
                txtDisplayInput.Text = "0";
            }
        }

        // assigns operator and serves as the equals button as only one input value is needed
        private void btnSquare_Click(object sender, EventArgs e)
        {
            operatorSymbol = btnSquare.Text;
            isOperatorClicked = true;
            lblViewExpression.Text = txtDisplayInput.Text + " " + operatorSymbol;
            double inputValue = double.Parse(txtDisplayInput.Text);
            inputValue = Math.Pow(inputValue, 2);
            txtDisplayInput.Text = inputValue.ToString();
            result = decimal.Parse(txtDisplayInput.Text);
            operatorSymbol = "";
        }

        private void btnSquareRoot_Click(object sender, EventArgs e)
        {
            operatorSymbol = btnSquareRoot.Text;
            isOperatorClicked = true;
            lblViewExpression.Text = txtDisplayInput.Text + " " + operatorSymbol;
            double inputValue = double.Parse(txtDisplayInput.Text);
            inputValue = Math.Sqrt(inputValue);
            txtDisplayInput.Text = inputValue.ToString();
            result = decimal.Parse(txtDisplayInput.Text);
            operatorSymbol = "";
        }

        private void btnDenominator_Click(object sender, EventArgs e)
        {
            operatorSymbol = $"1/({txtDisplayInput.Text})";
            isOperatorClicked = true;
            lblViewExpression.Text = operatorSymbol;
            double inputValue = double.Parse(txtDisplayInput.Text);
            inputValue = 1.0 / inputValue;
            txtDisplayInput.Text = inputValue.ToString();
            result = decimal.Parse(txtDisplayInput.Text);
            operatorSymbol = "";
        }

        // All memory functionalities
        private void btnMemoryStore_Click(object sender, EventArgs e)
        {
            memoryStore = decimal.Parse(txtDisplayInput.Text);
            btnMemoryClear.Enabled = true;
            btnMemoryRecall.Enabled = true;
        }

        private void btnMemoryClear_Click(object sender, EventArgs e)
        {
            memoryStore = 0;
            btnMemoryClear.Enabled = false;
            btnMemoryRecall.Enabled = false;
        }

        private void btnMemoryRecall_Click(object sender, EventArgs e)
        {
            txtDisplayInput.Text = memoryStore.ToString();
        }

        private void btnMemoryAdd_Click(object sender, EventArgs e)
        {
            if (memoryStore == 0)
            {
                btnMemoryStore.PerformClick();
            }
            else
            {
                memoryStore += decimal.Parse(txtDisplayInput.Text);
            }

        }

        private void btnMemorySubtract_Click(object sender, EventArgs e)
        {
            if (memoryStore == 0)
            {
                btnMemoryStore.PerformClick();
            }
            else
            {
                memoryStore -= decimal.Parse(txtDisplayInput.Text);
            }
        }

        //key bindings
        private void Calculator_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    buttonValue_click(btn1, e);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    buttonValue_click(btn2, e);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    buttonValue_click(btn3, e);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    buttonValue_click(btn4, e);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    buttonValue_click(btn5, e);
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    buttonValue_click(btn6, e);
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    buttonValue_click(btn7, e);
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    buttonValue_click(btn8, e);
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    buttonValue_click(btn9, e);
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    buttonValue_click(btn0, e);
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    buttonValue_click(btnPoint, e);
                    break;
                case Keys.Delete:
                case Keys.Escape:
                    btnClearAll.PerformClick();
                    break;
                case Keys.Back:
                    btnBackspace.PerformClick();
                    break;
                case Keys.Add:
                    buttonOperator_click(btnAdd, e);
                    break;
                case Keys.Subtract:
                    buttonOperator_click(btnSubtract, e);
                    break;
                case Keys.Multiply:
                    buttonOperator_click(btnMultiply, e);
                    break;
                case Keys.Divide:
                    buttonOperator_click(btnDivide, e);
                    break;
                case Keys.D:
                    btnDenominator.PerformClick();
                    break;
                case Keys.Q:
                    btnSquare.PerformClick();
                    break;
                case Keys.T:
                    btnSquareRoot.PerformClick();
                    break;
                case Keys.M:
                    btnMemoryStore.PerformClick();
                    break;
                case Keys.R:
                    btnMemoryRecall.PerformClick();
                    break;
                case Keys.C:
                    btnMemoryClear.PerformClick();
                    break;
                case Keys.A:
                    btnMemoryAdd.PerformClick();
                    break;
                case Keys.S:
                    btnMemorySubtract.PerformClick();
                    break;
                //on Enter -> changed default focus to be on equals button; Enter targets whatever is on focus; 0emplus works as intended
                case Keys.Enter:
                case Keys.Oemplus:
                    btnEquals.PerformClick();
                    break;
                default:
                    break;
            }
        }

        // give instructions
        private void btnHotKeys_Click(object sender, EventArgs e)
        {
            if (Width == 433)
            {
                Size = new Size(777, 633);
            }
            else if(Width == 777)
            {
                Size = new Size(433, 633);
            }

            rtbHints.Text = $"Welcome to my project - Simple Calculator created with Windows Forms, using C#. \n \n" +
                $"You can operate it by using the buttons on the interface, as well as by typing on the keyboard. \n\n" +
                $"For the basic operations - use the NumPad; \n" +
                $"Square - Q \n" +
                $"Square root - T \n" +
                $"Denominator - D \n\n" +

                $"Equals - the equals button (next to the dash);  \n" +
                $"Clear entry - Delete or Escape;  \n" +
                $"Undo(Step back) - Backspace; \n\n" +
                
                $"Memory Story - M \n" +
                $"Memory recall - R \n" +
                $"Memory clear - C \n" +
                $"Memory add - A \n" +
                $"Memory Subtract - S";
        }
    }
}
