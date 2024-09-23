namespace ClienteAPI.Repositorio
{
    public class ValidarCpf
    {

        public static bool ValidaCPF(string Cpf)
        {
            string cpfSemTraco = Cpf.Replace(".", "");
            cpfSemTraco = cpfSemTraco.Replace("-", "");
            cpfSemTraco = cpfSemTraco.Replace(",", "");

            if (cpfSemTraco.Length != 11)
            {
                return false;
            }

            int soma = 0;
            int soma2 = 0;
            int cont = 10;
            int cont2 = 11;


            for (int i = 0; i < cpfSemTraco.Length - 2; i++)
            {
                soma += Convert.ToInt32(cpfSemTraco[i].ToString()) * cont;
                cont--;
            }
            //  MessageBox.Show(soma.ToString());

            if (soma % 11 < 2)
            {
                int digito1 = 0;
                if (digito1 != Convert.ToInt32(cpfSemTraco[9].ToString()))
                {
                    return false;
                }

                for (int i = 0; i < cpfSemTraco.Length - 1; i++)
                {
                    soma2 += Convert.ToInt32(cpfSemTraco[i].ToString()) * cont2;
                    cont2--;
                }
                if (soma2 % 11 < 2)
                {
                    int digito2 = 0;
                    if (digito2 != Convert.ToInt32(cpfSemTraco[10].ToString()))
                    {
                        return false;
                    }
                    return true;
                }
                else if (soma2 % 11 >= 2)
                {
                    int digito2 = 11 - (soma2 % 11);

                    if (digito2 != Convert.ToInt32(cpfSemTraco[10].ToString()))
                    {
                        return false;
                    }
                    return true;
                }
                // MessageBox.Show(soma2.ToString());
            }
            else if (soma % 11 >= 2)
            {
                int digito1 = 11 - (soma % 11);

                if (digito1 != Convert.ToInt32(cpfSemTraco[9].ToString()))
                {
                    return false;
                }

                for (int i = 0; i < cpfSemTraco.Length - 1; i++)
                {
                    soma2 += Convert.ToInt32(cpfSemTraco[i].ToString()) * cont2;
                    cont2--;
                }
                // MessageBox.Show(soma2.ToString());
                if (soma2 % 11 < 2)
                {
                    int digito2 = 0;
                    if (digito2 != Convert.ToInt32(cpfSemTraco[10].ToString()))
                    {
                        return false;
                    }
                    return true;
                }
                else if (soma2 % 11 >= 2)
                {
                    int digito2 = 11 - (soma2 % 11);

                    if (digito2 != Convert.ToInt32(cpfSemTraco[10].ToString()))
                    {
                        return false;
                    }

                    return true;
                }
            }
            return true;
        }

    }
}
