vault {
  address = "http://192.168.10.17:8200"
}

auto_auth {
  method "approle" {
    mount_path = "auth/approle"
    config = {
      role_id_file_path   = "/vault/secrets/role_id"
      secret_id_file_path = "/vault/secrets/secret_id"
    }
  }

  sink "file" {
    config = {
      path = "/run/secrets/vault_token"
    }
  }
}
