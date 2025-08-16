vault {
  address = "http://host.docker.internal:18200"
}

auto_auth {
  method "approle" {
    mount_path = "auth/approle"
    config = {
      role_id_file_path   = "/run/secrets/role_id"
      secret_id_file_path = "/run/secrets/secret_id"
    }
  }

  sink "file" {
    config = {
      path = "/run/secrets/vault_token"
    }
  }
}
