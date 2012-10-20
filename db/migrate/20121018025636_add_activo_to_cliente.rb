class AddActivoToCliente < ActiveRecord::Migration
  def change
    add_column :clientes, :activo, :boolean
  end
end
