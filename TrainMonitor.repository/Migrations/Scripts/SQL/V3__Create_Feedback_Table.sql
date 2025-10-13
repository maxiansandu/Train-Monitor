CREATE TABLE feedbacks
(
    id                VARCHAR(36) NOT NULL PRIMARY KEY,
    username          VARCHAR(100),
    reason_for_delay  VARCHAR(500),
    aditional_message VARCHAR(1000),
    train_number int,
    train_id          VARCHAR(36) NOT NULL,
    user_id           VARCHAR(36) NOT NULL,

    CONSTRAINT feedbacks_train_id_fkey
        FOREIGN KEY (train_id)
            REFERENCES trains (id)
            ON DELETE CASCADE,

    CONSTRAINT feedbacks_user_id_fkey
        FOREIGN KEY (user_id)
            REFERENCES accounts (id)
            ON DELETE CASCADE
);
