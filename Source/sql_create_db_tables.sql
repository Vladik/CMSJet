create table users
(
    id            uuid      default gen_random_uuid() not null
        primary key,
    email         text                                not null
        unique,
    password_hash text                                not null,
    created_at    timestamp default now()
);

alter table users
    owner to postgres;

create table connectors
(
    id          uuid      default gen_random_uuid() not null
        primary key,
    name        text                                not null
        unique,
    category    text
        constraint connectors_category_check
            check (category = ANY (ARRAY ['cms'::text, 'storage'::text, 'external'::text])),
    description text,
    api_url     text,
    created_at  timestamp default now()
);

alter table connectors
    owner to postgres;

create table connections
(
    id                 uuid      default gen_random_uuid() not null
        primary key,
    user_id            uuid                                not null
        references users
            on delete cascade,
    connector_id       uuid                                not null
        references connectors
            on delete cascade,
    connection_name    text                                not null,
    connection_details jsonb                               not null,
    last_sync          timestamp default now()
);

alter table connections
    owner to postgres;

create table user_storage_configs
(
    id               uuid      default gen_random_uuid() not null
        primary key,
    user_id          uuid                                not null
        references users
            on delete cascade,
    storage_provider text                                not null
        constraint user_storage_configs_storage_provider_check
            check (storage_provider = ANY (ARRAY ['azure'::text, 'aws'::text, 'gcp'::text, 'custom'::text])),
    storage_account  text                                not null,
    bucket_name      text                                not null,
    table_name       text,
    storage_region   text,
    access_key       text                                not null,
    secret_key       text                                not null,
    created_at       timestamp default now()
);

alter table user_storage_configs
    owner to postgres;

create table migrations
(
    id                   uuid      default gen_random_uuid() not null
        primary key,
    user_id              uuid                                not null
        references users
            on delete cascade,
    name                 text                                not null,
    source_connection_id uuid                                not null
        references connections
            on delete cascade,
    target_connection_id uuid                                not null
        references connections
            on delete cascade,
    status               text      default 'pending'::text
        constraint migrations_status_check
            check (status = ANY (ARRAY ['pending'::text, 'in_progress'::text, 'completed'::text, 'failed'::text])),
    created_at           timestamp default now(),
    updated_at           timestamp default now()
);

alter table migrations
    owner to postgres;

create table migration_batches
(
    id                  uuid      default gen_random_uuid() not null
        primary key,
    migration_id        uuid                                not null
        references migrations
            on delete cascade,
    name                text                                not null,
    source_content_type text                                not null,
    target_content_type text                                not null,
    content_filters     jsonb,
    status              text      default 'enabled'::text
        constraint migration_batches_status_check
            check (status = ANY (ARRAY ['enabled'::text, 'disabled'::text, 'archived'::text])),
    created_at          timestamp default now(),
    updated_at          timestamp default now()
);

alter table migration_batches
    owner to postgres;

create table migration_attempts
(
    id                      uuid      default gen_random_uuid() not null
        primary key,
    migration_id            uuid                                not null
        references migrations
            on delete cascade,
    validation_errors       jsonb,
    cloud_table_storage_url text,
    attempt_started         timestamp default now(),
    attempt_completed       timestamp,
    attempt_status          text      default 'pending'::text
        constraint migration_attempts_attempt_status_check
            check (attempt_status = ANY
                   (ARRAY ['pending'::text, 'in_progress'::text, 'completed'::text, 'failed'::text]))
);

alter table migration_attempts
    owner to postgres;

create table migration_artifacts
(
    id             uuid      default gen_random_uuid() not null
        primary key,
    migration_id   uuid                                not null
        references migrations
            on delete cascade,
    cloud_blob_url text                                not null,
    generated_at   timestamp default now()
);

alter table migration_artifacts
    owner to postgres;

create table migration_logs
(
    id              uuid      default gen_random_uuid() not null
        primary key,
    migration_id    uuid                                not null
        references migrations
            on delete cascade,
    cloud_table_url text                                not null,
    created_at      timestamp default now()
);

alter table migration_logs
    owner to postgres;

create table field_mappings
(
    id           uuid      default gen_random_uuid() not null
        primary key,
    batch_id     uuid                                not null
        references migration_batches
            on delete cascade,
    source_field text                                not null,
    target_field text                                not null,
    created_at   timestamp default now()
);

alter table field_mappings
    owner to postgres;

create table field_transformations
(
    id               uuid      default gen_random_uuid() not null
        primary key,
    field_mapping_id uuid                                not null
        references field_mappings
            on delete cascade,
    transformations  jsonb                               not null,
    created_at       timestamp default now()
);

alter table field_transformations
    owner to postgres;


