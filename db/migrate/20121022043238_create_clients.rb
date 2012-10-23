class CreateClients < ActiveRecord::Migration
  def change
    create_table :clients do |t|
      t.string :name, null: false
      t.boolean :closed, default: false

      t.timestamps
    end
  end
end
