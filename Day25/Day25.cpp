#include "FileReader.h"
#include "SnafuConverter.h"
#include <iostream>

int main()
{
    // files are in bin folder
    //const auto lines = FileReader::ReadFile(R"(.\Example.txt)");
    const auto lines = FileReader::ReadFile(R"(.\Data.txt)");

    SnafuConverter converter;

    int64_t sum = 0;
    for (const auto line : lines)
    {
        const auto convertedValue = converter.ToDecimal(line);
        sum += convertedValue;

        std::cout << line << " -> " << convertedValue << std::endl;
    }

    const auto result = converter.ToSnafuText(sum); // "20-=0=02=-21=00-02=2" 

    std::cout << "Sum " << sum << " -> " << result << std::endl;

}
