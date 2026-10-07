using System;
using System.Collections.Generic;
using System.Text;

namespace Part3_BuilderPattern.src
{
    public class AddressBuilder
    {
        private string _street;
        private string _city;
        private string _state;
        private string _zipCode;
        private string _country;

        public AddressBuilder WithStreet(string street)
        {
            _street = street;
            return this;
        }

        public AddressBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public AddressBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        public AddressBuilder WithZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }

        public AddressBuilder WithCountry(string country)
        {
            _country = country;
            return this;
        }

        public Address Build()
        {
            // Mandatory Field Validations
            if (string.IsNullOrWhiteSpace(_street)) throw new InvalidOperationException("Address street is mandatory.");
            if (string.IsNullOrWhiteSpace(_city)) throw new InvalidOperationException("Address city is mandatory.");
            if (string.IsNullOrWhiteSpace(_zipCode)) throw new InvalidOperationException("Address zip code is mandatory.");
            if (string.IsNullOrWhiteSpace(_country)) throw new InvalidOperationException("Address country is mandatory.");

            return new Address(_street, _city, _state, _zipCode, _country);
        }
    }
}