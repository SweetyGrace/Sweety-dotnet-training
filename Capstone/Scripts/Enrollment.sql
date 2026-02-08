CREATE TABLE enrollments (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    policy_id INTEGER NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'Pending' CHECK (status IN ('Pending', 'Approved', 'Rejected')),
    requested_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    approved_at TIMESTAMP,
    
    -- Foreign Keys
    CONSTRAINT fk_user 
        FOREIGN KEY (user_id) 
        REFERENCES users(id) 
        ON DELETE CASCADE,
    
    CONSTRAINT fk_policy 
        FOREIGN KEY (policy_id) 
        REFERENCES policy(id) 
        ON DELETE CASCADE,
    
    -- Prevent duplicate enrollments
    CONSTRAINT unique_user_policy 
        UNIQUE (user_id, policy_id)
);