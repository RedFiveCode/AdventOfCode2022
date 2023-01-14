#include "FileReader.h"

std::vector<std::string> FileReader::ReadFile(const std::string& filename)
{
	// https://stackoverflow.com/questions/2602013/read-whole-ascii-file-into-c-stdstring
	std::ifstream s(filename);

	std::vector<std::string> lines;
	std::string line;

	// read the next line from file until it reaches the end
	while (std::getline(s, line))
	{
		if (line.size() > 0)
		{
			lines.push_back(line);
		}
	}

	s.close();

	return lines;
}