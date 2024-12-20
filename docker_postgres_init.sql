DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'HangfireDb') THEN
        PERFORM dblink_exec('dbname=postgres', 'CREATE DATABASE HangfireDb');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'NotifyMasterDb') THEN
        PERFORM dblink_exec('dbname=postgres', 'CREATE DATABASE NotifyMasterDb');
    END IF;
END
$$ LANGUAGE plpgsql;