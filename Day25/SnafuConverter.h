#pragma once
#include <string>
#include <vector>
#include <map>

class SnafuConverter
{
public:
	int64_t ToDecimal(const std::string& snafuValue);
	std::string ToSnafuText(const int64_t value);

private:
	const int64_t numberBase = 5;

	const std::map<char, int> snafuToDecimalMap
	{
		{ '2', 2 },
		{ '1', 1 },
		{ '0', 0 },
		{ '-', -1 },
		{ '=', -2 }
	};

	const std::map<int, char> decimalToSnafuMap
	{
		{ -2, '=' },
		{ -1, '-' },
		{  0, '0' },
		{  1, '1' },
		{  2, '2' }
	};
};

