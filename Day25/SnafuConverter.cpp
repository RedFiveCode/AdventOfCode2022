#include "SnafuConverter.h"
#include <algorithm>

int64_t SnafuConverter::ToDecimal(const std::string& snafuValue)
{
	int64_t decimalValue = 0;
	
	int64_t power = 1;
	for (int i = static_cast<int>(snafuValue.size()) - 1; i >= 0; i--)
	{
		const auto current = snafuValue.at(i);

		const auto value = snafuToDecimalMap.at(current);

		decimalValue += (value * power);

		power *= numberBase;
	}

	return decimalValue;
}


std::string SnafuConverter::ToSnafuText(const int64_t value)
{
	std::string result;

	int64_t current = value;

	while (current != 0)
	{
		int64_t remainder = current % numberBase;
		current = current / numberBase;

		if (remainder >= 3)
		{
			// cannot have digits 3 or 4
			// so increment next digit (power of 5)
			current++;
			remainder -= numberBase;
		}

		// convert remainder to snafu digit text
		result += decimalToSnafuMap.at(remainder);
	}

	// swap order
	std::reverse(result.begin(), result.end());

	return result;
}

