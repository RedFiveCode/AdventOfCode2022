#pragma once
#include <string>
#include <vector>
#include <fstream>

class FileReader
{
public:
	static std::vector<std::string> ReadFile(const std::string& filename);
};

