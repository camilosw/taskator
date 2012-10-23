namespace :db do
  desc "Populate the database with sample data"
  task populate: :environment do

    9.times do |i|
      User.create!(email: Faker::Internet.email,
                   password: "password",
                   password_confirmation: "password")
    end

    10.times do |i|
      client = Client.create!(name: Faker::Company.name, 
                              closed: [true, false].sample)
      rand(1..4).times do |j|
        project = client.projects.create!(name: Faker::Lorem.sentence(3))
        rand(7).times do |k|
          task = project.tasks.new(description: Faker::Lorem.sentence(rand(10..20)), 
                                  closed: [true, false].sample)
          task.creator = User.first(:order => "RANDOM()")
          task.assigned = User.first(:order => "RANDOM()")          
          task.save
        end
      end
    end
  end
end