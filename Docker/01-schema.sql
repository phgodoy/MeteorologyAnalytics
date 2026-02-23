CREATE TABLE weather (
    id SERIAL PRIMARY KEY,
    date DATE NOT NULL,
    temperature_c INT NOT NULL,
    temperature_f INT NOT NULL,
    summary VARCHAR(50) NOT NULL
);