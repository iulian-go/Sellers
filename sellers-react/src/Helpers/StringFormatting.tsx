export const toPascalCase = (value: string): string => {
    if (value == null || value.length === 0) return value;
    if (value.length === 1) return value.toUpperCase();

    return value.at(0)?.toUpperCase() + value.slice(1);
};