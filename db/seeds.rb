# This file should contain all the record creation needed to seed the database with its default values.
# The data can then be loaded with the rake db:seed (or created alongside the db with db:setup).
#
# Examples:
#
#   cities = City.create([{ name: 'Chicago' }, { name: 'Copenhagen' }])
#   Mayor.create(name: 'Emanuel', city: cities.first)

User.create!(nombre_usuario: "admin",
             email: "prueba@prueba.com",
             password: "desarrollo",
             password_confirmation: "desarrollo")

['Pendiente', 'Falta información', 'En proceso', 'Terminado', 'Cerrado', 'Rechazado', 'Corrección'].each do |name|
  Estado.create(nombre: name)
end