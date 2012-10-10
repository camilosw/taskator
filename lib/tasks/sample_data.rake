namespace :db do
  desc "Llena la base de datos con datos de ejemplo"
  task populate: :environment do

    User.create!(nombre_usuario: "admin",
                 email: "prueba@prueba.com",
                 password: "desarrollo",
                 password_confirmation: "desarrollo")
    9.times do |i|
      User.create!(nombre_usuario: Faker::Internet.user_name,
                   email: Faker::Internet.email,
                   password: "desarrollo",
                   password_confirmation: "desarrollo")
    end

    10.times do |i|
      cliente = Cliente.create!(nombre: Faker::Company.name)
      5.times do |j|
        cliente.proyectos.create!(nombre: Faker::Lorem.words(3))
      end
    end
    Estado.create!(nombre: 'Pendiente')
    Estado.create!(nombre: 'Falta información')
    Estado.create!(nombre: 'En proceso')
    Estado.create!(nombre: 'Terminado')
    Estado.create!(nombre: 'Cerrado')
    Estado.create!(nombre: 'Rechazado')
    Estado.create!(nombre: 'Corrección')
  end
end