# ✅ Dirección del servidor Vault
vault {
  address = "http://192.168.10.17:8200"
}

# ✅ No salgas después de autenticar
exit_after_auth = false

# ✅ Habilita el manejo de cache y renovación automática del token
cache {
  use_auto_auth_token = true
}

# ✅ Archivo PID opcional
#pid_file = "/var/run/vault-agent.pid"

# ✅ Método de autenticación: AppRole
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
      mode = 0600
    }
  }
}

# ✅ Listener interno para debug o para otros servicios internos (como `vault-template`, si usaras)
listener "tcp" {
  address     = "0.0.0.0:8200"
  tls_disable = true
}

# ✅ Nivel de log
log_level = "info"
