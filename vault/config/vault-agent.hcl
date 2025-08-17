vault {
  address = "http://192.168.10.17:8200"
}

auto_auth {
  method "approle" {
    mount_path = "auth/approle"
    config = {
      role_id   = "${VAULT_ROLE_ID}"
      secret_id = "${VAULT_SECRET_ID}"
    }
  }

  sink "file" {
    config = {
      path = "/run/secrets/vault_token"
    }
  }
}
