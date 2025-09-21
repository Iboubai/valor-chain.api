using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace valor_chain.api.Domain.Static
{
    using PhoneNumbers; // L'using principal de la bibliothèque

    public class PhoneNumberValidator
    {
        /// <summary>
        /// Valide un numéro de téléphone pour une région spécifique (pays).
        /// </summary>
        /// <param name="numberToValidate">Le numéro de téléphone à vérifier (ex: "06 12 34 56 78" ou "620000000").</param>
        /// <param name="regionCode">Le code ISO 3166-1 alpha-2 du pays (ex: "FR" pour la France, "GN" pour la Guinée).</param>
        /// <returns>True si le numéro est valide pour la région donnée, sinon False.</returns>
        public static bool IsValidNumber(string numberToValidate, string regionCode)
        {
            if (string.IsNullOrWhiteSpace(numberToValidate) || string.IsNullOrWhiteSpace(regionCode))
            {
                return false;
            }

            // On récupère l'instance du gestionnaire de numéros de téléphone.
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                // On essaie de "parser" (analyser) le numéro en fonction de la région.
                // Par exemple, pour la France ("FR"), il saura qu'un numéro commençant par "06" est un mobile.
                PhoneNumber phoneNumber = phoneUtil.Parse(numberToValidate, regionCode.ToUpper());

                // La bibliothèque nous dit si le numéro est considéré comme valide.
                // Cette vérification est très poussée : elle connaît les longueurs, 
                // les préfixes des opérateurs, etc.
                return phoneUtil.IsValidNumber(phoneNumber);
            }
            catch (NumberParseException)
            {
                // Si le numéro a un format complètement invalide (ex: contient des lettres),
                // le parsing échouera et lèvera une exception. On le considère donc comme invalide.
                return false;
            }
        }

        /// <summary>
        /// Tente de valider et de formater un numéro de téléphone au standard international E.164.
        /// </summary>
        /// <param name="numberToValidate">Le numéro de téléphone à vérifier.</param>
        /// <param name="regionCode">Le code ISO 3166-1 alpha-2 du pays.</param>
        /// <param name="formattedNumber">Le numéro formaté en E.164 (ex: "+224620112233") si la validation réussit.</param>
        /// <returns>True si le numéro est valide, sinon False.</returns>
        public static bool TryValidateAndFormat(string numberToValidate, string regionCode, out string? formattedNumber)
        {
            formattedNumber = null;
            if (string.IsNullOrWhiteSpace(numberToValidate) || string.IsNullOrWhiteSpace(regionCode))
            {
                return false;
            }

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber phoneNumber = phoneUtil.Parse(numberToValidate, regionCode.ToUpper());

                if (phoneUtil.IsValidNumber(phoneNumber))
                {
                    // Si le numéro est valide, on le formate au standard E.164.
                    // C'est le format idéal pour stocker les numéros dans votre base de données.
                    formattedNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164);
                    return true;
                }

                return false;
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }

}
