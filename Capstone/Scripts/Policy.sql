CREATE TABLE policy (
    id SERIAL PRIMARY KEY,
    policy_name VARCHAR(255) NOT NULL,
    policy_description TEXT,
    premium_amount INT NOT NULL,
    is_active BOOLEAN ,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);