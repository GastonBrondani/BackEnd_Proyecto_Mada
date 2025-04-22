using System.Linq;

namespace BackendMada.Utils
{
    // Clase utilitaria para validar un CUIT: estructura, longitud y dígito verificador
    public static class validadorCUIT
    {
        // Método que recibe un CUIT como string y devuelve true si es válido, false si no
        public static bool EsCuitValido(string cuit)
        {
            //   eliminar guiones o espacios si viniera con formato 20-12345678-3
            cuit = cuit.Replace("-", "").Trim();

            //   verificamos que tenga exactamente 11 caracteres numéricos
            // Si tiene letras, símbolos, o no tiene 11 dígitos, ya lo rechazamos
            if (cuit.Length != 11 || !long.TryParse(cuit, out _))
                return false;

            //   validamos que los primeros 2 dígitos (el prefijo) sean válidos
            // Son los que indican si es persona física o jurídica
            string[] prefijosValidos = { "20", "23", "24", "27", "30", "33", "34" };
            string prefijo = cuit.Substring(0, 2);
            if (!prefijosValidos.Contains(prefijo))
                return false;

            //   multiplicadores fijos para calcular el dígito verificador
            // Se aplican a los primeros 10 dígitos del CUIT
            int[] multiplicadores = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;

            //   recorremos los primeros 10 dígitos y multiplicamos por los factores
            for (int i = 0; i < 10; i++)
            {
                int digito = int.Parse(cuit[i].ToString());
                suma += digito * multiplicadores[i];
            }

            //   sacamos el módulo 11 de la suma
            int resto = suma % 11;

            //   con ese resto, calculamos el dígito verificador que debería tener
            // Regla oficial:
            //  Si el resto es 0 => verificador = 0
            //  Si el resto es 1 => verificador = 9
            //  En otros casos => verificador = 11 - resto
            int verificadorCalculado = resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;

            //   comparamos el dígito calculado con el último dígito del CUIT (posición 10)
            int verificadorReal = int.Parse(cuit[10].ToString());

            //   si coinciden, el CUIT es válido
            return verificadorCalculado == verificadorReal;
        }
    }
}
